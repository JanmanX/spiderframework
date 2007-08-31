using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml;
using fox.spider.runtime.utils;
using fox.spider.runtime.interfaces;
using fox.spider.runtime.constants;
using fox.spider.runtime.providers;
using fox.spider;

namespace fox.spider.runtime
{
    /// <summary>
    /// fired while a spider runtime bumps into an error in parsing period.
    /// </summary>
    /// <param name="sender">sender should be an instance of SpiderRuntime</param>
    /// <param name="url">the url currently parsed</param>
    /// <param name="doc">the HTMLDocument currently parsed</param>
    /// <param name="flow">the spider flow currently executing</param>
    /// <param name="e">exception it meets.</param>
    public delegate void SpiderError(object sender, string url, mshtml.IHTMLDocument2 doc, SpiderFlow flow, Exception e);

    /// <summary>
    /// fired while a spider runtime bumps into an error in turning next page period.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="url"></param>
    /// <param name="doc"></param>
    /// <param name="turner">the web page turner</param>
    /// <param name="pattern">the first url pattern matched to this url</param>
    /// <param name="fromurl">the url current displayed</param>
    /// <param name="e">exception</param>
    public delegate void SpiderPagingError(object sender, string url, mshtml.IHTMLDocument2 doc, IWebPageTurner turner, Regex pattern, string fromurl, Exception e);

    /// <summary>
    /// fired before parsing html document.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="url"></param>
    /// <param name="doc"></param>
    /// <returns></returns>
    public delegate bool BeforeSpiderParse(object sender, string url, mshtml.IHTMLDocument2 doc);

    /// <summary>
    /// fired while a spider flow matched this url.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="url"></param>
    /// <param name="doc"></param>
    /// <param name="flow"></param>
    /// <returns></returns>
    public delegate bool SpiderUrlMatched(object sender, string url, mshtml.IHTMLDocument2 doc, SpiderFlow flow);

    /// <summary>
    /// fired after document parsing. This event may be fired while there is no spider flow matched or parsed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="url"></param>
    /// <param name="doc"></param>
    /// <param name="parsed"></param>
    /// <param name="matched"></param>
    public delegate void SpiderParsed(object sender, string url, mshtml.IHTMLDocument2 doc, bool parsed, bool matched);

    /// <summary>
    /// fired after a matched spider flow parsed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="url"></param>
    /// <param name="flow"></param>
    /// <param name="doc"></param>
    public delegate void SpiderFlowParsed(object sender, string url, SpiderFlow flow, mshtml.IHTMLDocument2 doc);

    /// <summary>
    /// fired after page turner turned.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="turner"></param>
    /// <param name="pattern"></param>
    /// <param name="fromurl"></param>
    /// <param name="success"></param>
    public delegate void SpiderChangePageResult(object sender, IWebPageTurner turner, Regex pattern,string fromurl, bool success);

    /// <summary>
    /// fired after a spider runtime is ready to go.
    /// </summary>
    /// <param name="sender"></param>
    public delegate void SpiderReady(object sender);
    
    /// <summary>
    /// fired when there is a spider flow matched but not parsed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="url"></param>
    /// <param name="doc"></param>
    public delegate void SpiderUrlMatchedButNotParsed(object sender, string url, mshtml.IHTMLDocument2 doc);

    /// <summary>
    /// A SpiderRuntime controls a Web Browser. When this web browser navigates a page, spider runtime checks if there is any
    /// spider flow matching url. If yes, spider runtime use these spider flows to parse this page. After parsing, 
    /// spider runtime check if there is any web page turner matching url. If yes, spider runtime use the first matched web
    /// page turner to turn to next page.
    /// 
    /// SpiderRuntime �|����@�� WebBrowser �A�� WebBrowser �s����@�ӭ�������A���|�D�ʦa�h�d�߬O�_���ŦX�� SpiderFlow�A
    /// �öi���ƪ����R�C
    /// </summary>
    public class SpiderRuntime
    {
        private string m_Name;
        private string m_Id;
        private XmlElement m_Config;
        private IWebPageTurner m_FirstPageTurner;
        private GlobalResourcePool m_ResourcePool=new GlobalResourcePool();
        private List<SpiderFlow> m_SpiderFlowList = new List<SpiderFlow>();
        private List<PageTurnerHolder> m_PageTurnerList = new List<PageTurnerHolder>();
        private bool m_TestingBrowser = false;
        private bool m_Ready = false;
        private bool m_Running = false;

        #region Properties
        /// <summary>
        /// is this runtime running.
        /// �O�_���b���椤
        /// </summary>
        public bool Running
        {
            get { return m_Running; }
            set { m_Running = value; }
        }

        /// <summary>
        /// get global resource pool.
        /// ���o GlobalResourcePool
        /// </summary>
        protected GlobalResourcePool ResourcePool
        {
            get { return m_ResourcePool; }
            set { m_ResourcePool = value; }
        }
        /// <summary>
        /// get global resouce provider.
        /// ���o GlobalResourceProvider
        /// </summary>
        public IGlobalResourceProvider ResourceProvider
        {
            get { return m_ResourcePool; }
        }
        /// <summary>
        /// is ready to go. Spider runtime will do a test to make sure this blowser is correct, ie if navigating 
        /// "about:blank" is correct. After this check, spider runtime will set this property to true.
        /// SpiderRuntime �O�_�������� WebBrowser �A���|�g�L�@�������աA�o�Ӵ��շ|�� WebBrowser �s�� about:blank �����}�C
        /// </summary>
        public bool Ready
        {
            get { return m_Ready; }
            set { m_Ready = value; }
        }
        
        /// <summary>
        /// the name of this spider runtime.
        /// SpiderRuntine ���W��
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        /// <summary>
        /// the id of this spider runtime.
        /// SpiderRuntine �� id
        /// </summary>
        public string Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        /// <summary>
        /// the xml configuration of this spider runtime. This xml element should be "Spider".
        /// SpiderRuntine �� XmlElement �Ѽ�
        /// </summary>
        protected XmlElement Config
        {
            get { return m_Config; }
            set { m_Config = value; }
        }
        #endregion

        #region Events
        /// <summary>
        /// fired while a spider runtime bumps into an error in parsing period.
        /// �� Parse HTML Document ���ɭԡA�p�G�X�{ Exception �ASpiderRuntine �|�e�X�o�Өƥ�C
        /// </summary>
        public event SpiderError ParsingError;
        /// <summary>
        /// fired before parsing html document.
        /// �b Parse �@�� HTML Document �ASpiderRuntine �|�o�X�o�Өƥ�A�p�G�ƥ�B�z�{��ı�o�o�ӭ��������n�B�z�A�h�^�� false �C
        /// </summary>
        public event BeforeSpiderParse BeforeParse;
        /// <summary>
        /// fired after document parsing. This event may be fired while there is no spider flow matched or parsed.
        /// �p�G������@�� SpiderFlow ���R�L�Y�� HTML Document �ASpiderRuntine �|�o�X�o�Өƥ�C
        /// </summary>
        public event SpiderParsed Parsed;
        /// <summary>
        /// fired after page turner turned.
        /// ��o�� SpiderRuntine ���� SpiderFlow ��½�������ɭԡA�C��½�����|�o�X�o�Өƥ�C
        /// </summary>
        public event SpiderChangePageResult ChangePageResult;
        /// <summary>
        /// fired after a matched spider flow parsed.
        /// ���@�� SpiderFlow ����a���R�L HTML Document �A SpiderRuntine �|�o�X�o�Өƥ�C
        /// </summary>
        public event SpiderFlowParsed FlowParsed;
        /// <summary>
        /// fired after a spider runtime is ready to go.
        /// �� SpiderRuntine �ۻ{���౱��� WebBrowser ��A�N�|�o�X�o�Өƥ�C�Ҧ����ʧ@�A�������b�o�Өƥ�o�ͫ����C
        /// </summary>
        public event SpiderReady ReadyForStart;
        /// <summary>
        /// fired while a spider flow matched this url.
        /// ���Y�� SpiderFlow ����B�z�Y�� HTML Document �ɡA�N�|�o�X�o�Өƥ�A�p�G�ƥ�B�z��ı�o SpiderFlow �����n�B�z�A�h�^�� false �C
        /// </summary>
        public event SpiderUrlMatched UrlMatched;
        /// <summary>
        /// fired when there is a spider flow matched but not parsed.
        /// �� SpiderFlow ��B�z HTML Document �ɡA�B�S������@�� SpiderFlow �������a���R�o�� HTML Document �ɡA�|�o�X�o�Өƥ�C
        /// </summary>
        public event SpiderUrlMatchedButNotParsed ParseFailOnMatchedUrl;
        /// <summary>
        /// fired while a spider runtime bumps into an error in turning next page period.
        /// �� SpiderRuntine �i��½�U�@���ɡA�o�� exception ��A�N�|�o�X�o�Өƥ�C
        /// </summary>
        public event SpiderPagingError PagingError;

        #endregion

        public SpiderRuntime(XmlElement config)
        {
            Config = config;

        }

        #region Main Methods
        /// <summary>
        /// start the execution of spider runtime. Spider runtime uses /Spider/PageTurners/@starter to get the starting web page 
        /// turner.
        /// 
        /// This method takes effect only in following conditions accepted:
        /// 1. there is an attribute in /Spider/PageTurners/@starter
        /// 2. spider runtime can find the web page turner specified in @starter.
        /// 
        /// the return value is turing page result. true is turning ok.
        /// 
        /// �}�l SpiderRuntine ������A���|�ϥ� PageTurners/@starter �ҫ��w�� WebPageTurner �Ӷi��Ĥ@����½���C�o�Ӥ�k�u
        /// ���b XmlElement �������w WebPageTurner �� PageTurners/@starter �ݩʮɤ~��B�@�C�䤤 PageTurners/@starter �@�w
        /// �n����������� WebPageTurner �~�঳�����C
        /// </summary>
        /// <returns>�O�_���`�a��� PageTurner �A�P�ɥ��`�a½��</returns>
        public bool start()
        {
            bool bResult = false;
            if (m_FirstPageTurner != null)
            {
                bResult = m_FirstPageTurner.nextPage((mshtml.IHTMLDocument2)ResourcePool.WebBrowser.Document);
            }
            return bResult;
        }
        /// <summary>
        /// initialize spider runtime. This method initialize data tables, relation processors, web page turners, spider flows
        /// and do web browser test.
        /// 
        /// ��l�� SpiderRuntine �A���|��l�� DataTable �B Relation Processor �BWeb Page Turner �B Spider Flow ��
        /// �i�� WebBrowser ����l�ƴ��աC
        /// </summary>
        /// <param name="browser"></param>
        public void init(AxSHDocVw.AxWebBrowser browser)
        {
            if (Config == null)
            {
                throw new Exception("A spider runtime needs a XmlElement configuration node.");
            }
            else if (browser == null)
            {
                throw new Exception("A spider runtime needs a browser.");
            }

            ResourcePool.DataModel = new DataSet();
            ResourcePool.WebBrowser = browser;
            

            Name = Config.GetAttribute("name");
            Id = Config.GetAttribute("id");
            //init tables
            XmlNodeList oTables = Config.GetElementsByTagName("Table", SpiderRuntimeConstants.DefaultNamespace);
            if (oTables.Count == 0)
            {
                throw new Exception("A spider runtime should have at least one table.");
            }
            else
            {
                for (int i = 0; i < oTables.Count; i++)
                {
                    initTable((XmlElement)oTables.Item(i));
                }
            }
            //init relation processor
            XmlNodeList oRelations = Config.GetElementsByTagName("RelationProcessors", SpiderRuntimeConstants.DefaultNamespace);
            if (oRelations.Count > 0)
            {
                initRelationProcessor((XmlElement)oRelations.Item(0));
            }
            //init page turner
            XmlNodeList oTurners = Config.GetElementsByTagName("PageTurners", SpiderRuntimeConstants.DefaultNamespace);
            if (oTurners.Count > 0)
            {
                initPageTurner((XmlElement)oTurners.Item(0));
            }
            //init spider flow
            XmlNodeList oSpiderFlows = Config.GetElementsByTagName("SpiderFlow", SpiderRuntimeConstants.DefaultNamespace);
            if (oTables.Count == 0)
            {
                throw new Exception("A spider runtime should have at least one spider flow.");
            }
            else
            {
                for (int i = 0; i < oSpiderFlows.Count; i++)
                {
                    initSpiderFlow((XmlElement)oSpiderFlows.Item(i));
                }
            }

            //init Document Complete event

            ResourcePool.WebBrowser.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(WebBrowser_DocumentComplete);
            m_TestingBrowser = true;
            Ready = false;
            //start test.
            ResourcePool.WebBrowser.Navigate("about:blank");
        }
        
        #endregion

        #region Document Process Flow
        /// <summary>
        /// This is the document complete event listener of web browser. This method has two mode. The first is testing mode which is 
        /// used to test web browser functionality. The other is standard analyzing mode used to analyze html document.
        /// �B�z WebBrowser �� DocumentComplete ���{�ǡC�o�����|������ӼҦ��A��b�i����ժ��ɭԡA���|�N HTML Document �ǵ� 
        /// SpiderFlow �Ӥ��R�C
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebBrowser_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            m_Running = true;
            string sUrl = e.uRL.ToString();
            if (!m_TestingBrowser && Ready)//parsing
            {
                dataBack(sUrl, (mshtml.IHTMLDocument2)((SHDocVw.IWebBrowser2)e.pDisp).Document);
            }

            if (m_TestingBrowser && !Ready)//testing
            {
                m_TestingBrowser = false;
                Ready = true;
                if (ReadyForStart!=null)
                {
                    ReadyForStart(this);
                }
            }
            m_Running = false;
        }
        /// <summary>
        /// the main procedure of parsing html document. The procedure of this method is: dispatching BeforeParse event, 
        /// analyzing HTML Document, dispatching ParseFailOnMatchedUrl if needed, and turning page.
        /// �� HTML Document �^�ǡA�ö}�l���R�C�����y�{�O�G���o�X BeforeParse �ƥ�B���R HTML Document �B�o�X ParseFailOnMatchedUrl 
        /// �ƥ�B�i��½���C
        /// </summary>
        /// <param name="url"></param>
        /// <param name="doc"></param>
        protected void dataBack(string url, mshtml.IHTMLDocument2 doc)
        {
            ///�o�X BeforeParse ���ƥ�
            if (BeforeParse != null)
            {
                if (!BeforeParse(this, url, doc))
                {
                    return;
                }
            }

            bool bMatched = false;
            ///�B�z HTML Document
            bool bParsingResult=parseDocument(url, doc, ref bMatched);
            /// �p�G�� SpiderFlow ����B�z HTML Document �A�B�S������@�� SpiderFlow ���T�a�B�z HTML Document �ɡA
            /// �o�X ParseFailOnMatchedUrl �C
            if (bMatched && !bParsingResult && ParseFailOnMatchedUrl!=null)
            {
                ParseFailOnMatchedUrl(this, url, doc);
            }
            ///½���C
            paging(url, doc);
        }
        /// <summary>
        /// turning page. Spider runtime matches each page turner holder to find the matched one. 
        /// Once found, spider runtime uses the web page turner stored in this holder to turn page.
        /// If this web page turner turns correctly, return ture. Otherwise, spider runtime finds 
        /// next matched page turner. If there is no one matched or turning correctly, return false.
        /// 
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        protected bool paging(string url, mshtml.IHTMLDocument2 doc)
        {

            ///match all PageTurnerHolder
            foreach (PageTurnerHolder holder in m_PageTurnerList)
            {
                try
                {
                    ///check if this PageTurnerHolder matches this url
                    if (holder.UrlPattern == null || holder.UrlPattern.IsMatch("{" + url + "}"))
                    {
                        if (holder.Delay > 0)
                        {
                            System.Threading.Thread.Sleep(holder.Delay);
                        }

                        ///turning page
                        bool bReturn = holder.PageTurner.nextPage(doc);
                        ///dispatches ChangePageResult event
                        if (ChangePageResult != null)
                        {
                            ChangePageResult(this, holder.PageTurner, holder.UrlPattern, url, bReturn);
                        }
                        ///if turning correctly, return true
                        if (bReturn)
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ///if turning error, dispatches PagingError.
                    ///�p�G½���o�� Exception �ɡA�o�X PagingError �����~�C
                    if (PagingError != null)
                    {
                        PagingError(this, url, doc, holder.PageTurner, holder.UrlPattern, url, ex);
                    }
                }
            }
            /// if there is no one matched or turning correctly, return false.
            return false;
        }
        /// <summary>
        /// parse document. Spider runtime finds the matched spider flow. If spider runtime finds, spider runtime use the found one to
        /// parse the html document. If an error occurs while spider flow parsing, spider runtime will catch this error and let next 
        /// matched spider flow to parse.
        /// 
        /// This method do the following thing: checks if a spider flow matches url, dispatches UrlMatched event, 
        /// spider flow parses HTML document, dispatches FlowParsed event, update matched variable to true.
        /// 
        /// �� SpiderFlow ���R HTML Document �A�o�Ӥ�k�|�ˬd SpiderFlow �O�_����B�z�o�� HTML Document �A�p�G�S������ SpiderFlow
        /// ����B�z�o�� HTML Document �ɡA�|�^�� false �C�p�G���@�� Spider Flow ����B�z�o�� HTML Document �A�h�|�^�� true �C
        /// 
        /// �o�Ӥ�k���y�{�p�U�G�ˬd Spider Flow �O�_��B�z�o�� HTML Document �A�o�X UrlMatched �ƥ�A�� SpiderFlow �B�z HTMLDocument
        /// �A�o�X FlowParsed ���ƥ�A��s matched �ѼƤΦ^�ǭȬ� true�C
        /// </summary>
        /// <param name="url"></param>
        /// <param name="doc"></param>
        /// <param name="matched">out parameter, returns if any spider flow matches. 
        /// �^�ǰѼơA�O�_������@�� SpiderFlow ����B�z�o�� HTML Document</param>
        /// <returns>returns if any spider parse html document correctly.�O�_�� SpiderFlow ���T�a���R�L HTML Document</returns>
        protected bool parseDocument(string url, mshtml.IHTMLDocument2 doc, ref bool matched)
        {
            bool bParsed = false;
            ///checks all spider flow.
            ///�C�@�� SpiderFlow �����v�O���R HTMLDocument
            foreach (SpiderFlow flow in m_SpiderFlowList)
            {
                try
                {
                    ///check if this spider flow matches this url.
                    ///�ˬd SpiderFlow �O�_����R HTMLDocument
                    if (flow.UrlPattern.IsMatch("{" + url + "}"))
                    {
                        ///dispatches UrlMatched event.
                        ///�o�X UrlMatched �ƥ�
                        if (UrlMatched!=null)
                        {
                            bool bMatchFilter=UrlMatched(this, url, doc, flow);
                            ///if result returnd by UrlMatched event handler is false, discard this spider flow.
                            ///�p�G UrlMatched �ƥ�B�z�����Ʊ� SpiderFlow ���R�o�� HTMLDocument �ɫh���U�@�� SpiderFlow �C
                            if (!bMatchFilter)
                            {
                                continue;
                            }
                        }
                        /// spider flow analyzes HTML document.
                        ///SpiderFlow ���R HTML Document 
                        bool bFlowParsed = flow.run(doc);
                        /// if spider flow analyzes HTML document correctly, dispatches FlowParsed event.
                        ///�p�G SpiderFlow ���T�a���R�L HTML Document �N�o�X FlowParsed �ƥ�C
                        if (bFlowParsed && FlowParsed != null)
                        {
                            FlowParsed(this, url, flow, doc);
                           
                        }
                        /// updates matched and return value to true
                        ///��s matched �Φ^�ǭȬ� true 
                        matched = true;
                        bParsed = bParsed | bFlowParsed;
                    }
                }
                catch (Exception ex)
                {
                    /// if there is an error while parsing, dispatches ParsingError.
                    ///�p�G���R���ɭԵo�Ϳ��~�A�h�o�X ParsingError �ƥ�C
                    if (ParsingError != null)
                    {
                        ParsingError(this,url, doc, flow, ex);
                    }
                }
            }
            /// dispatches Parsed event. 
            ///���׬O�_�� SpiderFlow ���R�L �A���n�o�X Parsed ���ƥ�C
            if (Parsed != null)
            {
                Parsed(this, url, doc, bParsed, matched);
            }
            return bParsed;
        }
        #endregion

        #region Init Methods
        /// <summary>
        /// initialize data tables.
        /// ��l�� DataTable
        /// </summary>
        /// <param name="elem"></param>
        protected void initTable(XmlElement elem)
        {
            XmlQualifiedName oName = new XmlQualifiedName(elem.LocalName, elem.NamespaceURI);
            ITableProvider oP = ProviderFactory.Default.getTableProvider(oName);
            if (oP == null)
            {
                return;
            }
            oP.setGlobalResource(ResourcePool);
            DataTable oTable=oP.createTable(elem);
            ResourcePool.DataModel.Tables.Add(oTable);
        }
        /// <summary>
        /// initialize relation processors
        /// ��l�� RelationProcessor
        /// </summary>
        /// <param name="elem"></param>
        protected void initRelationProcessor(XmlElement elem)
        {
            for (int i = 0; i < elem.ChildNodes.Count; i++)
            {
                if (elem.ChildNodes.Item(i).NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                XmlElement oElem = (XmlElement)elem.ChildNodes.Item(i);
                string sId = oElem.GetAttribute("id");
                IRelationProcessor oNew = createRelationProcessor(oElem);
                if (oNew != null)
                {
                    ResourcePool.addRelationProcessor(sId, oNew);
                }
            }
        }
        /// <summary>
        /// initialize web page turners
        /// ��l�� WebPageTurner
        /// </summary>
        /// <param name="elem"></param>
        protected void initPageTurner(XmlElement elem)
        {
            for (int i = 0; i < elem.ChildNodes.Count; i++)
            {
                if (elem.ChildNodes.Item(i).NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement oElem = (XmlElement)elem.ChildNodes.Item(i);
                string sId = oElem.GetAttribute("id");
                PageTurnerHolder oHolder = new PageTurnerHolder(oElem, ResourcePool);
                if (oHolder.PageTurner != null)
                {
                    ResourcePool.addWebPageTurner(sId, oHolder.PageTurner);
                    m_PageTurnerList.Add(oHolder);
                }

                /*
                IWebPageTurner oNew = createWebPageTurner(oElem);
                if (oNew != null)
                {
                    ResourcePool.addWebPageTurner(sId, oNew);
                    m_PageTurnerList.Add(oNew);
                }*/
            }

            m_FirstPageTurner = ResourcePool.getWebPageTurner(elem.GetAttribute("starter"));
        }
        /// <summary>
        /// initialize spider flows
        /// ��l�� SpiderFlow
        /// </summary>
        /// <param name="elem"></param>
        protected void initSpiderFlow(XmlElement elem)
        {
            SpiderFlow oFlow = new SpiderFlow(elem);
            oFlow.setGlobalResource(ResourcePool);
            oFlow.init();
            m_SpiderFlowList.Add(oFlow);
        }
        /// <summary>
        /// create realtion processor by IRelationProcessor which is getting from ProviderFactory
        /// �إ� RelationProcessor
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        private IRelationProcessor createRelationProcessor(XmlElement elem)
        {
            XmlQualifiedName oName = new XmlQualifiedName(elem.LocalName, elem.NamespaceURI);
            IRelationProvider oP = ProviderFactory.Default.getRelationProvider(oName);
            if (oP == null)
            {
                return null;
            }
            oP.setGlobalResource(ResourcePool);

            IRelationProcessor oParser = oP.createRelationProcessor(elem);
            return oParser;
        }


        #endregion
    }
    
}
