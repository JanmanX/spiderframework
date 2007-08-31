using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// XPathRowContentParser is the simplest row content parse. It use spider path to find 
    /// nodes and save their innerText or nodeValue to data row.
    /// 
    /// sample code
    /// <code>
    /// XPathRowContentParser oXPathParser = new XPathRowContentParser();
    /// oXPathParser.addMapping("TD[1]/A", "Player");
    /// oXPathParser.addMapping("TD[1]/SPAN", "FGMA");
    /// </code>
    /// 
    /// </summary>
    public class XPathRowContentParser : ElementRowContentParser
    {
        private bool m_TrimAllText = false;

        /// <summary>
        /// if true, XPathRowContentParser trims text.
        /// </summary>
        public bool TrimAllText
        {
            get { return m_TrimAllText; }
            set { m_TrimAllText = value; }
        }
        /// <summary>
        /// add mapping info
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="col"></param>
        public void addMapping(string xpath, string col)
        {
            if (ColumnMapping == null)
            {
                ColumnMapping = new Hashtable();
            }

            ColumnMapping.Add(xpath, col);
        }
        /// <summary>
        /// add mapping info
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="col"></param>
        public void addMapping(string xpath, int col)
        {
            if (ColumnMapping == null)
            {
                ColumnMapping = new Hashtable();
            }

            ColumnMapping.Add(xpath, col);
        }

        /// <summary>
        /// parse content
        /// </summary>
        /// <param name="baseNode"></param>
        protected override void parseContent(mshtml.IHTMLDOMNode baseNode)
        {
            ICollection oCol = ColumnMapping.Keys;
            foreach(string sKey in oCol)
            {
                object oObj=ColumnMapping[sKey];
                IHTMLDOMNode oNode = SpiderPath.selectSingleNode(baseNode, sKey);
                if (oNode != null && oObj!=null)
                {
                    string sValue = (oNode is IHTMLElement ? ((IHTMLElement)oNode).innerText:oNode.nodeValue.ToString());
                    if (TrimAllText && sValue!=null)
                    {
                        sValue = sValue.Trim();
                    }
                    if (oObj is int)
                    {
                        getDataRow()[(int)oObj] = sValue;
                    }
                    else if (oObj is string)
                    {
                        getDataRow()[(string)oObj] = sValue;
                    }
                }
                
            }
        }
    }
}
