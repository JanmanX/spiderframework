using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using fox.spider.runtime;

namespace fox.spider.runtime.test
{
    public partial class TestForm : fox.spider.BasicSpiderForm
    {
        private SpiderRuntime m_Runtine;
        public TestForm()
        {
            InitializeComponent();
            this.Browser.Dock = DockStyle.Fill;
        }

        private void m_TestBtn_Click(object sender, EventArgs e)
        {
            if (m_OpenDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            XmlDocument oDoc = new XmlDocument();
            oDoc.Load(m_OpenDialog.FileName);

            m_Runtine = new SpiderRuntime(oDoc.DocumentElement);
            m_Runtine.init(Browser);

            m_Runtine.Parsed += new SpiderParsed(m_Runtine_Parsed);
            m_Runtine.ParsingError += new SpiderError(m_Runtine_ParsingError);
            m_Runtine.ReadyForStart += new SpiderReady(m_Runtine_ReadyForStart);
            m_Runtine.ChangePageResult += new SpiderChangePageResult(m_Runtine_ChangePageResult);
            m_Runtine.ParseFailOnMatchedUrl += new SpiderUrlMatchedButNotParsed(m_Runtine_ParseFailOnMatchedUrl);

        }

        void m_Runtine_ParseFailOnMatchedUrl(object sender, string url, mshtml.IHTMLDocument2 doc)
        {
            Console.WriteLine("Exit on Parsing fail");
            this.Invoke(new MethodInvoker(this.Close));
        }


        void m_Runtine_ChangePageResult(object sender, IWebPageTurner turner, System.Text.RegularExpressions.Regex pattern, string fromurl, bool success)
        {
            if (!success)
            {
                Console.WriteLine("Exit on Paging Failed");
                this.Invoke(new MethodInvoker(this.Close));
            }
        }

        void m_Runtine_ReadyForStart(object sender)
        {
            if (!m_Runtine.start())
            {
                Console.WriteLine("Exit on Not Start");
                this.Invoke(new MethodInvoker(this.Close));
            }
        }

        void m_Runtine_ParsingError(object sender, string url, mshtml.IHTMLDocument2 doc, SpiderFlow flow, Exception e)
        {
            Console.WriteLine(e.StackTrace);
            
        }

        void m_Runtine_Parsed(object sender, string url, mshtml.IHTMLDocument2 doc, bool parsed, bool matched)
        {
            Console.WriteLine(url);
            foreach (DataRow row in m_Runtine.ResourceProvider.getDataSet().Tables[0].Rows)
            {
                for (int i = 0; i < m_Runtine.ResourceProvider.getDataSet().Tables[0].Columns.Count; i++)
                {
                    Console.Write(row[i] + ", ");
                }
                Console.WriteLine();
            }
        }

        private void m_GoBtn_Click(object sender, EventArgs e)
        {
            
            
        }
    }
}

