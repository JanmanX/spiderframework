using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxSHDocVw;

namespace fox.spider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="browser"></param>
    /// <param name="url"></param>
    public delegate void DocumentCompleted(AxWebBrowser browser, string url);
    [Obsolete("Do not use it.")]
    public partial class WebBrowserForm : Form
    {
        private AxWebBrowser m_WebBrowser;

        public WebBrowserForm()
        {
            
            InitializeComponent();
            initWebBrowser();
        }

        private void initWebBrowser()
        {
            m_WebBrowser = new AxWebBrowser();
            m_WebBrowser.Dock = DockStyle.Fill;
            m_BrowserContainer.Controls.Add(m_WebBrowser);
            m_WebBrowser.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(m_WebBrowser_DocumentComplete);
            m_WebBrowser.StatusTextChange += new DWebBrowserEvents2_StatusTextChangeEventHandler(m_WebBrowser_StatusTextChange);
        }

        void m_WebBrowser_StatusTextChange(object sender, DWebBrowserEvents2_StatusTextChangeEvent e)
        {
            m_StatusText.Text = "Status: " + e.text;
        }

        void m_WebBrowser_DocumentComplete(object sender, DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            m_StatusText.Text = "Document Complete: " + e.uRL.ToString();
            if (DocComing != null)
            {
                DocComing(m_WebBrowser, e.uRL.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public AxWebBrowser Browser
        {
            get
            {
                return m_WebBrowser;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        public void setLabelText(string str)
        {
            m_ReadingLbl.Text = str;
            Application.DoEvents();
        }
        /// <summary>
        /// 
        /// </summary>
        public event DocumentCompleted DocComing;

        
    }
}