using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using AxSHDocVw;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// a convenient form for Spider users.
    /// </summary>
    public partial class BasicSpiderForm : Form
    {
        AxWebBrowser m_Browser;
        private bool m_PutBeforeNavigate2 = false;
        private string m_BaseUrl;

        /// <summary>
        /// Web Browser
        /// </summary>
        public AxWebBrowser Browser
        {
            get { return m_Browser; }
            set { m_Browser = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public BasicSpiderForm()
        {
            m_Browser = new AxWebBrowser();
            this.Controls.Add(m_Browser);
            InitializeComponent();
            SpiderUtilities.turnOffJavascriptErrorAndPopup(m_Browser);
        }

        /// <summary>
        /// lock the browser navigation in the baseUrl.
        /// </summary>
        /// <param name="baseUrl"></param>
        protected void LockBrowserIn(string baseUrl)
        {
            m_BaseUrl = baseUrl;
            if (!m_PutBeforeNavigate2)
            {
                Browser.BeforeNavigate2 += new DWebBrowserEvents2_BeforeNavigate2EventHandler(Browser_BeforeNavigate2);
                m_PutBeforeNavigate2 = true;
            }
        }

        /// <summary>
        /// navigate url.
        /// </summary>
        /// <param name="url"></param>
        protected void NavigateUrl(string url)
        {
            SpiderUtilities.navigateUrlWithoutCache(Browser, url);
        }

        void Browser_BeforeNavigate2(object sender, DWebBrowserEvents2_BeforeNavigate2Event e)
        {
            Regex oReg = new Regex(@"(http://[a-zA-Z0-9\.]+:?[0-9]*/)");
            Match oMatch = oReg.Match(m_BaseUrl);
            if (oMatch.Success)
            {
                string sServer = oMatch.Groups[1].Value;
                if (!e.uRL.ToString().StartsWith(sServer) && !e.uRL.Equals("about:blank"))
                {
                    e.cancel = true;
                    SHDocVw.IWebBrowser2 oBrowser = (SHDocVw.IWebBrowser2)e.pDisp;
                    oBrowser.Stop();
                    object oObj = null;
                    oBrowser.Navigate("about:blank", ref oObj, ref oObj, ref oObj, ref oObj);
                }
            }
        }

        private void BasicSpiderForm_Shown(object sender, EventArgs e)
        {
            m_Browser.Silent = true;
        }

        /// <summary>
        /// log message to errlog.txt.
        /// </summary>
        /// <param name="s"></param>
        protected void logErrorText(string s)
        {
            FileInfo oErrLog = new FileInfo("errlog.txt");
            StreamWriter oWriter = oErrLog.AppendText();
            oWriter.WriteLine("[" + DateTime.Now + "] Error data: " + s);
            oWriter.Close();
        }
        /// <summary>
        ///log text to console and ToolStripStatusLabel.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ss">若 ss 為 null ，則不顯示</param>
        protected void logText(string s, ToolStripStatusLabel ss)
        {
            Console.WriteLine("[" + DateTime.Now + "] " + s);
            if (ss != null)
            {
                ss.Text = s;
            }
            Application.DoEvents();
        }
    }
}