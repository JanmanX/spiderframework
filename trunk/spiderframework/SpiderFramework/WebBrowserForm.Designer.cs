namespace fox.spider
{
    partial class WebBrowserForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            //m_WebBrowser.Offline = true;
            if (disposing && (components != null))
            {
                m_WebBrowser.Stop();
                //((MSHTML.IHTMLDocument2)m_WebBrowser.Document).open("", null, null, null);
                ((mshtml.IHTMLDocument2)m_WebBrowser.Document).close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_WebBrowser);
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.m_ReadingLbl = new System.Windows.Forms.Label();
            this.m_StatusText = new System.Windows.Forms.Label();
            this.m_BrowserContainer = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // m_ReadingLbl
            // 
            this.m_ReadingLbl.AutoSize = true;
            this.m_ReadingLbl.Location = new System.Drawing.Point(15, 12);
            this.m_ReadingLbl.Name = "m_ReadingLbl";
            this.m_ReadingLbl.Size = new System.Drawing.Size(115, 12);
            this.m_ReadingLbl.TabIndex = 1;
            this.m_ReadingLbl.Text = "Reading From Web......";
            // 
            // m_StatusText
            // 
            this.m_StatusText.AutoSize = true;
            this.m_StatusText.Location = new System.Drawing.Point(15, 117);
            this.m_StatusText.Name = "m_StatusText";
            this.m_StatusText.Size = new System.Drawing.Size(33, 12);
            this.m_StatusText.TabIndex = 2;
            this.m_StatusText.Text = "label1";
            // 
            // m_BrowserContainer
            // 
            this.m_BrowserContainer.Location = new System.Drawing.Point(17, 150);
            this.m_BrowserContainer.Name = "m_BrowserContainer";
            this.m_BrowserContainer.Size = new System.Drawing.Size(730, 412);
            this.m_BrowserContainer.TabIndex = 3;
            // 
            // WebBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 589);
            this.ControlBox = false;
            this.Controls.Add(this.m_BrowserContainer);
            this.Controls.Add(this.m_StatusText);
            this.Controls.Add(this.m_ReadingLbl);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebBrowserForm";
            this.Text = "WebBrowserForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_ReadingLbl;
        private System.Windows.Forms.Label m_StatusText;
        private System.Windows.Forms.Panel m_BrowserContainer;
    }
}