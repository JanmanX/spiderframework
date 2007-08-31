namespace fox.spider.runtime.test
{
    partial class TestForm
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
            this.m_LoadBtn = new System.Windows.Forms.Button();
            this.m_OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // m_LoadBtn
            // 
            this.m_LoadBtn.Location = new System.Drawing.Point(317, 0);
            this.m_LoadBtn.Name = "m_LoadBtn";
            this.m_LoadBtn.Size = new System.Drawing.Size(75, 23);
            this.m_LoadBtn.TabIndex = 1;
            this.m_LoadBtn.Text = "Load";
            this.m_LoadBtn.UseVisualStyleBackColor = true;
            this.m_LoadBtn.Click += new System.EventHandler(this.m_TestBtn_Click);
            // 
            // m_OpenDialog
            // 
            this.m_OpenDialog.DefaultExt = "*.xml";
            this.m_OpenDialog.Filter = "Config|*.xml";
            this.m_OpenDialog.Title = "Choose a configuration file";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(391, 365);
            this.Controls.Add(this.m_LoadBtn);
            this.Name = "TestForm";
            this.Controls.SetChildIndex(this.m_LoadBtn, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_LoadBtn;
        private System.Windows.Forms.OpenFileDialog m_OpenDialog;
    }
}
