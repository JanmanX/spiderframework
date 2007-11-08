namespace fox.spider.path.builder.ui
{
    partial class WebStructure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebStructure));
            this.m_StatusStrip = new System.Windows.Forms.StatusStrip();
            this.m_StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_BrowserStrip = new System.Windows.Forms.ToolStrip();
            this.m_PreviousBtn = new System.Windows.Forms.ToolStripButton();
            this.m_NextBtn = new System.Windows.Forms.ToolStripButton();
            this.m_StopBtn = new System.Windows.Forms.ToolStripButton();
            this.m_RefreshBtn = new System.Windows.Forms.ToolStripButton();
            this.m_HomeBtn = new System.Windows.Forms.ToolStripButton();
            this.m_ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.m_SplitPane = new System.Windows.Forms.SplitContainer();
            this.m_ContentSpliter = new System.Windows.Forms.SplitContainer();
            this.m_StructureTree = new System.Windows.Forms.TreeView();
            this.m_AttributeList = new System.Windows.Forms.ListView();
            this.m_ListViewNameColumn = new System.Windows.Forms.ColumnHeader();
            this.m_ListViewValueColumn = new System.Windows.Forms.ColumnHeader();
            this.m_ListViewExpandoColumn = new System.Windows.Forms.ColumnHeader();
            this.m_AttributeLbl = new System.Windows.Forms.Label();
            this.m_URLStrip = new System.Windows.Forms.ToolStrip();
            this.m_URLLbl = new System.Windows.Forms.ToolStripLabel();
            this.m_URL = new System.Windows.Forms.ToolStripTextBox();
            this.m_GoBtn = new System.Windows.Forms.ToolStripButton();
            this.m_PathStrip = new System.Windows.Forms.ToolStrip();
            this.m_PathLbl = new System.Windows.Forms.ToolStripLabel();
            this.m_Path = new System.Windows.Forms.ToolStripTextBox();
            this.m_BuildBtn = new System.Windows.Forms.ToolStripButton();
            this.m_StructureToolBar = new System.Windows.Forms.ToolStrip();
            this.m_ReloadStructure = new System.Windows.Forms.ToolStripButton();
            this.m_AttributeToolbar = new System.Windows.Forms.ToolStrip();
            this.m_ReloadAttribute = new System.Windows.Forms.ToolStripButton();
            this.m_StatusStrip.SuspendLayout();
            this.m_BrowserStrip.SuspendLayout();
            this.m_ToolStripContainer.ContentPanel.SuspendLayout();
            this.m_ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.m_ToolStripContainer.SuspendLayout();
            this.m_SplitPane.Panel1.SuspendLayout();
            this.m_SplitPane.SuspendLayout();
            this.m_ContentSpliter.Panel1.SuspendLayout();
            this.m_ContentSpliter.Panel2.SuspendLayout();
            this.m_ContentSpliter.SuspendLayout();
            this.m_URLStrip.SuspendLayout();
            this.m_PathStrip.SuspendLayout();
            this.m_StructureToolBar.SuspendLayout();
            this.m_AttributeToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_StatusStrip
            // 
            this.m_StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_StatusText});
            this.m_StatusStrip.Location = new System.Drawing.Point(0, 499);
            this.m_StatusStrip.Name = "m_StatusStrip";
            this.m_StatusStrip.Size = new System.Drawing.Size(678, 22);
            this.m_StatusStrip.TabIndex = 0;
            // 
            // m_StatusText
            // 
            this.m_StatusText.Name = "m_StatusText";
            this.m_StatusText.Size = new System.Drawing.Size(35, 17);
            this.m_StatusText.Text = "Ready";
            // 
            // m_BrowserStrip
            // 
            this.m_BrowserStrip.AllowMerge = false;
            this.m_BrowserStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.m_BrowserStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.m_BrowserStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_PreviousBtn,
            this.m_NextBtn,
            this.m_StopBtn,
            this.m_RefreshBtn,
            this.m_HomeBtn});
            this.m_BrowserStrip.Location = new System.Drawing.Point(3, 0);
            this.m_BrowserStrip.Name = "m_BrowserStrip";
            this.m_BrowserStrip.Size = new System.Drawing.Size(324, 31);
            this.m_BrowserStrip.TabIndex = 1;
            this.m_BrowserStrip.Text = "toolStrip1";
            // 
            // m_PreviousBtn
            // 
            this.m_PreviousBtn.Image = global::fox.spider.path.builder.ui.Properties.Resources.previous2;
            this.m_PreviousBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_PreviousBtn.Name = "m_PreviousBtn";
            this.m_PreviousBtn.Size = new System.Drawing.Size(73, 28);
            this.m_PreviousBtn.Text = "&Previous";
            this.m_PreviousBtn.Click += new System.EventHandler(this.m_PreviousBtn_Click);
            // 
            // m_NextBtn
            // 
            this.m_NextBtn.Image = global::fox.spider.path.builder.ui.Properties.Resources.nex2t;
            this.m_NextBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_NextBtn.Name = "m_NextBtn";
            this.m_NextBtn.Size = new System.Drawing.Size(55, 28);
            this.m_NextBtn.Text = "&Next";
            this.m_NextBtn.Click += new System.EventHandler(this.m_NextBtn_Click);
            // 
            // m_StopBtn
            // 
            this.m_StopBtn.Image = global::fox.spider.path.builder.ui.Properties.Resources.stop2;
            this.m_StopBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_StopBtn.Name = "m_StopBtn";
            this.m_StopBtn.Size = new System.Drawing.Size(54, 28);
            this.m_StopBtn.Text = "&Stop";
            this.m_StopBtn.Click += new System.EventHandler(this.m_StopBtn_Click);
            // 
            // m_RefreshBtn
            // 
            this.m_RefreshBtn.Image = global::fox.spider.path.builder.ui.Properties.Resources.refresh2;
            this.m_RefreshBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_RefreshBtn.Name = "m_RefreshBtn";
            this.m_RefreshBtn.Size = new System.Drawing.Size(69, 28);
            this.m_RefreshBtn.Text = "&Refresh";
            this.m_RefreshBtn.Click += new System.EventHandler(this.m_RefreshBtn_Click);
            // 
            // m_HomeBtn
            // 
            this.m_HomeBtn.Image = global::fox.spider.path.builder.ui.Properties.Resources.home2;
            this.m_HomeBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_HomeBtn.Name = "m_HomeBtn";
            this.m_HomeBtn.Size = new System.Drawing.Size(61, 28);
            this.m_HomeBtn.Text = "&Home";
            this.m_HomeBtn.Click += new System.EventHandler(this.m_HomeBtn_Click);
            // 
            // m_ToolStripContainer
            // 
            // 
            // m_ToolStripContainer.ContentPanel
            // 
            this.m_ToolStripContainer.ContentPanel.Controls.Add(this.m_SplitPane);
            this.m_ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(678, 418);
            this.m_ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.m_ToolStripContainer.Name = "m_ToolStripContainer";
            this.m_ToolStripContainer.Size = new System.Drawing.Size(678, 499);
            this.m_ToolStripContainer.TabIndex = 2;
            this.m_ToolStripContainer.Text = "toolStripContainer1";
            // 
            // m_ToolStripContainer.TopToolStripPanel
            // 
            this.m_ToolStripContainer.TopToolStripPanel.Controls.Add(this.m_BrowserStrip);
            this.m_ToolStripContainer.TopToolStripPanel.Controls.Add(this.m_URLStrip);
            this.m_ToolStripContainer.TopToolStripPanel.Controls.Add(this.m_PathStrip);
            // 
            // m_SplitPane
            // 
            this.m_SplitPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_SplitPane.Location = new System.Drawing.Point(0, 0);
            this.m_SplitPane.Name = "m_SplitPane";
            // 
            // m_SplitPane.Panel1
            // 
            this.m_SplitPane.Panel1.Controls.Add(this.m_ContentSpliter);
            this.m_SplitPane.Size = new System.Drawing.Size(678, 418);
            this.m_SplitPane.SplitterDistance = 226;
            this.m_SplitPane.TabIndex = 0;
            // 
            // m_ContentSpliter
            // 
            this.m_ContentSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ContentSpliter.Location = new System.Drawing.Point(0, 0);
            this.m_ContentSpliter.Name = "m_ContentSpliter";
            this.m_ContentSpliter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_ContentSpliter.Panel1
            // 
            this.m_ContentSpliter.Panel1.Controls.Add(this.m_StructureTree);
            this.m_ContentSpliter.Panel1.Controls.Add(this.m_StructureToolBar);
            // 
            // m_ContentSpliter.Panel2
            // 
            this.m_ContentSpliter.Panel2.Controls.Add(this.m_AttributeList);
            this.m_ContentSpliter.Panel2.Controls.Add(this.m_AttributeToolbar);
            this.m_ContentSpliter.Panel2.Controls.Add(this.m_AttributeLbl);
            this.m_ContentSpliter.Size = new System.Drawing.Size(226, 418);
            this.m_ContentSpliter.SplitterDistance = 205;
            this.m_ContentSpliter.TabIndex = 0;
            // 
            // m_StructureTree
            // 
            this.m_StructureTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_StructureTree.HideSelection = false;
            this.m_StructureTree.Location = new System.Drawing.Point(0, 25);
            this.m_StructureTree.Name = "m_StructureTree";
            this.m_StructureTree.Size = new System.Drawing.Size(226, 180);
            this.m_StructureTree.TabIndex = 0;
            this.m_StructureTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_StructureTree_AfterSelect);
            this.m_StructureTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_StructureTree_BeforeSelect);
            // 
            // m_AttributeList
            // 
            this.m_AttributeList.AllowColumnReorder = true;
            this.m_AttributeList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ListViewNameColumn,
            this.m_ListViewValueColumn,
            this.m_ListViewExpandoColumn});
            this.m_AttributeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_AttributeList.FullRowSelect = true;
            this.m_AttributeList.Location = new System.Drawing.Point(0, 37);
            this.m_AttributeList.MultiSelect = false;
            this.m_AttributeList.Name = "m_AttributeList";
            this.m_AttributeList.Size = new System.Drawing.Size(226, 172);
            this.m_AttributeList.TabIndex = 0;
            this.m_AttributeList.UseCompatibleStateImageBehavior = false;
            this.m_AttributeList.View = System.Windows.Forms.View.Details;
            this.m_AttributeList.SelectedIndexChanged += new System.EventHandler(this.m_AttributeList_SelectedIndexChanged);
            // 
            // m_ListViewNameColumn
            // 
            this.m_ListViewNameColumn.Text = "Name";
            // 
            // m_ListViewValueColumn
            // 
            this.m_ListViewValueColumn.Text = "Value";
            // 
            // m_ListViewExpandoColumn
            // 
            this.m_ListViewExpandoColumn.Text = "Expando";
            // 
            // m_AttributeLbl
            // 
            this.m_AttributeLbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_AttributeLbl.Location = new System.Drawing.Point(0, 0);
            this.m_AttributeLbl.Name = "m_AttributeLbl";
            this.m_AttributeLbl.Size = new System.Drawing.Size(226, 12);
            this.m_AttributeLbl.TabIndex = 1;
            this.m_AttributeLbl.Text = "Attributes:";
            // 
            // m_URLStrip
            // 
            this.m_URLStrip.AllowMerge = false;
            this.m_URLStrip.AutoSize = false;
            this.m_URLStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.m_URLStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_URLLbl,
            this.m_URL,
            this.m_GoBtn});
            this.m_URLStrip.Location = new System.Drawing.Point(3, 31);
            this.m_URLStrip.Name = "m_URLStrip";
            this.m_URLStrip.Size = new System.Drawing.Size(147, 25);
            this.m_URLStrip.TabIndex = 2;
            // 
            // m_URLLbl
            // 
            this.m_URLLbl.Name = "m_URLLbl";
            this.m_URLLbl.Size = new System.Drawing.Size(31, 22);
            this.m_URLLbl.Text = "&URL:";
            // 
            // m_URL
            // 
            this.m_URL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.m_URL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.m_URL.AutoSize = false;
            this.m_URL.Name = "m_URL";
            this.m_URL.Size = new System.Drawing.Size(500, 25);
            this.m_URL.Enter += new System.EventHandler(this.m_URL_Enter);
            this.m_URL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_URL_MouseUp);
            this.m_URL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_URL_KeyUp);
            // 
            // m_GoBtn
            // 
            this.m_GoBtn.Image = global::fox.spider.path.builder.ui.Properties.Resources.go2;
            this.m_GoBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_GoBtn.Name = "m_GoBtn";
            this.m_GoBtn.Size = new System.Drawing.Size(39, 20);
            this.m_GoBtn.Text = "&Go";
            this.m_GoBtn.Click += new System.EventHandler(this.m_GoBtn_Click);
            // 
            // m_PathStrip
            // 
            this.m_PathStrip.AllowMerge = false;
            this.m_PathStrip.AutoSize = false;
            this.m_PathStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.m_PathStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_PathLbl,
            this.m_Path,
            this.m_BuildBtn});
            this.m_PathStrip.Location = new System.Drawing.Point(3, 56);
            this.m_PathStrip.Name = "m_PathStrip";
            this.m_PathStrip.Size = new System.Drawing.Size(147, 25);
            this.m_PathStrip.TabIndex = 0;
            this.m_PathStrip.Text = "toolStrip1";
            // 
            // m_PathLbl
            // 
            this.m_PathLbl.Name = "m_PathLbl";
            this.m_PathLbl.Size = new System.Drawing.Size(28, 22);
            this.m_PathLbl.Text = "Path:";
            // 
            // m_Path
            // 
            this.m_Path.AutoSize = false;
            this.m_Path.Name = "m_Path";
            this.m_Path.ReadOnly = true;
            this.m_Path.Size = new System.Drawing.Size(100, 25);
            this.m_Path.Enter += new System.EventHandler(this.m_Path_Enter);
            // 
            // m_BuildBtn
            // 
            this.m_BuildBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_BuildBtn.Image = ((System.Drawing.Image)(resources.GetObject("m_BuildBtn.Image")));
            this.m_BuildBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_BuildBtn.Name = "m_BuildBtn";
            this.m_BuildBtn.Size = new System.Drawing.Size(35, 16);
            this.m_BuildBtn.Text = "Build";
            this.m_BuildBtn.Click += new System.EventHandler(this.m_BuildBtn_Click);
            // 
            // m_StructureToolBar
            // 
            this.m_StructureToolBar.AllowMerge = false;
            this.m_StructureToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ReloadStructure});
            this.m_StructureToolBar.Location = new System.Drawing.Point(0, 0);
            this.m_StructureToolBar.Name = "m_StructureToolBar";
            this.m_StructureToolBar.Size = new System.Drawing.Size(226, 25);
            this.m_StructureToolBar.TabIndex = 1;
            this.m_StructureToolBar.Text = "toolStrip1";
            // 
            // m_ReloadStructure
            // 
            this.m_ReloadStructure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ReloadStructure.Image = global::fox.spider.path.builder.ui.Properties.Resources.refresh2;
            this.m_ReloadStructure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_ReloadStructure.Name = "m_ReloadStructure";
            this.m_ReloadStructure.Size = new System.Drawing.Size(23, 22);
            this.m_ReloadStructure.Click += new System.EventHandler(this.m_ReloadStructure_Click);
            // 
            // m_AttributeToolbar
            // 
            this.m_AttributeToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ReloadAttribute});
            this.m_AttributeToolbar.Location = new System.Drawing.Point(0, 12);
            this.m_AttributeToolbar.Name = "m_AttributeToolbar";
            this.m_AttributeToolbar.Size = new System.Drawing.Size(226, 25);
            this.m_AttributeToolbar.TabIndex = 2;
            this.m_AttributeToolbar.Text = "toolStrip1";
            // 
            // m_ReloadAttribute
            // 
            this.m_ReloadAttribute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ReloadAttribute.Image = global::fox.spider.path.builder.ui.Properties.Resources.refresh2;
            this.m_ReloadAttribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_ReloadAttribute.Name = "m_ReloadAttribute";
            this.m_ReloadAttribute.Size = new System.Drawing.Size(23, 22);
            this.m_ReloadAttribute.Click += new System.EventHandler(this.m_ReloadAttribute_Click);
            // 
            // WebStructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 521);
            this.Controls.Add(this.m_ToolStripContainer);
            this.Controls.Add(this.m_StatusStrip);
            this.Name = "WebStructure";
            this.Text = "WebStructure";
            this.Resize += new System.EventHandler(this.WebStructure_Resize);
            this.Shown += new System.EventHandler(this.WebStructure_Shown);
            this.ResizeEnd += new System.EventHandler(this.WebStructure_ResizeEnd);
            this.m_StatusStrip.ResumeLayout(false);
            this.m_StatusStrip.PerformLayout();
            this.m_BrowserStrip.ResumeLayout(false);
            this.m_BrowserStrip.PerformLayout();
            this.m_ToolStripContainer.ContentPanel.ResumeLayout(false);
            this.m_ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.m_ToolStripContainer.TopToolStripPanel.PerformLayout();
            this.m_ToolStripContainer.ResumeLayout(false);
            this.m_ToolStripContainer.PerformLayout();
            this.m_SplitPane.Panel1.ResumeLayout(false);
            this.m_SplitPane.ResumeLayout(false);
            this.m_ContentSpliter.Panel1.ResumeLayout(false);
            this.m_ContentSpliter.Panel1.PerformLayout();
            this.m_ContentSpliter.Panel2.ResumeLayout(false);
            this.m_ContentSpliter.Panel2.PerformLayout();
            this.m_ContentSpliter.ResumeLayout(false);
            this.m_URLStrip.ResumeLayout(false);
            this.m_URLStrip.PerformLayout();
            this.m_PathStrip.ResumeLayout(false);
            this.m_PathStrip.PerformLayout();
            this.m_StructureToolBar.ResumeLayout(false);
            this.m_StructureToolBar.PerformLayout();
            this.m_AttributeToolbar.ResumeLayout(false);
            this.m_AttributeToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip m_StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusText;
        private System.Windows.Forms.ToolStrip m_BrowserStrip;
        private System.Windows.Forms.ToolStripButton m_PreviousBtn;
        private System.Windows.Forms.ToolStripButton m_NextBtn;
        private System.Windows.Forms.ToolStripButton m_StopBtn;
        private System.Windows.Forms.ToolStripButton m_RefreshBtn;
        private System.Windows.Forms.ToolStripButton m_HomeBtn;
        private System.Windows.Forms.ToolStripContainer m_ToolStripContainer;
        private System.Windows.Forms.ToolStrip m_URLStrip;
        private System.Windows.Forms.ToolStripLabel m_URLLbl;
        private System.Windows.Forms.ToolStripTextBox m_URL;
        private System.Windows.Forms.ToolStripButton m_GoBtn;
        private System.Windows.Forms.ToolStrip m_PathStrip;
        private System.Windows.Forms.ToolStripLabel m_PathLbl;
        private System.Windows.Forms.ToolStripTextBox m_Path;
        private System.Windows.Forms.ToolStripButton m_BuildBtn;
        private System.Windows.Forms.SplitContainer m_SplitPane;
        private System.Windows.Forms.TreeView m_StructureTree;
        private System.Windows.Forms.SplitContainer m_ContentSpliter;
        private System.Windows.Forms.ListView m_AttributeList;
        private System.Windows.Forms.ColumnHeader m_ListViewNameColumn;
        private System.Windows.Forms.ColumnHeader m_ListViewValueColumn;
        private System.Windows.Forms.ColumnHeader m_ListViewExpandoColumn;
        private System.Windows.Forms.Label m_AttributeLbl;
        private System.Windows.Forms.ToolStrip m_StructureToolBar;
        private System.Windows.Forms.ToolStripButton m_ReloadStructure;
        private System.Windows.Forms.ToolStrip m_AttributeToolbar;
        private System.Windows.Forms.ToolStripButton m_ReloadAttribute;
    }
}