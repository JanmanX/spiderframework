using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using fox.spider.runtime;

namespace fox.spider.runtime.window
{
    public partial class MainForm : fox.spider.BasicSpiderForm
    {

        private SpiderRuntime m_Runtime = null;

        public MainForm()
        {
            InitializeComponent();
            this.m_Spliter.Panel2.Controls.Add(Browser);
            Browser.Dock = DockStyle.Fill;
            Browser.BringToFront();

            Browser.BeforeNavigate2 += new AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(Browser_BeforeNavigate2);
            Browser.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(Browser_DocumentComplete);
        }

        void Browser_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            m_StatusLabel.Text = "Document Loaded: " + e.uRL.ToString();
            Application.DoEvents();
        }

        void Browser_BeforeNavigate2(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
        {
            m_StatusLabel.Text = "Navigating: " + e.uRL.ToString();
            Application.DoEvents();
        }

        private void m_BrowseBtn_Click(object sender, EventArgs e)
        {
            if (m_OpenDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            m_Config.Text = m_OpenDialog.FileName;
            m_StartBtn.Enabled = true;
        }

        private void m_NavigateBtn_Click(object sender, EventArgs e)
        {
            if (m_Url.Text != null && !"".Equals(m_Url.Text))
            {
                NavigateUrl(m_Url.Text);
            }
        }

        private void m_StartBtn_Click(object sender, EventArgs e)
        {
            XmlDocument oDocument = new XmlDocument();
            oDocument.Load(m_Config.Text);
            m_Runtime = new SpiderRuntime(oDocument.DocumentElement);
            m_Runtime.init(Browser);
            m_Runtime.ReadyForStart += new SpiderReady(m_Runtime_ReadyForStart);
            m_Runtime.BeforeParse += new BeforeSpiderParse(m_Runtime_BeforeParse);
            m_Runtime.Parsed += new SpiderParsed(m_Runtime_Parsed);
            m_Runtime.ChangePageResult += new SpiderChangePageResult(m_Runtime_ChangePageResult);
        }

        void m_Runtime_ChangePageResult(object sender, IWebPageTurner turner, System.Text.RegularExpressions.Regex pattern,
            string fromurl, bool success)
        {
            m_StatusLabel.Text = "Turn page, " + success + ", from " + fromurl;
            Application.DoEvents();
        }

        void m_Runtime_Parsed(object sender, string url, mshtml.IHTMLDocument2 doc, bool parsed, bool matched)
        {
            if (parsed)
            {
                m_StatusLabel.Text = "Spider parsed: " + url;
                m_SaveXmlBtn.Enabled = true;
            }
            else if(!matched)
            {
                m_StatusLabel.Text = "Spider failed to parse: " + url;
            }
            Application.DoEvents();
        }

        bool m_Runtime_BeforeParse(object sender, string url, mshtml.IHTMLDocument2 doc)
        {
            m_StatusLabel.Text = "Spider start to parse: " + url;
            Application.DoEvents();
            return true;
        }

        void m_Runtime_ReadyForStart(object sender)
        {
            m_Runtime.start();
            m_NavigateBtn.Enabled = true;
        }

        private void m_SaveXmlBtn_Click(object sender, EventArgs e)
        {
            if (m_Runtime.Running)
            {
                m_StatusLabel.Text = "Spider Running.. Please Wait...";
                Application.DoEvents();
            }

            
            m_Runtime.ResourceProvider.getDataSet().WriteXml(Application.StartupPath + "\\out.xml");

        }
    }
}

