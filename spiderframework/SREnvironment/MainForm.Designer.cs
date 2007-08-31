namespace fox.spider.runtime.window
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_StatusStrip = new System.Windows.Forms.StatusStrip();
            this.m_StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_Spliter = new System.Windows.Forms.SplitContainer();
            this.m_ControlPanel = new System.Windows.Forms.Panel();
            this.m_SaveXmlBtn = new System.Windows.Forms.Button();
            this.m_StartBtn = new System.Windows.Forms.Button();
            this.m_NavigateBtn = new System.Windows.Forms.Button();
            this.m_UrlLbl = new System.Windows.Forms.Label();
            this.m_Url = new System.Windows.Forms.TextBox();
            this.m_BrowseBtn = new System.Windows.Forms.Button();
            this.m_Config = new System.Windows.Forms.TextBox();
            this.m_ConfigLbl = new System.Windows.Forms.Label();
            this.m_OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_StatusStrip.SuspendLayout();
            this.m_Spliter.Panel1.SuspendLayout();
            this.m_Spliter.SuspendLayout();
            this.m_ControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_StatusStrip
            // 
            this.m_StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_StatusLabel});
            this.m_StatusStrip.Location = new System.Drawing.Point(0, 445);
            this.m_StatusStrip.Name = "m_StatusStrip";
            this.m_StatusStrip.Size = new System.Drawing.Size(593, 22);
            this.m_StatusStrip.TabIndex = 1;
            // 
            // m_StatusLabel
            // 
            this.m_StatusLabel.Name = "m_StatusLabel";
            this.m_StatusLabel.Size = new System.Drawing.Size(80, 17);
            this.m_StatusLabel.Text = "System Loading";
            // 
            // m_Spliter
            // 
            this.m_Spliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_Spliter.Location = new System.Drawing.Point(0, 0);
            this.m_Spliter.Name = "m_Spliter";
            this.m_Spliter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_Spliter.Panel1
            // 
            this.m_Spliter.Panel1.Controls.Add(this.m_ControlPanel);
            this.m_Spliter.Size = new System.Drawing.Size(593, 445);
            this.m_Spliter.SplitterDistance = 81;
            this.m_Spliter.TabIndex = 2;
            // 
            // m_ControlPanel
            // 
            this.m_ControlPanel.Controls.Add(this.m_SaveXmlBtn);
            this.m_ControlPanel.Controls.Add(this.m_StartBtn);
            this.m_ControlPanel.Controls.Add(this.m_NavigateBtn);
            this.m_ControlPanel.Controls.Add(this.m_UrlLbl);
            this.m_ControlPanel.Controls.Add(this.m_Url);
            this.m_ControlPanel.Controls.Add(this.m_BrowseBtn);
            this.m_ControlPanel.Controls.Add(this.m_Config);
            this.m_ControlPanel.Controls.Add(this.m_ConfigLbl);
            this.m_ControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ControlPanel.Location = new System.Drawing.Point(0, 0);
            this.m_ControlPanel.Name = "m_ControlPanel";
            this.m_ControlPanel.Size = new System.Drawing.Size(593, 81);
            this.m_ControlPanel.TabIndex = 0;
            // 
            // m_SaveXmlBtn
            // 
            this.m_SaveXmlBtn.Enabled = false;
            this.m_SaveXmlBtn.Location = new System.Drawing.Point(506, 44);
            this.m_SaveXmlBtn.Name = "m_SaveXmlBtn";
            this.m_SaveXmlBtn.Size = new System.Drawing.Size(75, 23);
            this.m_SaveXmlBtn.TabIndex = 7;
            this.m_SaveXmlBtn.Text = "Save as XML";
            this.m_SaveXmlBtn.UseVisualStyleBackColor = true;
            this.m_SaveXmlBtn.Click += new System.EventHandler(this.m_SaveXmlBtn_Click);
            // 
            // m_StartBtn
            // 
            this.m_StartBtn.Enabled = false;
            this.m_StartBtn.Location = new System.Drawing.Point(506, 5);
            this.m_StartBtn.Name = "m_StartBtn";
            this.m_StartBtn.Size = new System.Drawing.Size(75, 23);
            this.m_StartBtn.TabIndex = 6;
            this.m_StartBtn.Text = "Start";
            this.m_StartBtn.UseVisualStyleBackColor = true;
            this.m_StartBtn.Click += new System.EventHandler(this.m_StartBtn_Click);
            // 
            // m_NavigateBtn
            // 
            this.m_NavigateBtn.Enabled = false;
            this.m_NavigateBtn.Location = new System.Drawing.Point(440, 44);
            this.m_NavigateBtn.Name = "m_NavigateBtn";
            this.m_NavigateBtn.Size = new System.Drawing.Size(59, 23);
            this.m_NavigateBtn.TabIndex = 5;
            this.m_NavigateBtn.Text = "Navigate";
            this.m_NavigateBtn.UseVisualStyleBackColor = true;
            this.m_NavigateBtn.Click += new System.EventHandler(this.m_NavigateBtn_Click);
            // 
            // m_UrlLbl
            // 
            this.m_UrlLbl.AutoSize = true;
            this.m_UrlLbl.Location = new System.Drawing.Point(12, 47);
            this.m_UrlLbl.Name = "m_UrlLbl";
            this.m_UrlLbl.Size = new System.Drawing.Size(23, 12);
            this.m_UrlLbl.TabIndex = 4;
            this.m_UrlLbl.Text = "Url:";
            // 
            // m_Url
            // 
            this.m_Url.Location = new System.Drawing.Point(112, 44);
            this.m_Url.Name = "m_Url";
            this.m_Url.Size = new System.Drawing.Size(322, 22);
            this.m_Url.TabIndex = 3;
            // 
            // m_BrowseBtn
            // 
            this.m_BrowseBtn.Location = new System.Drawing.Point(440, 4);
            this.m_BrowseBtn.Name = "m_BrowseBtn";
            this.m_BrowseBtn.Size = new System.Drawing.Size(59, 23);
            this.m_BrowseBtn.TabIndex = 2;
            this.m_BrowseBtn.Text = "Browse";
            this.m_BrowseBtn.UseVisualStyleBackColor = true;
            this.m_BrowseBtn.Click += new System.EventHandler(this.m_BrowseBtn_Click);
            // 
            // m_Config
            // 
            this.m_Config.Location = new System.Drawing.Point(112, 6);
            this.m_Config.Name = "m_Config";
            this.m_Config.ReadOnly = true;
            this.m_Config.Size = new System.Drawing.Size(322, 22);
            this.m_Config.TabIndex = 1;
            // 
            // m_ConfigLbl
            // 
            this.m_ConfigLbl.AutoSize = true;
            this.m_ConfigLbl.Location = new System.Drawing.Point(12, 9);
            this.m_ConfigLbl.Name = "m_ConfigLbl";
            this.m_ConfigLbl.Size = new System.Drawing.Size(94, 12);
            this.m_ConfigLbl.TabIndex = 0;
            this.m_ConfigLbl.Text = "Configuration File:";
            // 
            // m_OpenDialog
            // 
            this.m_OpenDialog.DefaultExt = "*.xml";
            this.m_OpenDialog.FileName = "*.xml";
            this.m_OpenDialog.Filter = "Config XML|*.xml";
            this.m_OpenDialog.Title = "Configuration File";
            // 
            // MainForm
            // 
            this.AcceptButton = this.m_StartBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(593, 467);
            this.Controls.Add(this.m_Spliter);
            this.Controls.Add(this.m_StatusStrip);
            this.Name = "MainForm";
            this.Controls.SetChildIndex(this.m_StatusStrip, 0);
            this.Controls.SetChildIndex(this.m_Spliter, 0);
            this.m_StatusStrip.ResumeLayout(false);
            this.m_StatusStrip.PerformLayout();
            this.m_Spliter.Panel1.ResumeLayout(false);
            this.m_Spliter.ResumeLayout(false);
            this.m_ControlPanel.ResumeLayout(false);
            this.m_ControlPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip m_StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusLabel;
        private System.Windows.Forms.SplitContainer m_Spliter;
        private System.Windows.Forms.Panel m_ControlPanel;
        private System.Windows.Forms.Button m_NavigateBtn;
        private System.Windows.Forms.Label m_UrlLbl;
        private System.Windows.Forms.TextBox m_Url;
        private System.Windows.Forms.Button m_BrowseBtn;
        private System.Windows.Forms.TextBox m_Config;
        private System.Windows.Forms.Label m_ConfigLbl;
        private System.Windows.Forms.OpenFileDialog m_OpenDialog;
        private System.Windows.Forms.Button m_StartBtn;
        private System.Windows.Forms.Button m_SaveXmlBtn;
    }
}
