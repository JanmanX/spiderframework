namespace fox.spider.screenshot
{
    partial class CameraForm
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
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraForm));
            this.m_ToolStropContaner = new System.Windows.Forms.ToolStripContainer();
            this.m_MainStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.m_URL = new System.Windows.Forms.ToolStripTextBox();
            this.m_GoBtn = new System.Windows.Forms.ToolStripButton();
            this.m_SaveBtn = new System.Windows.Forms.ToolStripButton();
            this.m_BrowserStrip = new System.Windows.Forms.ToolStrip();
            this.m_PreviousBtn = new System.Windows.Forms.ToolStripButton();
            this.m_NextBtn = new System.Windows.Forms.ToolStripButton();
            this.m_StopBtn = new System.Windows.Forms.ToolStripButton();
            this.m_RefreshBtn = new System.Windows.Forms.ToolStripButton();
            this.m_HomeBtn = new System.Windows.Forms.ToolStripButton();
            this.m_StatusBar = new System.Windows.Forms.StatusStrip();
            this.m_StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_ToolStropContaner.TopToolStripPanel.SuspendLayout();
            this.m_ToolStropContaner.SuspendLayout();
            this.m_MainStrip.SuspendLayout();
            this.m_BrowserStrip.SuspendLayout();
            this.m_StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ToolStropContaner
            // 
            this.m_ToolStropContaner.BottomToolStripPanelVisible = false;
            // 
            // m_ToolStropContaner.ContentPanel
            // 
            resources.ApplyResources(this.m_ToolStropContaner.ContentPanel, "m_ToolStropContaner.ContentPanel");
            resources.ApplyResources(this.m_ToolStropContaner, "m_ToolStropContaner");
            this.m_ToolStropContaner.LeftToolStripPanelVisible = false;
            this.m_ToolStropContaner.Name = "m_ToolStropContaner";
            this.m_ToolStropContaner.RightToolStripPanelVisible = false;
            // 
            // m_ToolStropContaner.TopToolStripPanel
            // 
            this.m_ToolStropContaner.TopToolStripPanel.Controls.Add(this.m_BrowserStrip);
            this.m_ToolStropContaner.TopToolStripPanel.Controls.Add(this.m_MainStrip);
            // 
            // m_MainStrip
            // 
            this.m_MainStrip.CanOverflow = false;
            resources.ApplyResources(this.m_MainStrip, "m_MainStrip");
            this.m_MainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.m_URL,
            this.m_GoBtn,
            this.m_SaveBtn});
            this.m_MainStrip.Name = "m_MainStrip";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            // 
            // m_URL
            // 
            this.m_URL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.m_URL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            resources.ApplyResources(this.m_URL, "m_URL");
            this.m_URL.Name = "m_URL";
            this.m_URL.Enter += new System.EventHandler(this.m_URL_Enter);
            this.m_URL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_URL_MouseUp);
            this.m_URL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_URL_KeyUp);
            // 
            // m_GoBtn
            // 
            this.m_GoBtn.Image = global::fox.spider.screenshot.Properties.Resources.go2;
            resources.ApplyResources(this.m_GoBtn, "m_GoBtn");
            this.m_GoBtn.Name = "m_GoBtn";
            this.m_GoBtn.Click += new System.EventHandler(this.m_GoBtn_Click);
            // 
            // m_SaveBtn
            // 
            this.m_SaveBtn.Image = global::fox.spider.screenshot.Properties.Resources.save;
            resources.ApplyResources(this.m_SaveBtn, "m_SaveBtn");
            this.m_SaveBtn.Name = "m_SaveBtn";
            this.m_SaveBtn.Click += new System.EventHandler(this.m_SaveBtn_Click);
            // 
            // m_BrowserStrip
            // 
            resources.ApplyResources(this.m_BrowserStrip, "m_BrowserStrip");
            this.m_BrowserStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.m_BrowserStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_PreviousBtn,
            this.m_NextBtn,
            this.m_StopBtn,
            this.m_RefreshBtn,
            this.m_HomeBtn});
            this.m_BrowserStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.m_BrowserStrip.Name = "m_BrowserStrip";
            // 
            // m_PreviousBtn
            // 
            this.m_PreviousBtn.Image = global::fox.spider.screenshot.Properties.Resources.previous2;
            resources.ApplyResources(this.m_PreviousBtn, "m_PreviousBtn");
            this.m_PreviousBtn.Name = "m_PreviousBtn";
            this.m_PreviousBtn.Click += new System.EventHandler(this.m_PreviousBtn_Click);
            // 
            // m_NextBtn
            // 
            this.m_NextBtn.Image = global::fox.spider.screenshot.Properties.Resources.nex2t;
            resources.ApplyResources(this.m_NextBtn, "m_NextBtn");
            this.m_NextBtn.Name = "m_NextBtn";
            this.m_NextBtn.Click += new System.EventHandler(this.m_NextBtn_Click);
            // 
            // m_StopBtn
            // 
            this.m_StopBtn.Image = global::fox.spider.screenshot.Properties.Resources.stop2;
            resources.ApplyResources(this.m_StopBtn, "m_StopBtn");
            this.m_StopBtn.Name = "m_StopBtn";
            this.m_StopBtn.Click += new System.EventHandler(this.m_StopBtn_Click);
            // 
            // m_RefreshBtn
            // 
            this.m_RefreshBtn.Image = global::fox.spider.screenshot.Properties.Resources.refresh2;
            resources.ApplyResources(this.m_RefreshBtn, "m_RefreshBtn");
            this.m_RefreshBtn.Name = "m_RefreshBtn";
            this.m_RefreshBtn.Click += new System.EventHandler(this.m_RefreshBtn_Click);
            // 
            // m_HomeBtn
            // 
            this.m_HomeBtn.Image = global::fox.spider.screenshot.Properties.Resources.home2;
            resources.ApplyResources(this.m_HomeBtn, "m_HomeBtn");
            this.m_HomeBtn.Name = "m_HomeBtn";
            this.m_HomeBtn.Click += new System.EventHandler(this.m_HomeBtn_Click);
            // 
            // m_StatusBar
            // 
            this.m_StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_StatusText});
            resources.ApplyResources(this.m_StatusBar, "m_StatusBar");
            this.m_StatusBar.Name = "m_StatusBar";
            // 
            // m_StatusText
            // 
            this.m_StatusText.Name = "m_StatusText";
            resources.ApplyResources(this.m_StatusText, "m_StatusText");
            // 
            // m_SaveDialog
            // 
            resources.ApplyResources(this.m_SaveDialog, "m_SaveDialog");
            // 
            // CameraForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_StatusBar);
            this.Controls.Add(this.m_ToolStropContaner);
            this.Name = "CameraForm";
            this.Resize += new System.EventHandler(this.CameraForm_Resize);
            this.Shown += new System.EventHandler(this.CameraForm_Shown);
            this.ResizeEnd += new System.EventHandler(this.CameraForm_ResizeEnd);
            this.m_ToolStropContaner.TopToolStripPanel.ResumeLayout(false);
            this.m_ToolStropContaner.TopToolStripPanel.PerformLayout();
            this.m_ToolStropContaner.ResumeLayout(false);
            this.m_ToolStropContaner.PerformLayout();
            this.m_MainStrip.ResumeLayout(false);
            this.m_MainStrip.PerformLayout();
            this.m_BrowserStrip.ResumeLayout(false);
            this.m_BrowserStrip.PerformLayout();
            this.m_StatusBar.ResumeLayout(false);
            this.m_StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer m_ToolStropContaner;
        private System.Windows.Forms.ToolStrip m_MainStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox m_URL;
        private System.Windows.Forms.ToolStripButton m_GoBtn;
        private System.Windows.Forms.StatusStrip m_StatusBar;
        private System.Windows.Forms.ToolStripButton m_SaveBtn;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusText;
        private System.Windows.Forms.SaveFileDialog m_SaveDialog;
        private System.Windows.Forms.ToolStrip m_BrowserStrip;
        private System.Windows.Forms.ToolStripButton m_PreviousBtn;
        private System.Windows.Forms.ToolStripButton m_NextBtn;
        private System.Windows.Forms.ToolStripButton m_StopBtn;
        private System.Windows.Forms.ToolStripButton m_RefreshBtn;
        private System.Windows.Forms.ToolStripButton m_HomeBtn;

    }
}