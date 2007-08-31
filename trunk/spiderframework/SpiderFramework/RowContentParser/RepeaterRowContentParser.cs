using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// RepeaterRowContentParser uses repeated pattern to fill data row. Like RepeaterContentParser, 
    /// it also has first item path and next item path. RepeaterRowContentParser evaluates first item
    /// path to find the first item and use next item path to find the other items. 
    /// <br/>
    /// Besides item path, 
    /// RepeaterRowContentParser proposes a special matching mechanism to determine which content needs
    /// to be saved to where.You may provide arguments to RepeaterRowContentParser by addParsingInfo. 
    /// RepeaterRowContentParser will use match path, another spider path, and match regexp,  to 
    /// check if specified node exists and the node content also matched with regexp. If yes, 
    /// RepeaterRowContentParser will use value path, a spider path, and value filter, a regexp, to 
    /// retrieve the content which will be saved in data row. 
    /// 
    /// <br/>
    ///  This is very useful RowContentParser when you meet the data who will be stored in a data row 
    /// spread in many nodes. 
    /// </summary>
    public class RepeaterRowContentParser : ElementRowContentParser
    {
        private List<string> m_FirstItem = new List<string>();
        private List<string> m_NextItem = new List<string>();
        private List<ParsingInfo> m_ParsingInfo = new List<ParsingInfo>();

        /// <summary>
        /// add a first item path.
        /// </summary>
        /// <param name="path"></param>
        public void addFirstItemPath(string path)
        {
            m_FirstItem.Add(path);
        }
        /// <summary>
        /// add a next item path
        /// </summary>
        /// <param name="p"></param>
        public void addNextItemPath(string p)
        {
            m_NextItem.Add(p);
        }
        /// <summary>
        /// remove the first item path
        /// </summary>
        /// <param name="p"></param>
        public void removeFirstItemPath(string p)
        {
            if (m_FirstItem.Contains(p))
            {
                m_FirstItem.Remove(p);
            }
        }
        /// <summary>
        /// remove the next item path
        /// </summary>
        /// <param name="p"></param>
        public void removeNextItemPath(string p)
        {
            if (m_NextItem.Contains(p))
            {
                m_NextItem.Remove(p);
            }
        }
        /// <summary>
        /// add parsing info. If matcherPath points to an existing node, the value of the node pointed by valuePath 
        /// will be stored in field column.
        /// </summary>
        /// <param name="field">a data column name or index of data row</param>
        /// <param name="matcherPath">a spider path used to check if matcher exists.</param>
        /// <param name="valuePath">a spider path used to find the value node</param>
        public void addParsingInfo(object field, string matcherPath, string valuePath)
        {
            addParsingInfo(field, matcherPath, null, valuePath);
        }
        /// <summary>
        /// add parsing info. If matcherPath points to an existing node and the content, innerText or nodeValue, 
        /// of this node matches to macher, the value of the node pointed by valuePath will be stored in field column.
        /// </summary>
        /// <param name="field">a data column name or index of data row</param>
        /// <param name="matcherPath">a spider path used to check if matcher exists.</param>
        /// <param name="matcher">a regular expression used to check if the node pointed by matcherPath is we want</param>
        /// <param name="valuePath">a spider path used to find the value node</param>
        public void addParsingInfo(object field, string matcherPath, Regex matcher, string valuePath)
        {
            addParsingInfo(field, matcherPath, matcher, valuePath, null);
        }
        /// <summary>
        /// add parsing info. If matcherPath points to an existing node and the content, innerText or nodeValue, 
        /// of this node matches to macher, the value, which will be filtered by valueFilter and only the first 
        /// match group will be used, of the node pointed by valuePath will be stored in field column.
        /// </summary>
        /// <param name="field">a data column name or index of data row</param>
        /// <param name="matcherPath">a spider path used to check if matcher exists.</param>
        /// <param name="matcher">a regular expression used to check if the node pointed by matcherPath is we want</param>
        /// <param name="valuePath">a spider path used to find the value node</param>
        /// <param name="valueFilter">a regluar expression used to filter the value</param>
        public void addParsingInfo(object field, string matcherPath, Regex matcher, string valuePath, Regex valueFilter)
        {
            ParsingInfo oInfo = new ParsingInfo();
            oInfo.Field = field;
            oInfo.MatcherPath = matcherPath;
            oInfo.MatcherRegex = matcher;
            oInfo.ValuePath = valuePath;
            oInfo.ValueRegex = valueFilter;

            m_ParsingInfo.Add(oInfo);
        }

        /// <summary>
        /// parse a node with parsing info.
        /// </summary>
        /// <param name="n"></param>
        protected void parseItem(IHTMLDOMNode n)
        {
            foreach (ParsingInfo info in m_ParsingInfo)
            {
                IHTMLDOMNode oMatcher = SpiderPath.selectSingleNode(n, info.MatcherPath);
                if (oMatcher == null)
                {
                    continue;
                }
                IHTMLDOMNode oValue = SpiderPath.selectSingleNode(oMatcher, info.ValuePath);
                if (oValue == null)
                {
                    continue;
                }

                string sMatcherText = HTMLUtilities.getNodeValue(oMatcher);
                if (info.MatcherRegex == null || info.MatcherRegex.IsMatch(sMatcherText))
                {
                    string sValue = HTMLUtilities.getNodeValue(oValue);
                    if (info.ValueRegex != null)
                    {
                        Match oMatch = info.ValueRegex.Match(sValue);
                        if (oMatch.Success && oMatch.Groups.Count > 1)
                        {
                            sValue = oMatch.Groups[1].Value;
                        }
                    }
                    setFieldValue(info.Field, sValue);
                }
            }
        }
        /// <summary>
        /// parse content with first item path and next item path.
        /// </summary>
        /// <param name="node"></param>
        protected override void parseContent(IHTMLDOMNode node)
        {
            IHTMLDOMNode oNode = SpiderUtilities.queryHTMLNode(node, m_FirstItem);
            while (oNode != null)
            {
                parseItem(oNode);
                oNode = SpiderUtilities.queryHTMLNode(oNode, m_NextItem);
            }
            
        }

        private class ParsingInfo
        {
            private string m_MatcherPath;

            public string MatcherPath
            {
                get { return m_MatcherPath; }
                set { m_MatcherPath = value; }
            }

            private string m_ValuePath;

            public string ValuePath
            {
                get { return m_ValuePath; }
                set { m_ValuePath = value; }
            }

            private Regex m_MatcherRegex;

            public Regex MatcherRegex
            {
                get { return m_MatcherRegex; }
                set { m_MatcherRegex = value; }
            }

            private Regex m_ValueRegex;

            public Regex ValueRegex
            {
                get { return m_ValueRegex; }
                set { m_ValueRegex = value; }
            }

            private object m_Field;

            public object Field
            {
                get { return m_Field; }
                set { m_Field = value; }
            }
        }
    }
}
