using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using mshtml;
using AxSHDocVw;
using System.Collections;

namespace fox.spider
{
    //target : /BODY/CENTER[0]/TABLE[0]/TBODY[0]/TR[0]/TD[0]/TABLE[0]/TBODY[0]/TR[1]/TD[0]/CENTER[0]/TABLE[0]/TBODY[0]/TR[3]/TD[0]
    /// <summary>
    /// 過度複雜的結構，建議盡量少使用它
    /// 範例程式：
    /// <code>
    ///Hashtable oMapping2 = new Hashtable();
    ///oMapping2.Add(LinkedRowContentParser.LINKPATH, "TD[3]/A[0]");
    ///oMapping2.Add(LinkedRowContentParser.TARGETPATH, "/BODY/CENTER[0]/TABLE[0]/TBODY[0]/TR[0]/TD[0]/TABLE[0]/TBODY[0]/TR[1]/TD[0]/CENTER[0]/TABLE[0]/TBODY[0]/TR[3]/TD[0]");
    ///oMapping2.Add(LinkedRowContentParser.FIELD, "內文");    
    /// 
    ///LinkedRowContentParser oContentParser = new LinkedRowContentParser();
    ///oContentParser.ColumnMapping = oMapping2;
    ///oContentParser.RandomSpanEnabled = true;
    ///oRowParser.setNext(oContentParser);
    /// </code>
    /// 以上的程式會從 TD[3]/A[0] 的位置中找到 url 的路徑，然後開啟一個 WebBrowserForm 連結該網址。
    /// 當有資料回傳的時候，它會使用 /BODY/CENTER[0]/TABLE[0]... 來找到 node ，並將這個 node 的 innerText
    /// 儲存在『內文』當中。
    /// 
    /// 這個程式還有另一個參數，ColumnMapping2，它將會儲存一堆 LinkedRowContentParserParam 物件，並用它來將某個
    /// 資料儲存在 DataRow 當中。這個的功能會跟 ColumnMapping 的功能一樣，但 ColumnMapping 只能將資料儲存到一個
    /// 欄位之中；ColumnMapping2 會將多個資料儲存到多個欄位之中。
    /// 
    /// LinkedRowContentParser 因為會開另一個 Browser ，所以可以有另一個 Document Parser 負責 parse 開啟後
    /// 的 web browser 。
    /// </summary>
    [Obsolete("Too complex class. You shouldn't use it.")]
    public class LinkedRowContentParser : ElementRowContentParser
    {
        /// <summary>
        /// 
        /// </summary>
        public const string LINKPATH = "LINKPATH";
        /// <summary>
        /// 
        /// </summary>
        public const string TARGETPATH = "TARGETPATH";
        /// <summary>
        /// 
        /// </summary>
        public const string TARGETPATHS = "TARGETPATHS";
        /// <summary>
        /// 
        /// </summary>
        public const string FIELD = "FIELD";
        private bool m_Loaded = false;
        private bool m_Loaded2 = false;
        private string m_URL;
        private bool m_RandomSpanEnabled = false;
        private IHTMLDOMNode m_Node;
        private IDocumentParser m_Child;
        private WebBrowserForm m_Form;
        private long iRoundCount;
        private bool m_TimesUP = false;
        private bool m_IsParsed = false;
        private ArrayList m_ColumnMapping2;
        /// <summary>
        /// 
        /// </summary>
        public ArrayList ColumnMapping2
        {
            get { return m_ColumnMapping2; }
            set { m_ColumnMapping2 = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public IDocumentParser ChildDocumentParser
        {
            get { return m_Child; }
            set { m_Child = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool RandomSpanEnabled
        {
            get { return m_RandomSpanEnabled; }
            set { m_RandomSpanEnabled = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        protected override void parseContent(mshtml.IHTMLDOMNode row)
        {
            string sPath = (string)ColumnMapping[LINKPATH];
            m_Node = SpiderPath.selectSingleNode(row, sPath);
            if (m_Node == null)
                return;

            m_Loaded = false;
            m_URL = (string)((IHTMLElement)m_Node).getAttribute("HREF", 0);


            Thread oCaptureThread = new Thread(new ThreadStart(fetch));
            oCaptureThread.SetApartmentState(ApartmentState.STA);
            oCaptureThread.Start();
            m_TimesUP = false;
            m_IsParsed = false;
            while (!m_Loaded)
            {
                Thread.Sleep(1000);
                System.Windows.Forms.Application.DoEvents();
                iRoundCount++;
                Console.WriteLine("Round: " + iRoundCount);
                if (iRoundCount > 6000)
                {
                    iRoundCount = 0;
                    m_TimesUP = true;
                    /*oCaptureThread.Abort();
                    if (m_Form != null)
                    {
                        m_Form.Dispose();
                    }*/
                }
            }

            iRoundCount = 0;
            m_URL = null;
        }

        void fetch()
        {
            if (m_RandomSpanEnabled)
                Thread.Sleep((new Random(System.Environment.TickCount)).Next(3000, 8000));
            else
                Thread.Sleep(3000);

            /*            AppDomainSetup oAppSetup = new AppDomainSetup();
                        oAppSetup.ApplicationBase = Application.StartupPath;
                        oAppSetup.PrivateBinPath = Application.StartupPath;
                        oAppSetup.ApplicationName = "IEHost";

                        AppDomain oIEDomain = System.AppDomain.CreateDomain("IE", null, oAppSetup);*/


            WebBrowserForm oForm = new WebBrowserForm();
            //WebBrowserForm oForm = (WebBrowserForm)oIEDomain.CreateInstanceFromAndUnwrap("ExtractorUI.exe", "ExtractorUI.WebBrowserForm");
            oForm.DocComing += new DocumentCompleted(oForm_DocComing);
            //oForm.Browser.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(Browser_DocumentComplete);
            //oForm.Browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(Browser_DocumentCompleted);
            oForm.Show();
            oForm.setLabelText("Waiting... Reading Info from:\r\n" + m_URL);
            Console.WriteLine("URL: " + m_URL);
            m_Form = oForm;
            m_Loaded2 = false;
            oForm.Browser.Navigate(m_URL);
            while (!m_Loaded2)
            {
                Thread.Sleep(500);
                System.Windows.Forms.Application.DoEvents();
                if (m_TimesUP)
                {
                    m_Form.Browser.Stop();
                    processDocument((IHTMLDocument2)m_Form.Browser.Document);
                    m_Loaded2 = true;
                }
            }
            oForm.Dispose();

            //AppDomain.Unload(oIEDomain);
            System.GC.ReRegisterForFinalize(oForm);
            //System.GC.ReRegisterForFinalize(oIEDomain);
            System.GC.WaitForPendingFinalizers();
            m_Form = null;
            m_Loaded = true;
        }

        void oForm_DocComing(AxWebBrowser browser, string url)
        {
            Console.WriteLine(url);
            if (!m_IsParsed)
                processDocument((IHTMLDocument2)browser.Document);
        }

        void Browser_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            Console.WriteLine(e.uRL.ToString());
            if (!m_IsParsed)
                processDocument((IHTMLDocument2)((AxWebBrowser)sender).Document);
        }
/// <summary>
/// 
/// </summary>
/// <param name="document"></param>
        protected void fetchSinglePath(IHTMLDocument2 document)
        {
            string sPath = (string)ColumnMapping[TARGETPATH];
            IHTMLDOMNode oNode = SpiderPath.selectSingleNode(document, sPath);

            if (oNode == null)
            {
                return;
            }

            string sField = (string)ColumnMapping[FIELD];
            if (oNode != null && sField != "")
                getDataRow()[sField] = ((IHTMLElement)oNode).innerText;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        protected void fetchMultiplePath(IHTMLDocument2 document)
        {
            System.Collections.ArrayList oList = (System.Collections.ArrayList)ColumnMapping[TARGETPATHS];
            foreach (string sKey in oList)
            {
                IHTMLDOMNode oNode = SpiderPath.selectSingleNode(document, sKey);

                if (oNode != null)
                {
                    string sField = ((string)ColumnMapping[FIELD]).Trim();
                    string sValue = ((IHTMLElement)oNode).innerText;
                    if (sField != "" && sValue != null && sValue.Trim() != "")
                    {
                        getDataRow()[sField] = sValue.Trim();
                        return;
                    }

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="path"></param>
        /// <param name="field"></param>
        protected void fetchItem(IHTMLDocument2 document, ArrayList path, string field)
        {
            foreach (string sKey in path)
            {
                IHTMLDOMNode oNode = SpiderPath.selectSingleNode(document, sKey);

                if (oNode != null)
                {
                    string sField = field.Trim();
                    string sValue = ((IHTMLElement)oNode).innerText;
                    if (sField != "" && sValue != null && sValue.Trim() != "")
                    {
                        getDataRow()[sField] = sValue.Trim();
                        return;
                    }

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        protected void fetchOthers(IHTMLDocument2 document)
        {
            foreach (LinkedRowContentParserParam oParam in ColumnMapping2)
            {
                if (oParam.Path != null && oParam.Field != null)
                {
                    fetchItem(document, oParam.Path, oParam.Field);
                }
            }
        }

        void processDocument(IHTMLDocument2 document)
        {
            m_Form.setLabelText("Data received and are analying now......");


            try
            {
                if (ColumnMapping != null)
                {
                    if (ColumnMapping[TARGETPATHS] != null)
                    {
                        fetchMultiplePath(document);
                    }
                    else if (ColumnMapping[TARGETPATH] != null)
                    {
                        fetchSinglePath(document);
                    }
                }

                if (ColumnMapping2 != null)
                {
                    fetchOthers(document);
                }

                if (ChildDocumentParser != null)
                {
                    m_Form.setLabelText("Initiate document processors......");
                    if (ChildDocumentParser.getRelationProcessor() != null)
                    {
                        ChildDocumentParser.getRelationProcessor().setContextNode(m_Node);
                        ChildDocumentParser.getRelationProcessor().setContextRow(getDataRow());
                    }

                    ChildDocumentParser.parse(document);
                    m_Form.setLabelText("Document processors parsed...");
                }
                m_IsParsed = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            m_Loaded2 = true;
        }

    }

    class LinkedRowContentParserParam
    {
        /// <summary>
        /// 
        /// </summary>
        public System.Collections.ArrayList Path;
        /// <summary>
        /// 
        /// </summary>
        public string Field;
    }
}
