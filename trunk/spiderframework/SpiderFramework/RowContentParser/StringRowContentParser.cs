using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// StringRowContentParser is a kind of regular expression row content parser. It uses
    /// a lot of regular expression to retrieve value and store to data column.
    /// 
    /// example: 
    /// <code>
    ///StringRowContentParser oRegex = new StringRowContentParser();
    ///oRegex.MatchHTML=false;
    ///oRegex.NodePath="TD[0]"
    ///oRegex.addMapping(@"(\d+)", "RefCount");
    /// </code>
    /// The above code will save (\d+) to RefCount field.
    /// </summary>
    public class StringRowContentParser : ElementRowContentParser
    {
        /// <summary>
        /// 
        /// </summary>
        [Obsolete("Use NodePath instead.")]
        public const string TDPATH = "TDPATH";

        private bool m_MatchHTML=false;

        /// <summary>
        /// if true, StringRowContentParser will match innerHtml instead of innerText or nodeValue.
        /// </summary>
        public bool MatchHTML
        {
            get { return m_MatchHTML; }
            set { m_MatchHTML = value; }
        }

        /// <summary>
        /// add a regular expression and column name mapping.
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="name"></param>
        public void addMapping(string regex, string name)
        {
            if (ColumnMapping == null)
            {
                ColumnMapping = new System.Collections.Hashtable();
            }

            ColumnMapping.Add(regex, name);
        }
        /// <summary>
        /// add a regular expression and column index mapping.
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="index"></param>
        public void addMapping(string regex, int index)
        {
            if (ColumnMapping == null)
            {
                ColumnMapping = new System.Collections.Hashtable();
            }

            ColumnMapping.Add(regex, index);
        }
        /// <summary>
        /// a spider path pointing to the node whose content needs to be saved.
        /// </summary>
        public string NodePath
        {
            get
            {
                if (ColumnMapping == null)
                {
                    return null;
                }

                return ColumnMapping[TDPATH].ToString();
            }
            set
            {
                if (ColumnMapping == null)
                {
                    ColumnMapping = new System.Collections.Hashtable();
                }

                ColumnMapping.Add(TDPATH, value);
            }
        }

        /// <summary>
        /// parse content.
        /// </summary>
        /// <param name="row"></param>
        protected override void parseContent(mshtml.IHTMLDOMNode row)
        {
            string sPath = (string)ColumnMapping[TDPATH];
            IHTMLDOMNode oNode = SpiderPath.selectSingleNode(row, sPath);
            if (oNode == null)
            {
                return;
            }

            string sText = MatchHTML ? ((IHTMLElement)oNode).outerHTML : ((IHTMLElement)oNode).innerText;
            foreach (string sKey in ColumnMapping.Keys)
            {
                if (sKey.Equals(TDPATH))
                    continue;
                object oObj = (object)ColumnMapping[sKey];
                Regex oRegex = new Regex(sKey, RegexOptions.IgnoreCase);
                Match oMatch = oRegex.Match(sText);
                if (oMatch.Success)
                {
                    if (oObj is int)
                    {
                        getDataRow()[(int)oObj] = oMatch.Groups[1].Value;
                    }
                    else if (oObj is string)
                    {
                        getDataRow()[(string)oObj] = oMatch.Groups[1].Value;
                    }
                }

            }
        }
    }
}
