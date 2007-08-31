using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using fox.spider;
using mshtml;

namespace TestFramework
{
    public partial class TestingForm : fox.spider.BasicSpiderForm
    {
        public TestingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Browser.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(Browser_DocumentComplete);
            this.NavigateUrl("http://www.enet.com.cn/eschool/zhuanti/easyhtml/4/sample/06.htm");
        }

        void Browser_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            if (e.uRL.Equals("http://www.enet.com.cn/eschool/zhuanti/easyhtml/4/sample/06.htm"))
            {
                IHTMLDOMNode node = SpiderPath.selectSingleNode((mshtml.IHTMLDocument2)Browser.Document, "/FRAME[0]/BODY/P[0]");
                Console.WriteLine(node.nodeName);
                if (node is IHTMLElement)
                {
                    Console.WriteLine(((IHTMLElement)node).innerText);
                }

            }
        
        }
    }
}

