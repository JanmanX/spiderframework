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
    /// SpiderRuntime 會控制一個 WebBrowser ，當 WebBrowser 瀏覽到一個頁面之後，它會主動地去查詢是否有符合的 SpiderFlow，
    /// 並進行資料的分析。
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
        /// 是否正在執行中
        /// </summary>
        public bool Running
        {
            get { return m_Running; }
            set { m_Running = value; }
        }

        /// <summary>
        /// get global resource pool.
        /// 取得 GlobalResourcePool
        /// </summary>
        protected GlobalResourcePool ResourcePool
        {
            get { return m_ResourcePool; }
            set { m_ResourcePool = value; }
        }
        /// <summary>
        /// get global resouce provider.
        /// 取得 GlobalResourceProvider
        /// </summary>
        public IGlobalResourceProvider ResourceProvider
        {
            get { return m_ResourcePool; }
        }
        /// <summary>
        /// is ready to go. Spider runtime will do a test to make sure this blowser is correct, ie if navigating 
        /// "about:blank" is correct. After this check, spider runtime will set this property to true.
        /// SpiderRuntime 是否完全控制 WebBrowser ，它會經過一次的測試，這個測試會讓 WebBrowser 瀏覽 about:blank 的網址。
        /// </summary>
        public bool Ready
        {
            get { return m_Ready; }
            set { m_Ready = value; }
        }
        
        /// <summary>
        /// the name of this spider runtime.
        /// SpiderRuntine 的名稱
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        /// <summary>
        /// the id of this spider runtime.
        /// SpiderRuntine 的 id
        /// </summary>
        public string Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        /// <summary>
        /// the xml configuration of this spider runtime. This xml element should be "Spider".
        /// SpiderRuntine 的 XmlElement 參數
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
        /// 當 Parse HTML Document 的時候，如果出現 Exception ，SpiderRuntine 會送出這個事件。
        /// </summary>
        public event SpiderError ParsingError;
        /// <summary>
        /// fired before parsing html document.
        /// 在 Parse 一個 HTML Document ，SpiderRuntine 會發出這個事件，如果事件處理程式覺得這個頁面不須要處理，則回傳 false 。
        /// </summary>
        public event BeforeSpiderParse BeforeParse;
        /// <summary>
        /// fired after document parsing. This event may be fired while there is no spider flow matched or parsed.
        /// 如果有任何一個 SpiderFlow 分析過某個 HTML Document ，SpiderRuntine 會發出這個事件。
        /// </summary>
        public event SpiderParsed Parsed;
        /// <summary>
        /// fired after page turner turned.
        /// 當這個 SpiderRuntine 中的 SpiderFlow 有翻頁器的時候，每次翻頁都會發出這個事件。
        /// </summary>
        public event SpiderChangePageResult ChangePageResult;
        /// <summary>
        /// fired after a matched spider flow parsed.
        /// 有一個 SpiderFlow 完整地分析過 HTML Document ， SpiderRuntine 會發出這個事件。
        /// </summary>
        public event SpiderFlowParsed FlowParsed;
        /// <summary>
        /// fired after a spider runtime is ready to go.
        /// 當 SpiderRuntine 自認為能控制住 WebBrowser 後，就會發出這個事件。所有的動作，都必須在這個事件發生後執行。
        /// </summary>
        public event SpiderReady ReadyForStart;
        /// <summary>
        /// fired while a spider flow matched this url.
        /// 當有某個 SpiderFlow 能夠處理某個 HTML Document 時，就會發出這個事件，如果事件處理器覺得 SpiderFlow 不須要處理，則回傳 false 。
        /// </summary>
        public event SpiderUrlMatched UrlMatched;
        /// <summary>
        /// fired when there is a spider flow matched but not parsed.
        /// 當有 SpiderFlow 能處理 HTML Document 時，且沒有任何一個 SpiderFlow 能夠完整地分析這個 HTML Document 時，會發出這個事件。
        /// </summary>
        public event SpiderUrlMatchedButNotParsed ParseFailOnMatchedUrl;
        /// <summary>
        /// fired while a spider runtime bumps into an error in turning next page period.
        /// 當 SpiderRuntine 進行翻下一頁時，發生 exception 後，就會發出這個事件。
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
        /// 開始 SpiderRuntine 的執行，它會使用 PageTurners/@starter 所指定的 WebPageTurner 來進行第一次的翻頁。這個方法只
        /// 有在 XmlElement 中有指定 WebPageTurner 及 PageTurners/@starter 屬性時才能運作。其中 PageTurners/@starter 一定
        /// 要能夠找到對應的 WebPageTurner 才能有反應。
        /// </summary>
        /// <returns>是否正常地找到 PageTurner ，同時正常地翻頁</returns>
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
        /// 初始化 SpiderRuntine ，它會初始化 DataTable 、 Relation Processor 、Web Page Turner 、 Spider Flow 及
        /// 進行 WebBrowser 的初始化測試。
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
        /// 處理 WebBrowser 的 DocumentComplete 的程序。這部份會分成兩個模式，當在進行測試的時候，不會將 HTML Document 傳給 
        /// SpiderFlow 來分析。
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
        /// 有 HTML Document 回傳，並開始分析。它的流程是：先發出 BeforeParse 事件、分析 HTML Document 、發出 ParseFailOnMatchedUrl 
        /// 事件、進行翻頁。
        /// </summary>
        /// <param name="url"></param>
        /// <param name="doc"></param>
        protected void dataBack(string url, mshtml.IHTMLDocument2 doc)
        {
            ///發出 BeforeParse 的事件
            if (BeforeParse != null)
            {
                if (!BeforeParse(this, url, doc))
                {
                    return;
                }
            }

            bool bMatched = false;
            ///處理 HTML Document
            bool bParsingResult=parseDocument(url, doc, ref bMatched);
            /// 如果有 SpiderFlow 能夠處理 HTML Document ，且沒有任何一個 SpiderFlow 正確地處理 HTML Document 時，
            /// 發出 ParseFailOnMatchedUrl 。
            if (bMatched && !bParsingResult && ParseFailOnMatchedUrl!=null)
            {
                ParseFailOnMatchedUrl(this, url, doc);
            }
            ///翻頁。
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
                    ///如果翻頁發生 Exception 時，發出 PagingError 的錯誤。
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
        /// 讓 SpiderFlow 分析 HTML Document ，這個方法會檢查 SpiderFlow 是否能夠處理這個 HTML Document ，如果沒有任何 SpiderFlow
        /// 能夠處理這個 HTML Document 時，會回傳 false 。如果有一個 Spider Flow 能夠處理這個 HTML Document ，則會回傳 true 。
        /// 
        /// 這個方法的流程如下：檢查 Spider Flow 是否能處理這個 HTML Document ，發出 UrlMatched 事件，由 SpiderFlow 處理 HTMLDocument
        /// ，發出 FlowParsed 的事件，更新 matched 參數及回傳值為 true。
        /// </summary>
        /// <param name="url"></param>
        /// <param name="doc"></param>
        /// <param name="matched">out parameter, returns if any spider flow matches. 
        /// 回傳參數，是否有任何一個 SpiderFlow 能夠處理這個 HTML Document</param>
        /// <returns>returns if any spider parse html document correctly.是否有 SpiderFlow 正確地分析過 HTML Document</returns>
        protected bool parseDocument(string url, mshtml.IHTMLDocument2 doc, ref bool matched)
        {
            bool bParsed = false;
            ///checks all spider flow.
            ///每一個 SpiderFlow 都有權力分析 HTMLDocument
            foreach (SpiderFlow flow in m_SpiderFlowList)
            {
                try
                {
                    ///check if this spider flow matches this url.
                    ///檢查 SpiderFlow 是否能分析 HTMLDocument
                    if (flow.UrlPattern.IsMatch("{" + url + "}"))
                    {
                        ///dispatches UrlMatched event.
                        ///發出 UrlMatched 事件
                        if (UrlMatched!=null)
                        {
                            bool bMatchFilter=UrlMatched(this, url, doc, flow);
                            ///if result returnd by UrlMatched event handler is false, discard this spider flow.
                            ///如果 UrlMatched 事件處理器不希望 SpiderFlow 分析這個 HTMLDocument 時則換下一個 SpiderFlow 。
                            if (!bMatchFilter)
                            {
                                continue;
                            }
                        }
                        /// spider flow analyzes HTML document.
                        ///SpiderFlow 分析 HTML Document 
                        bool bFlowParsed = flow.run(doc);
                        /// if spider flow analyzes HTML document correctly, dispatches FlowParsed event.
                        ///如果 SpiderFlow 正確地分析過 HTML Document 就發出 FlowParsed 事件。
                        if (bFlowParsed && FlowParsed != null)
                        {
                            FlowParsed(this, url, flow, doc);
                           
                        }
                        /// updates matched and return value to true
                        ///更新 matched 及回傳值為 true 
                        matched = true;
                        bParsed = bParsed | bFlowParsed;
                    }
                }
                catch (Exception ex)
                {
                    /// if there is an error while parsing, dispatches ParsingError.
                    ///如果分析的時候發生錯誤，則發出 ParsingError 事件。
                    if (ParsingError != null)
                    {
                        ParsingError(this,url, doc, flow, ex);
                    }
                }
            }
            /// dispatches Parsed event. 
            ///不論是否有 SpiderFlow 分析過 ，都要發出 Parsed 的事件。
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
        /// 初始化 DataTable
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
        /// 初始化 RelationProcessor
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
        /// 初始化 WebPageTurner
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
        /// 初始化 SpiderFlow
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
        /// 建立 RelationProcessor
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
