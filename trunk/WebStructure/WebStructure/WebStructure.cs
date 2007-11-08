using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxSHDocVw;
using mshtml;

namespace fox.spider.path.builder.ui
{
    public partial class WebStructure : Form
    {
        private AxSHDocVw.AxWebBrowser m_Browser;
        private int m_NodeCount = 0;
        private IHTMLDOMNode m_ActiveNode;
        private IHTMLDOMAttribute2 m_ActiveAttribute;

        public WebStructure()
        {
            InitializeComponent();
            /// initialize web browser.
            m_Browser = new AxWebBrowser();
            m_Browser.Dock = DockStyle.Fill;
            this.m_SplitPane.Panel2.Controls.Add(m_Browser);
            m_Browser.BringToFront();
            m_Browser.StatusTextChange += new DWebBrowserEvents2_StatusTextChangeEventHandler(m_Browser_StatusTextChange);
            m_Browser.BeforeNavigate2 += new DWebBrowserEvents2_BeforeNavigate2EventHandler(m_Browser_BeforeNavigate2);
            m_Browser.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(m_Browser_DocumentComplete);

            /// resize the URL and Path cool bar.
            WebStructure_ResizeEnd(this, null);
        }

        #region Browser event handler

        void m_Browser_DocumentComplete(object sender, DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            /// emit a garbage collection request which should clears managed objects 
            /// who have a reference to COM.
            System.GC.Collect();

            /// if the event sender is equal to the browser we use, we need to parse the whole HTML 
            /// DOM to build the structure tree..
            SHDocVw.IWebBrowser2 oBrowser = (SHDocVw.IWebBrowser2)e.pDisp;
            if (m_Browser.Document.Equals(oBrowser.Document))
            {
                m_URL.Text = e.uRL.ToString();
                reloadTree();
            }
        }

        void m_Browser_BeforeNavigate2(object sender, DWebBrowserEvents2_BeforeNavigate2Event e)
        {
            /// if the event sender is equal to the browser we use, we need to change url to the translated
            /// one.
            
            SHDocVw.IWebBrowser2 oBrowser = (SHDocVw.IWebBrowser2)e.pDisp;
            if (oBrowser.Document == m_Browser.Document || oBrowser.Document.Equals(m_Browser.Document))
            {
                m_URL.Text = e.uRL.ToString();
            }
        }

        void m_Browser_StatusTextChange(object sender, DWebBrowserEvents2_StatusTextChangeEvent e)
        {
            /// bridge browser messages.
            m_StatusText.Text = e.text;
            m_StatusText.Invalidate();
            m_StatusStrip.Update();
        }

        #endregion

        #region functional button for browser event listener

        private void m_GoBtn_Click(object sender, EventArgs e)
        {
            if (!"".Equals(m_URL.Text) && m_URL.Text != null)
            {
                m_Browser.Navigate(m_URL.Text);
            }
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
            ///if we use m_Browser.Refresh2(), it does not fire a document complete 
            ///event whose sender is equal to the browser we use. So, if user want to 
            ///refresh web page, we use navigate to browse the page again.
            IHTMLDocument2 oDoc = (IHTMLDocument2)m_Browser.Document;
            m_Browser.Navigate(oDoc.url);
        }

        private void m_HomeBtn_Click(object sender, EventArgs e)
        {
            m_Browser.GoHome();
        }

        #endregion

        private void WebStructure_ResizeEnd(object sender, EventArgs e)
        {
            ///recalc the size of URL strip and Path strip.
            m_URLStrip.SuspendLayout();
            m_URLStrip.Width = m_ToolStripContainer.TopToolStripPanel.Width - 20;
            m_URL.Width = this.Width - 120;
            m_URLStrip.ResumeLayout();
            m_PathStrip.SuspendLayout();
            m_PathStrip.Width = m_ToolStripContainer.TopToolStripPanel.Width - 20;
            m_Path.Width = this.Width - 120;
            m_PathStrip.ResumeLayout();
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
            /// if user press return in URL text box, navigate the url he/her types.
            if (e.KeyCode == Keys.Return)
            {
                this.m_GoBtn.PerformClick();
            }
        }

        private void WebStructure_Resize(object sender, EventArgs e)
        {
            ///recalc the size of URL strip and Path strip.
            m_URLStrip.SuspendLayout();
            m_URLStrip.Width = m_ToolStripContainer.TopToolStripPanel.Width - 20;
            m_URL.Width = this.Width - 120;
            m_URLStrip.ResumeLayout();
            m_PathStrip.SuspendLayout();
            m_PathStrip.Width = m_ToolStripContainer.TopToolStripPanel.Width - 20;
            m_Path.Width = this.Width - 120;
            m_PathStrip.ResumeLayout();
        }

        private void m_Path_Enter(object sender, EventArgs e)
        {
            m_Path.Copy();
        }

        private void WebStructure_Shown(object sender, EventArgs e)
        {
            m_HomeBtn.PerformClick();
        }

        private void logText(string str)
        {
            /// convenient function to dump messages.
            Console.WriteLine("[" + DateTime.Now + "] " + str );
            m_StatusText.Text = str;
            Application.DoEvents();
        }
        #region Main Functions
        private void reloadTree()
        {
            IHTMLDocument2 oDoc = (IHTMLDocument2)m_Browser.Document;
            if (oDoc.body == null)
            {
                return;
            }
            logText("start to reconstruct structure tree.");
            m_NodeCount = 0;
            m_StructureTree.Nodes.Clear();
            m_AttributeList.Items.Clear();
            m_ActiveNode = null;
            
            TreeNode oBody = displayNode((IHTMLDOMNode)oDoc.body);
            m_StructureTree.Nodes.Add(oBody);
            logText("structure tree construction finished. " + m_NodeCount + " nodes created.");
        }

        private TreeNode displayNode(IHTMLDOMNode n)
        {
            if (n == null)
                return null;

            /// create a tree node tagged with n.
            TreeNode oNode = new TreeNode(n.nodeName);
            oNode.Tag = n;
            oNode.Name = n.nodeName + m_NodeCount;
            m_NodeCount++;
            logText("construct tree node #" + m_NodeCount + ", name: " + n.nodeName);
            if (n.hasChildNodes())
            {
                ///create children of n
                IHTMLDOMNode oChild = n.firstChild;
                while (oChild != null)
                {
                    TreeNode oChildTN = displayNode(oChild);
                    if (oChildTN != null)
                    {
                        oNode.Nodes.Add(oChildTN);
                    }
                    oChild = oChild.nextSibling;
                }
            }

            if (n is IHTMLFrameBase2)
            {
                /// if n is a frame, we need to build the structure of its content.
                IHTMLFrameBase2 oFrame = (IHTMLFrameBase2)n;
                if (null != oFrame.contentWindow &&
                    null != oFrame.contentWindow.document &&
                    null != oFrame.contentWindow.document.body)
                {
                    try
                    {
                        TreeNode oChildTN = displayNode((IHTMLDOMNode)oFrame.contentWindow.document.body);
                        oChildTN.ToolTipText = oFrame.contentWindow.document.url;
                        if (oChildTN != null)
                        {
                            oNode.Nodes.Add(oChildTN);
                        }
                    }
                    catch (Exception ex)
                    {
                        logText("unparsed frame, url: " + ((IHTMLFrameBase)oFrame).src + " \r\n Reason: " + ex.Message);
                    }
                }
            }
            return oNode;
        }

        private void reloadAttribute(IHTMLDOMNode n)
        {
            
            m_AttributeList.Items.Clear();
            if (n.attributes == null)
            {
                return;
            }
            logText("start to list all attributes");
            /// list all attributes
            IHTMLAttributeCollection aAtt = (IHTMLAttributeCollection)n.attributes;
            for (int i = 0; i < aAtt.length; i++)
            {

                object oIndex = (object)i;
                IHTMLDOMAttribute oAtt = (IHTMLDOMAttribute)aAtt.item(ref oIndex);
                /// if the attribute has been specified by html or javascript, we list 
                /// it in attribute list.
                if (oAtt.specified)
                { 
                    logText("list attribute " + oAtt.nodeName);
                    m_AttributeList.Items.Add(createListViewItem(oAtt));
                }
            }
            logText("attributes has been listed.");
        }

        private ListViewItem createListViewItem(IHTMLDOMAttribute att)
        {
            /// create a ListViewItem tagged with att.
            IHTMLDOMAttribute2 att2 = (IHTMLDOMAttribute2)att;
            ListViewItem oItem = new ListViewItem(att.nodeName);
            oItem.Tag = att;
            oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, att.nodeValue.ToString()));
            oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, att2.expando.ToString()));
            return oItem;
        }

        private void markSelection()
        {
            /// put the feedback border on.
            IHTMLDOMNode oElem = (IHTMLDOMNode)m_StructureTree.SelectedNode.Tag;
            if (oElem is IHTMLElement)
            {
                ((IHTMLElement)oElem).style.border = "red 1px dotted";
            }
            ///reload all attributes.
            reloadAttribute(oElem);
            m_ActiveNode = oElem;
            m_ActiveAttribute = null;
        }

        private void unmarkSelection()
        {
            if (m_StructureTree.SelectedNode == null)
                return;
            /// clear the feedback border.
            IHTMLDOMNode oElem = (IHTMLDOMNode)m_StructureTree.SelectedNode.Tag;
            if (oElem is IHTMLElement)
            {
                ((IHTMLElement)oElem).style.border = "";
            }
            m_ActiveNode = null;
            m_ActiveAttribute = null;
        }

        #endregion

        #region selection event handler

        private void m_StructureTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            markSelection();
        }

        private void m_StructureTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            unmarkSelection();
        }

        private void m_AttributeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// set active attribute if there is at least one selected item.
            if(m_AttributeList.SelectedItems.Count > 0){
                m_ActiveAttribute = (IHTMLDOMAttribute2)m_AttributeList.SelectedItems[0].Tag;
            }
        }

        #endregion

        private void m_BuildBtn_Click(object sender, EventArgs e)
        {
            /// uses path builder to build the SPath.
            if (m_ActiveNode != null)
            {
                if (m_ActiveAttribute != null)
                {
                    m_Path.Text = fox.spider.path.builder.Builder.buildPath(m_ActiveNode, m_ActiveAttribute);
                }
                else
                {
                    m_Path.Text = fox.spider.path.builder.Builder.buildPath(m_ActiveNode);
                }
                IHTMLDOMNode2 oNode2 = (IHTMLDOMNode2)m_ActiveNode;
                logText("URL: " + ((IHTMLDocument2)oNode2.ownerDocument).url);
            }
        }

        private void m_ReloadAttribute_Click(object sender, EventArgs e)
        {
            if (m_ActiveNode != null)
            {
                reloadAttribute(m_ActiveNode);
            }
            
        }

        private void m_ReloadStructure_Click(object sender, EventArgs e)
        {
            unmarkSelection();
            reloadTree();
        }
    }
}