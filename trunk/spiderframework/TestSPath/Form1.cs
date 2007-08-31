using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using fox.spider.path;

namespace TestSPath
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.w3.org/TR/xpath");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            SPath oSPath = new SPath();
            object oReturn=oSPath.selectSingleNode(webBrowser1.Document, "/BODY/DIV/A[0]/IMG[0]/@src");
            Console.WriteLine(oReturn);
        }
    }
}