using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace fox.spider
{
    /// <summary>
    /// RepeaterContentParser is similar to CellBasedDocumentParser. It analyzes data in 
    /// repeated pattern. Some web pages render data in a sequential repeated block which
    /// may be a div or a li or table. RepeaterContentParser is used to analyze this kind 
    /// of html. 
    /// 
    /// RepeaterContentParser uses first item path to find out the first item and parse it 
    /// as a data row. After first item, it uses next item path which is a relative path to
    /// find the other items and parses them as data rows.
    /// </summary>
    public class RepeaterContentParser : DocumentParser
    {
        private IElementRowContentParser m_RowContentParser;

        private List<string> m_FirstItem=new List<string>();
        private List<string> m_NextItem=new List<string>();

        /// <summary>
        /// add first item path, it should be absolute path and will be used in searching first item.
        /// The sequence of first item path depends on the sequence of method calls. The first added item will be evaluated
        /// at first. These first item path will be evaluated sequentially until the first item is found.
        /// </summary>
        /// <param name="first"></param>
        public void addFirstItemPath(string first)
        {
            m_FirstItem.Add(first);
        }
        /// <summary>
        /// add next item path, it should be relative path and will be used in searching next item.
        /// The sequence of next item path depends on the sequence of method calls. The first added item will be evaluated
        /// at first. These next item path will be evaluated sequentially until the next item is found.
        /// </summary>
        /// <param name="next"></param>
        public void addNextItemPath(string next)
        {
            m_NextItem.Add(next);
        }
        /// <summary>
        /// remove first item path
        /// </summary>
        /// <param name="first"></param>
        public void removeFirstItemPath(string first)
        {
            if (m_FirstItem.Contains(first))
            {
                m_FirstItem.Remove(first);
            }
        }
        /// <summary>
        /// remove next item path;
        /// </summary>
        /// <param name="next"></param>
        public void removeNextItemPath(string next)
        {
            if (m_NextItem.Contains(next))
            {
                m_NextItem.Remove(next);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("This is an obsolete method. Please use addNextItemPath/removeNextPath instead.")]
        public string NextItemPath2
        {
            get { return m_NextItem.Count > 1 ? m_NextItem[1] : null; }
            set
            {
                m_NextItem.Add(value);
            }
        }
        /// <summary>
        /// </summary>
        [Obsolete("This is an obsolete method. Please use addFirstItemPath/removeFirstPath instead.")]
        public string FirstItemPath2
        {
            get { return m_FirstItem.Count > 1 ? m_FirstItem[1] : null; }
            set { m_FirstItem.Add(value); }
        }
        /// <summary>
        /// a IElementRowContentParser is used to parse data row.
        /// </summary>
        public IElementRowContentParser RowContentParser
        {
            get { return m_RowContentParser; }
            set { m_RowContentParser = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Obsolete("This is an obsolete method. Please use addFirstItemPath/removeFirstPath instead.")]
        public string FirstItemPath
        {
            get { return m_FirstItem.Count > 0 ? m_FirstItem[0] : null; }
            set { m_FirstItem.Add(value); }
        }
        
        /// <summary>
        /// </summary>
        [Obsolete("This is an obsolete method. Please use addNextItemPath/removeNextPath instead.")]
        public string NextItemPath
        {
            get { return m_NextItem.Count > 0 ? m_NextItem[0] : null; }
            set { m_NextItem.Add(value); }
        }

        /// <summary>
        /// calls parseContent((mshtml.IHTMLDocument2) doc.DomDocument)
        /// </summary>
        /// <param name="doc"></param>
        protected override bool parseContent(System.Windows.Forms.HtmlDocument doc)
        {
            return parseContent((mshtml.IHTMLDocument2) doc.DomDocument);
        }


        /// <summary>
        /// parse content.
        /// 
        /// RepeaterContentParser uses first item path to find first html node and parse this node as a
        /// data row. After first item, RepeaterContentParser uses next item path to find other nodes and
        /// prase them as data rows.
        /// </summary>
        /// <param name="doc"></param>
        protected override bool parseContent(mshtml.IHTMLDocument2 doc)
        {
            bool bReturn = false;
            mshtml.IHTMLDOMNode oElem = SpiderUtilities.queryHTMLNode(doc, m_FirstItem);
            while (oElem != null)
            {
                mshtml.IHTMLDOMNode oNode = oElem;
                DataRow oRow = getDataTable().NewRow();
                getDataTable().Rows.Add(oRow);
                m_RowContentParser.setDataRow(oRow);
                m_RowContentParser.parse(oNode);
                if (getRelationProcessor() != null)
                {
                    getRelationProcessor().bindRelation(oNode, oRow);
                }

                bool bOk = fireRowParsedEvent(oRow);
                if (!bOk)
                    getDataTable().Rows.Remove(oRow);
                bReturn = bReturn | bOk;
                oElem = SpiderUtilities.queryHTMLNode(oElem, m_NextItem);
            }
            return bReturn;
        }
    }
}
