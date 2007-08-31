using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxSHDocVw;
using mshtml;

namespace fox.spider.runtime.window
{
    public partial class TestForm : Form
    {
        private AxWebBrowser m_Browser;
        public TestForm()
        {
            InitializeComponent();
            m_Browser = new AxWebBrowser();
            m_Browser.Dock = DockStyle.Fill;
            m_Browser.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(m_Browser_DocumentComplete);
            this.Controls.Add(m_Browser);
            m_Browser.BringToFront();
            
            //m_Browser.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(m_Browser_DocumentComplete);
            
        }

        void m_Browser_DocumentComplete(object sender, DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            SHDocVw.IWebBrowser oBrowser = (SHDocVw.IWebBrowser)e.pDisp;
            if (oBrowser.Document.Equals(m_Browser.Document))
            {
                button1_Click(this, null);
            }
            //IHTMLDocument2 oDoc = (IHTMLDocument2)oBrowser.Document;
            ////oDoc.body.setAttribute("SCROLL", "no", 0);
            //oDoc.body.style.overflow = "hidden";
            toolStripStatusLabel1.Text = e.uRL.ToString();
            ////IHTMLDocument2 oDoc=(IHTMLDocument2) m_Browser.Document;
            //oDoc.body.setAttribute("SCROLL", "no", 1);
            //IHTMLDocument3 oDoc3 = (IHTMLDocument3)m_Browser.Document;
            //oDoc3.documentElement.setAttribute("SCROLL", "no", 1);
            //MessageBox.Show(oDoc.body.getAttribute("SCROLL", 0).ToString());
            //button1_Click(this, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap oMap = fox.spider.screenshot.WebCamera.takeBrowserScreen(m_Browser, null);
            oMap.Save("C:\\Test.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void TestForm_Shown(object sender, EventArgs e)
        {
            m_Browser.Navigate("http://tw.page.bid.yahoo.com/tw/auction/1155536384");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            IHTMLDocument2 oDoc = (IHTMLDocument2)m_Browser.Document;
            IHTMLDocument3 oDoc3 = (IHTMLDocument3)m_Browser.Document;
            oDoc.body.setAttribute("SCROLL", "no", 0);
            oDoc.body.style.overflow = "hidden";

            oDoc3.documentElement.setAttribute("SCROLL", "no", 0);
        }
    }
}