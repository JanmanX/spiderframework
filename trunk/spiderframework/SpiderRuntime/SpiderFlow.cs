using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using fox.spider;
using fox.spider.runtime.interfaces;
using fox.spider.runtime.utils;
using fox.spider.runtime.constants;
using mshtml;

namespace fox.spider.runtime
{
    /// <summary>
    /// A SpiderFlow analyzes a web page in SpiderRuntime. There may be a plenty of SpiderFlows in 
    /// a SpiderRuntime. And there may be a plenty of SpiderFlows analyzing a web page.
    /// 
    /// You may view a spider flow as a compund IDocumentParser. It hosts a lot of IDocumentParser.
    /// 
    /// In initialization period, a SpiderFlow creates a lot of IDocumentParser and a lot of 
    /// IElementRowContent, and connects them together. 
    /// 
    /// In execution preiod, a SpiderFlow calls the top document parser to analyze a web page. 
    /// After the analysis procedure of the top document parser, it calls its next document parser
    /// to do so.
    /// 
    /// SpiderFlow �O SpiderRuntime ���ΨӤ��R�������@�� flow �A���O�h�� IDocumentParser ����X��C
    /// �b��l���q�A���|�N������ IDocumentParser ����l�ƥX�ӡA�P�ɤ]�|�� IElementRowContent �����W
    /// IDocumentParser �����C
    /// </summary>
    public class SpiderFlow : IRuntimeUnit
    {
        private IGlobalResourceProvider m_GlobalResourceProvider;

        private string m_Name;
        private string m_Id;
        private Regex m_UrlPattern;
        private XmlElement m_Element;
        //private IWebPageTurner m_WebPageTurner;//a useless member
        private FlowResourcePool m_ResourcePool=new FlowResourcePool();
        private IDocumentParser m_TopParser;

        #region Properties
        /// <summary>
        /// The name of this spider flow.
        /// �o�� SpiderFlow ���W��
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
       /// <summary>
       /// The id of this spider flow.
       /// �o�� SpiderFlow �� id �A���N�|�O XmlElement �����w�� id �C
       /// </summary>
        public string Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }
        /// <summary>
        /// The url pattern of this spider flow. This pattern is used to exam which web page should be 
        /// analyzed by this flow.
        /// �o�� SpiderFlow �t�d�B�z�� UrlPattern �ASpiderRuntime �|�ϥγo�� Regex �ӧP�_�o�� SpiderFlow �O�_�n����C
        /// </summary>
        public Regex UrlPattern
        {
            get { return m_UrlPattern; }
            set { m_UrlPattern = value; }
        }
        /// <summary>
        /// An XmlElement to initialize this spider flow.
        /// �o�� SpiderFlow �� XmlElement �ѼơC
        /// </summary>
        protected XmlElement Element
        {
            get { return m_Element; }
            set { m_Element = value; }
        }

        /// 
        /// we don't turn any page here.
        /// �o�� SpiderFlow �B�z������|�Ψ쪺 IWebPageTurner �A�o���ݩʥi��|�O null �C
        ///
        //public IWebPageTurner WebPageTurner
        //{
        //    get { return m_WebPageTurner; }
        //    set { m_WebPageTurner = value; }
        //}

        /// <summary>
        /// a global resource
        /// ���o IGlobalResourceProvider
        /// </summary>
        protected IGlobalResourceProvider GlobalResourceProvider
        {
            get { return m_GlobalResourceProvider; }
            set { m_GlobalResourceProvider = value; }
        }
        /// <summary>
        /// a flow resource stored in this flow.
        /// �o�� SpiderFlow �� FlowResourcePool �C
        /// </summary>
        protected FlowResourcePool ResourcePool
        {
            get { return m_ResourcePool; }
            set { m_ResourcePool = value; }
        }

        #endregion

        #region Constructors

        public SpiderFlow()
        {
        }

        public SpiderFlow(XmlElement elm)
        {
            Element = elm;

        }

        #endregion

        #region Methods
        /// <summary>
        /// Initialize this spider flow. This method reads name, id, url pattern from XmlElement and initialize them 
        /// to crossponding attribtues. Then, it creates all row content parsers, and all document parsers. In creation of 
        /// row content/document parser, this method connects the previous/next parsers by the sequence of xml element.
        /// Finally, this method creates row filters and hooks them to specified document parser.
        /// 
        /// ��l�ƾ�� Spider Flow�A���|Ū�� XmlElement ���� Name�BId�BUrlPattern�A�ç�o�Ǫ����l�ƨ��ݩʤ����C
        /// �t�~�A���|����l�� RowContentParser �A���U�Ӫ�l�� IDocumentParser �A���U�ӦA��l�� IRowFilter �C
        /// </summary>
        public void init()
        {
            if (Element == null)
            {
                throw new Exception("A spider flow needs a XmlElement configuration node.");
            }

            Name = Element.GetAttribute("name");
            Id = Element.GetAttribute("id");
            //init UrlPattern
            XmlNodeList oUrlPatternList = Element.GetElementsByTagName("UrlPattern", SpiderRuntimeConstants.DefaultNamespace);
            if (oUrlPatternList.Count == 0)
            {
                throw new Exception("A spider flow should have at least one Url Pattern.");
            }
            else
            {
                initUrlPattern((XmlElement)oUrlPatternList[0]);
            }

            //init RowContentParser
            XmlNodeList oRowContentParserList = Element.GetElementsByTagName("RowContentParsers", SpiderRuntimeConstants.DefaultNamespace);
            if (oRowContentParserList.Count > 0)
            {
                initRowContentParser((XmlElement)oRowContentParserList[0]);
                
            }
            // before the initialization of document parsers, you should initialize row content parser.
            //init DocumentParsers, �b��l�� DocumentParser ���e�A��������l�Ʀn RowContentParser
            XmlNodeList oDocumentParserList = Element.GetElementsByTagName("DocumentParsers", SpiderRuntimeConstants.DefaultNamespace);
            if (oDocumentParserList.Count > 0)
            {
                m_TopParser = initDocumentParser((XmlElement)oDocumentParserList[0]);
            }
            else
            {
                throw new Exception("A spider flow should have at least one Document Parser.");
            }
            // before the initialization of row filter, you should initialize document parser.
            //init RowFilter, �b��l�� RowFilter ���e�A��������l�Ʀn Document Parser
            XmlNodeList oRowFilterList = Element.GetElementsByTagName("RowFilters", SpiderRuntimeConstants.DefaultNamespace);
            if (oRowFilterList.Count > 0)
            {
                initRowFilter((XmlElement)oRowFilterList[0]);
            }

            //init Page Turner
            //XmlNodeList oPageTurnerList = Element.GetElementsByTagName("PageTurner", SpiderRuntimeConstants.DefaultNamespace);
            //if (oPageTurnerList.Count > 0)
            //{
            //    initWebPageTurner((XmlElement)oPageTurnerList[0]);
            //}            
        }
        /// <summary>
        /// do analysis.
        /// ���� Spider Flow �����R�u�@
        /// </summary>
        /// <param name="doc"></param>
        public bool run(IHTMLDocument2 doc)
        {
            return m_TopParser.parse(doc);
        }
        /// <summary>
        /// init url pattern. this method converts inner text of elem to Regex object.
        /// ��l�� Urlpattern �A�N XmlElement �����Ѽƪ�l�Ʀ� Regex ����C
        /// </summary>
        /// <param name="elem"></param>
        protected void initUrlPattern(XmlElement elem)
        {
            if (elem.InnerText != null)
            {
                UrlPattern = new Regex("\\{"+elem.InnerText+"\\}");
            }
        }

        /// useless
        /// ��l�� WebPageTurner 
        ///
        //protected void initWebPageTurner(XmlElement elem)
        //{
        //    string sId = elem.InnerText;
        //    WebPageTurner = m_GlobalResourceProvider.getWebPageTurner(sId);
        //}


        /// <summary>
        /// initialize the row content parser specified by elm and stores it to FlowResourcePool.
        /// ��l�� IElementRowContentParser�A�o�Ӫ�l�ƹL�{���A�|�N IElementRowContentParser �[�� FlowResourcePool �����C
        /// </summary>
        /// <param name="elem"></param>
        protected void initRowContentParser(XmlElement elem)
        {
            IElementRowContentParser oOld = null;
            for (int i = 0; i < elem.ChildNodes.Count; i++)
            {
                if (elem.ChildNodes.Item(i).NodeType!=XmlNodeType.Element)
                {
                    continue;
                }

                XmlElement oElem = (XmlElement)elem.ChildNodes.Item(i);
                string sId = oElem.GetAttribute("id");
                IElementRowContentParser oNew = createRowContentParser(oElem);
                if (oNew == null)
                {
                    continue;
                }

                ResourcePool.addRowContentParser(sId, oNew);

                if (oOld != null && !"true".Equals(oElem.GetAttribute("isRoot")))
                {
                    oOld.setNext(oNew);
                }

                oOld = oNew;
            }
        }
        /// <summary>
        /// initialize the document parser specified by elm and stores it to FlowResourcePool.
        /// ��l�� IDocumentParser�A�o�Ӫ�l�ƹL�{���A�|�N IDocumentParser �[�� FlowResourcePool �����C
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        protected IDocumentParser initDocumentParser(XmlElement elem)
        {
            IDocumentParser oOld = null;
            IDocumentParser oFirst = null;
            for (int i = 0; i < elem.ChildNodes.Count; i++)
            {
                if (elem.ChildNodes.Item(i).NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                XmlElement oElem = (XmlElement)elem.ChildNodes.Item(i);
                string sId = oElem.GetAttribute("id");
                IDocumentParser oNew = createDocumentParser(oElem);
                if (oNew == null)
                {
                    continue;
                }

                ResourcePool.addDocumentParser(sId, oNew);

                if (oOld != null)
                {
                    oOld.setNext(oNew);
                }
                else
                {
                    oFirst = oNew;
                }

                oOld = oNew;
            }

            return oFirst;
        }
        /// <summary>
        /// initialize the row filter specified by elm and stores it to FlowResourcePool.
        /// ��l�� IRowFilter�A�o�Ӫ�l�ƹL�{���A�|�N IRowFilter �[�� FlowResourcePool �����C
        /// </summary>
        /// <param name="elem"></param>
        protected void initRowFilter(XmlElement elem)
        {
            for (int i = 0; i < elem.ChildNodes.Count; i++)
            {
                if (elem.ChildNodes.Item(i).NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                XmlElement oElem = (XmlElement)elem.ChildNodes.Item(i);
                string sId = oElem.GetAttribute("id");
                IRowFilter oNew = createRowFilter(oElem);
                if (oNew != null)
                {
                    ResourcePool.addRowFilter(sId, oNew);
                }
            }
        }
        /// <summary>
        /// create a row content parser specified by elem. This method all get an IRowContentParserProvider from
        /// ProviderFactory and use it to do so.
        /// �� XmlElement �ӫإ� IElementRowContentParser �A���|�I�s ProviderFactory �Ө��o IRowContentParserProvider �C
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        private IElementRowContentParser createRowContentParser(XmlElement elem)
        {
            XmlQualifiedName oName = new XmlQualifiedName(elem.LocalName, elem.NamespaceURI);
            IRowContentParserProvider oP = ProviderFactory.Default.getRowContentParserProvider(oName);
            if (oP == null)
            {
                return null;
            }
            oP.setFlowResourceProvider(ResourcePool);
            oP.setGlobalResource(GlobalResourceProvider);
            IElementRowContentParser oParser = oP.createRowContentParser(elem);
            return oParser;
        }
        /// <summary>
        /// create a  document parser specified by elem. This method all get an IDocumentParserProvider from
        /// ProviderFactory and use it to do so.
        /// 
        /// In the creation method of IDocumentParserProvider, IDocumentParser will be filled with row content
        /// parser.
        /// 
        /// �� XmlElement �ӫإ� IDocumentParser �A���|�I�s ProviderFactory �Ө��o IDocumentParserProvider �C
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        private IDocumentParser createDocumentParser(XmlElement elem)
        {
            XmlQualifiedName oName = new XmlQualifiedName(elem.LocalName, elem.NamespaceURI);
            IDocumentParserProvider oP = ProviderFactory.Default.getDocumentParserProvider(oName);
            if (oP == null)
            {
                return null;
            }
            oP.setFlowResourceProvider(ResourcePool);
            oP.setGlobalResource(GlobalResourceProvider);

            IDocumentParser oParser = oP.createDocumentParser(elem);
            return oParser;
        }
        /// <summary>
        /// create a row filter specified by elem. This method all get an IRowFilterProvider from
        /// ProviderFactory and use it to do so.
        /// 
        /// In the creation method of IRowFilterProvider, IRowFilter is hooked to IDocumentParser.
        /// 
        /// �� XmlElement �ӫإ� IRowFilter �A���|�I�s ProviderFactory �Ө��o IRowFilterProvider �C
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        private IRowFilter createRowFilter(XmlElement elem)
        {
            XmlQualifiedName oName = new XmlQualifiedName(elem.LocalName, elem.NamespaceURI);
            IRowFilterProvider oP = ProviderFactory.Default.getRowFilterProvider(oName);
            if (oP == null)
            {
                return null;
            }
            oP.setFlowResourceProvider(ResourcePool);
            oP.setGlobalResource(GlobalResourceProvider);

            IRowFilter oParser = oP.createRowFilter(elem);
            return oParser;
        }

        #endregion

        #region IRuntimeUnit Members
        /// <summary>
        /// sets the global resource.
        /// </summary>
        /// <param name="p"></param>
        public void setGlobalResource(IGlobalResourceProvider p)
        {
            m_GlobalResourceProvider = p;
        }

        #endregion

    }
}
