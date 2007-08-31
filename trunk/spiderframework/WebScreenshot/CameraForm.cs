using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using AxSHDocVw;
using mshtml;
using ImageQuantization;

namespace fox.spider.screenshot
{
    public delegate void DHTMLEventDelegate(IHTMLEventObj e);

    public partial class CameraForm : Form
    {
        private AxWebBrowser m_Browser, m_AdBrowser;

        private string m_AutoURL = null;
        private string m_SaveLocation = null;

        public string SaveLocation
        {
            get { return m_SaveLocation; }
            set { m_SaveLocation = value; }
        }

        private bool m_AutoRunDone = false;

        public bool AutoRunDone
        {
            get { return m_AutoRunDone; }
            set { m_AutoRunDone = value; }
        }

        public string AutoURL
        {
            get { return m_AutoURL; }
            set { m_AutoURL = value; }
        }

        private bool m_Shareware=false;

        private IHTMLFrameBase2 m_TargetFrame = null;
        private Dictionary<IHTMLWindow2, IHTMLFrameBase2> m_Window2Frame = new Dictionary<IHTMLWindow2, IHTMLFrameBase2>();

        public CameraForm()
        {
            InitializeComponent();
            this.SuspendLayout();
            m_ToolStropContaner.BringToFront();

            m_Browser = new AxWebBrowser();
            m_Browser.Dock = DockStyle.Fill;
            this.m_ToolStropContaner.ContentPanel.Controls.Add(m_Browser);
            m_Browser.BringToFront();
            m_Browser.StatusTextChange += new DWebBrowserEvents2_StatusTextChangeEventHandler(m_Browser_StatusTextChange);
            m_Browser.BeforeNavigate2 += new DWebBrowserEvents2_BeforeNavigate2EventHandler(m_Browser_BeforeNavigate2);
            m_Browser.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(m_Browser_DocumentComplete);

            if (m_Shareware)
            {
                putAdsense();
            }
            this.ResumeLayout();
        }

        private void putAdsense()
        {
            m_AdBrowser = new AxWebBrowser();
            m_AdBrowser.Dock = DockStyle.Bottom;
            //m_AdBrowser.PreferredSize.Width=473;
            //
            m_AdBrowser.MaximumSize = new Size(65535, 85);
            m_AdBrowser.Size = new Size(473, 85);

            this.m_ToolStropContaner.ContentPanel.Controls.Add(m_AdBrowser);
            
            m_Browser.BringToFront();
            this.Refresh();
            //IHTMLDocument2 oDoc=(IHTMLDocument2) m_Browser.Document;
            //if(oDoc.body is HTMLBodyClass){
            //    IHTMLScriptElement oScript1=(IHTMLScriptElement)oDoc.createElement("SCRIPT");
            //    oScript1.text ="google_ad_client = \"pub-7438623638886017\";\r\n";
            //    oScript1.text += "google_ad_width = 468;\r\n";
            //    oScript1.text += "google_ad_height = 60;\r\n";
            //    oScript1.text += "google_ad_format = \"468x60_as\";\r\n";
            //    oScript1.text += "google_ad_type = \"image\";\r\n";
            //    oScript1.text += "google_ad_channel = \"3126992858\";\r\n";
            //    IHTMLElement oScript2=oDoc.createElement("SCRIPT");
            //    oScript2.setAttribute("SRC", "http://pagead2.googlesyndication.com/pagead/show_ads.js", 0);

            //    IHTMLDOMNode oNode = (IHTMLDOMNode)oDoc.body;
            //    IHTMLDOMNode oFirst = oNode.firstChild;
            //    oNode.insertBefore((IHTMLDOMNode)oScript1, oNode.firstChild);
            //    oNode.insertBefore((IHTMLDOMNode)oScript2, oNode.firstChild);
            //}
        }
    

        void m_Browser_DocumentComplete(object sender, DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            System.GC.Collect();
            SHDocVw.IWebBrowser2 oBrowser = (SHDocVw.IWebBrowser2)e.pDisp;
            if (oBrowser.Document == m_Browser.Document || oBrowser.Document.Equals(m_Browser.Document))
            {
                m_TargetFrame = null;
                m_URL.Text = e.uRL.ToString();
                m_SaveBtn.Enabled = true;
                if (AutoURL != null)
                {
                    save(SaveLocation == null ? "test.bmp" : SaveLocation, 1);
                    m_AutoRunDone = true;
                }
            }
            addEventListener((IHTMLDocument2)oBrowser.Document);
        }

        void m_Browser_BeforeNavigate2(object sender, DWebBrowserEvents2_BeforeNavigate2Event e)
        {
            SHDocVw.IWebBrowser2 oBrowser = (SHDocVw.IWebBrowser2)e.pDisp;
            if (oBrowser.Document == m_Browser.Document || oBrowser.Document.Equals(m_Browser.Document))
            {
                m_URL.Text = e.uRL.ToString();
                m_SaveBtn.Enabled = false;
            }
        }

        void m_Browser_StatusTextChange(object sender, DWebBrowserEvents2_StatusTextChangeEvent e)
        {
            m_StatusText.Text = e.text;
            this.Refresh();
            //Application.DoEvents();
        }

        private void addEventListener(IHTMLDocument2 doc)
        {
            HTMLDocumentClass oDoc = (HTMLDocumentClass)doc;
            try
            {
                
                if (oDoc.body is HTMLBodyClass)
                {
                    DHTMLEventHandler oHandler = new DHTMLEventHandler(doc);
                    oHandler.Handler += new DHTMLEvent(oDoc_HTMLDocumentEvents2_Event_onclick);
                    oDoc.onclick = oHandler;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("You can't access this web page: " + doc.url);
            }
        }

        void oDoc_HTMLDocumentEvents2_Event_onclick(IHTMLEventObj pEvtObj)
        {
            IHTMLDocument2 oDoc = (IHTMLDocument2)pEvtObj.srcElement.document;
            IHTMLWindow4 oWindow = (IHTMLWindow4)oDoc.parentWindow;
            if (oWindow.frameElement != null)//parentElement.offsetParent 不是 null 代表它是 frame
            {
                if (m_TargetFrame != null)
                {
                    m_TargetFrame.contentWindow.document.body.style.borderWidth = null;
                    m_TargetFrame.contentWindow.document.body.style.borderColor = null;
                    m_TargetFrame.contentWindow.document.body.style.borderStyle = null;
                }
                m_TargetFrame = (IHTMLFrameBase2)oWindow.frameElement;

                oDoc.body.style.borderWidth = "1px";
                oDoc.body.style.borderColor = "red";
                oDoc.body.style.borderStyle = "solid";
            }
            
            pEvtObj.returnValue = true;
            //return false;
        }

        private void m_GoBtn_Click(object sender, EventArgs e)
        {
            m_Browser.Navigate(m_URL.Text);
        }

        private void save(string f, int type)
        {
            Application.DoEvents();

            Bitmap oBM = null;
            if (m_TargetFrame == null)
            {
                oBM = WebCamera.takeBrowserScreen(m_Browser, m_AdBrowser);
            }
            else
            {
                oBM = WebCamera.takeFrameScreen(m_Browser, m_TargetFrame, m_AdBrowser);
            }

            switch (type)
            {
                case 1:
                    oBM.Save(f, ImageFormat.Bmp);
                    break;
                case 2:
                    oBM.Save(f, ImageFormat.Jpeg);
                    break;
                case 3:
                    oBM.Save(f, ImageFormat.Png);
                    break;
                case 4:
                    oBM.Save(f, ImageFormat.Tiff);
                    break;
            }

            oBM.Dispose();
        }

        private void m_SaveBtn_Click(object sender, EventArgs e)
        {
            if (m_SaveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            save(m_SaveDialog.FileName, m_SaveDialog.FilterIndex);
        }

        private void CameraForm_Shown(object sender, EventArgs e)
        {
            m_Browser.StatusBar = true;
            if (AutoURL != null)
            {
                m_Browser.Navigate(AutoURL);
                m_Browser.Silent = true;
            }
            else
            {
                m_Browser.GoHome();
            }

            if (m_AdBrowser != null)
            {
                m_AdBrowser.Navigate("http://fox.jenming.info/ad.html");
                if (AutoURL!=null)
                {
                    m_AdBrowser.Silent = true;
                }
            }
            CameraForm_Resize(sender, e);

        }

        private void CameraForm_Resize(object sender, EventArgs e)
        {
            m_URL.Width = this.Width - 210;
            m_ToolStropContaner.Refresh();
            this.Refresh();
            Application.DoEvents();
        }

        private void m_PreviousBtn_Click(object sender, EventArgs e)
        {
            try
            {
                m_Browser.GoBack();
            }
            catch (Exception ex) { }
        }

        private void m_NextBtn_Click(object sender, EventArgs e)
        {
            try
            {
                m_Browser.GoForward();
            }
            catch (Exception ex) { }
        }

        private void m_StopBtn_Click(object sender, EventArgs e)
        {
            m_Browser.Stop();
        }

        private void m_RefreshBtn_Click(object sender, EventArgs e)
        {
            m_Browser.Refresh2();
        }

        private void m_HomeBtn_Click(object sender, EventArgs e)
        {
            m_Browser.GoHome();
        }

        private void CameraForm_ResizeEnd(object sender, EventArgs e)
        {
            m_URL.Width = this.Width - 210;
            m_ToolStropContaner.Refresh();
            this.Refresh();
            Application.DoEvents();
        }

        private void m_URL_Enter(object sender, EventArgs e)
        {
            m_URL.SelectAll();
        }

        private void m_URL_MouseUp(object sender, MouseEventArgs e)
        {
            m_URL.SelectAll();
        }

        private void m_URL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.m_GoBtn_Click(this, e);
            }
        }

    }
}