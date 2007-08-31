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
    /// SpiderFlow 是 SpiderRuntime 中用來分析網頁的一堆 flow ，它是多個 IDocumentParser 的綜合體。
    /// 在初始階段，它會將全部的 IDocumentParser 給初始化出來，同時也會把 IElementRowContent 給接上
    /// IDocumentParser 之中。
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
        /// 這個 SpiderFlow 的名稱
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
       /// <summary>
       /// The id of this spider flow.
       /// 這個 SpiderFlow 的 id ，它將會是 XmlElement 中指定的 id 。
       /// </summary>
        public string Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }
        /// <summary>
        /// The url pattern of this spider flow. This pattern is used to exam which web page should be 
        /// analyzed by this flow.
        /// 這個 SpiderFlow 負責處理的 UrlPattern ，SpiderRuntime 會使用這個 Regex 來判斷這個 SpiderFlow 是否要執行。
        /// </summary>
        public Regex UrlPattern
        {
            get { return m_UrlPattern; }
            set { m_UrlPattern = value; }
        }
        /// <summary>
        /// An XmlElement to initialize this spider flow.
        /// 這個 SpiderFlow 的 XmlElement 參數。
        /// </summary>
        protected XmlElement Element
        {
            get { return m_Element; }
            set { m_Element = value; }
        }

        /// 
        /// we don't turn any page here.
        /// 這個 SpiderFlow 處理完成後會用到的 IWebPageTurner ，這個屬性可能會是 null 。
        ///
        //public IWebPageTurner WebPageTurner
        //{
        //    get { return m_WebPageTurner; }
        //    set { m_WebPageTurner = value; }
        //}

        /// <summary>
        /// a global resource
        /// 取得 IGlobalResourceProvider
        /// </summary>
        protected IGlobalResourceProvider GlobalResourceProvider
        {
            get { return m_GlobalResourceProvider; }
            set { m_GlobalResourceProvider = value; }
        }
        /// <summary>
        /// a flow resource stored in this flow.
        /// 這個 SpiderFlow 的 FlowResourcePool 。
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
        /// 初始化整個 Spider Flow，它會讀取 XmlElement 中的 Name、Id、UrlPattern，並把這些物件初始化到屬性之中。
        /// 另外，它會先初始化 RowContentParser ，接下來初始化 IDocumentParser ，接下來再初始化 IRowFilter 。
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
            //init DocumentParsers, 在初始化 DocumentParser 之前，必須先初始化好 RowContentParser
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
            //init RowFilter, 在初始化 RowFilter 之前，必須先初始化好 Document Parser
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
        /// 執行 Spider Flow 的分析工作
        /// </summary>
        /// <param name="doc"></param>
        public bool run(IHTMLDocument2 doc)
        {
            return m_TopParser.parse(doc);
        }
        /// <summary>
        /// init url pattern. this method converts inner text of elem to Regex object.
        /// 初始化 Urlpattern ，將 XmlElement 中的參數初始化成 Regex 物件。
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
        /// 初始化 WebPageTurner 
        ///
        //protected void initWebPageTurner(XmlElement elem)
        //{
        //    string sId = elem.InnerText;
        //    WebPageTurner = m_GlobalResourceProvider.getWebPageTurner(sId);
        //}


        /// <summary>
        /// initialize the row content parser specified by elm and stores it to FlowResourcePool.
        /// 初始化 IElementRowContentParser，這個初始化過程中，會將 IElementRowContentParser 加到 FlowResourcePool 之中。
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
        /// 初始化 IDocumentParser，這個初始化過程中，會將 IDocumentParser 加到 FlowResourcePool 之中。
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
        /// 初始化 IRowFilter，這個初始化過程中，會將 IRowFilter 加到 FlowResourcePool 之中。
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
        /// 用 XmlElement 來建立 IElementRowContentParser ，它會呼叫 ProviderFactory 來取得 IRowContentParserProvider 。
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
        /// 用 XmlElement 來建立 IDocumentParser ，它會呼叫 ProviderFactory 來取得 IDocumentParserProvider 。
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
        /// 用 XmlElement 來建立 IRowFilter ，它會呼叫 ProviderFactory 來取得 IRowFilterProvider 。
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
