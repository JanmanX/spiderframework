using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// PriorityRowContentParser has ability to evaluate a list of Spider Path sequentially.
    /// But it only save the first result returned from the list to data row.
    /// 
    /// </summary>
    public class PriorityRowContentParser : ElementRowContentParser
    {
        /// <summary>
        /// 
        /// </summary>
        [Obsolete("Use addXPath instead.")]
        public const string XPATH_LIST="Priority_List";
        /// <summary>
        /// </summary>
        [Obsolete("Use ColumnName/ColumnPosition instead.")]
        public const string TARGET = "Priority_TARGET";
        
        /// <summary>
        /// add a spider path to the list. the sequence of addition will be the sequence of evaluation
        /// </summary>
        /// <param name="xpath"></param>
        public void addXPath(string xpath)
        {
            if (!ColumnMapping.Contains(XPATH_LIST))
            {
                ColumnMapping.Add(XPATH_LIST, new ArrayList());
            }

            ArrayList aPath = (ArrayList)ColumnMapping[XPATH_LIST];
            aPath.Add(xpath);
        }
        /// <summary>
        /// the data column name
        /// </summary>
        public string ColumnName
        {
            get
            {
                if (ColumnMapping == null)
                {
                    return null;
                }

                if (ColumnMapping[TARGET] == null)
                {
                    return null;
                }

                if (ColumnMapping[TARGET] is string)
                {
                    return (string)ColumnMapping[TARGET];
                }
                return null;
            }
            set
            {
                if (!ColumnMapping.Contains(XPATH_LIST))
                {
                    ColumnMapping = new Hashtable();
                }
                ColumnMapping.Add(TARGET, value);
            }

        }
        /// <summary>
        /// the data column index
        /// </summary>
        public int ColumnPosition
        {
            get
            {
                if (ColumnMapping == null)
                {
                    return -1;
                }

                if (ColumnMapping[TARGET] == null)
                {
                    return -1;
                }

                if (ColumnMapping[TARGET] is int)
                {
                    return (int)ColumnMapping[TARGET];
                }
                return -1;
            }
            set
            {
                if (!ColumnMapping.Contains(XPATH_LIST))
                {
                    ColumnMapping = new Hashtable();
                }
                ColumnMapping.Add(TARGET, value);
            }

        }

        /// <summary>
        /// parse
        /// </summary>
        /// <param name="node"></param>
        protected override void parseContent(mshtml.IHTMLDOMNode node)
        {
            ArrayList aPath = (ArrayList)ColumnMapping[XPATH_LIST];
            object oObj = ColumnMapping[TARGET];
            foreach (string sKey in aPath)
            {
                IHTMLDOMNode oNode = SpiderPath.selectSingleNode(node, sKey);
                if (oNode != null && oObj != null)
                {
                    string sValue = (oNode is IHTMLElement ? ((IHTMLElement)oNode).innerText : oNode.nodeValue.ToString());
                    if (oObj is int)
                    {
                        getDataRow()[(int)oObj] = sValue;
                    }
                    else if (oObj is string)
                    {
                        getDataRow()[(string)oObj] = sValue;
                    }
                    return;
                }

            }
        }

        
    }
}
