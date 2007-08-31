using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// CellBasedDocumentParser is a similar to RepeaterContentParser. It uses row-based and
    /// cell-based repeated pattern to analyze document. Some web pages render a lot of data in a table.
    /// They always renders a few data rows in a cell of table. CellBasedDocumentParser is 
    /// used to parse this kind of data, and views a cell of table as it data row.
    /// 
    /// CellBasedDocumentParser uses first row path to identify the row of a table. In each row,  
    /// CellBasedDocumentParser uses first cell path to identify the cell of a row that will be 
    /// parsed as a DataRow. After a cell is parsed, it uses next cell path to find the next cell
    /// relative to previous cell. While no cells be found in this row, CellBasedDocumentParser 
    /// uses next row path to find the next row relative to previous row and do the same analysis
    /// like first row. It will not stop analyzing while next row is not found.
    /// </summary>
    public class CellBasedDocumentParser : DocumentParser
    {
        private List<string> m_RowFirstItem=new List<string>();
        private List<string> m_CellFirstItem = new List<string>();
        private List<string> m_RowNextItem = new List<string>();
        private List<string> m_CellNextItem = new List<string>();

        private IElementRowContentParser m_RowContentParser;
        /// <summary>
        /// the row content parser will be used in the parse procedure.
        /// </summary>
        public IElementRowContentParser RowContentParser
        {
            get { return m_RowContentParser; }
            set { m_RowContentParser = value; }
        }
        /// <summary>
        /// a absolute Spider Path which is used to identify the IHTMLDOMNode standing first row.
        /// </summary>
        [Obsolete("This is an obsolete method, please use addRowFirstPath/removeRowFirstPath instead.")]
        public string RowFirstItem
        {
            get { return m_RowFirstItem.Count > 0 ? m_RowFirstItem[0] : null; }
            set { m_RowFirstItem.Add(value); }
        }

        /// <summary>
        /// a absolute Spider Path which is used to identify the IHTMLDOMNode which stands first row. This path 
        /// will be evaluated while the result evaluated from RowFirstItem is null.
        /// </summary>
        [Obsolete("This is an obsolete method, please use addRowNextPath/removeRowNextPath instead.")]
        public string RowFirstItem2
        {
            get { return m_RowFirstItem.Count > 1 ? m_RowFirstItem[1] : null; }
            set { m_RowFirstItem.Add(value); }
        }

        /// <summary>
        /// a relative Spider Path which is used to identify the IHTMLDOMNode which stands next row relative to its previous row.
        /// </summary>
        [Obsolete("This is an obsolete method, please use addRowFirstPath/removeRowFirstPath instead.")]
        public string RowNextItem
        {
            get { return m_RowNextItem.Count > 0 ? m_RowNextItem[0] : null; }
            set { m_RowNextItem.Add(value); }
        }
        /// <summary>
        /// a relative Spider Path which is used to identify the IHTMLDOMNode which stands first cell relative to its parent row.
        /// </summary>
        [Obsolete("This is an obsolete method, please use addCellFirstPath/removeCellFirstPath instead.")]
        public string CellFirstItem
        {
            get { return m_CellFirstItem.Count > 0 ? m_CellFirstItem[0] : null; }
            set { m_CellFirstItem.Add(value); }
        }
        /// <summary>
        /// a relative Spider Path which is used to identify the IHTMLDOMNode which stands next cell relative to its previous cell.
        /// </summary>
        [Obsolete("This is an obsolete method, please use addCellNextPath/removeCellNextPath instead. ")]
        public string CellNextItem
        {
            get { return m_CellNextItem.Count > 0 ? m_CellNextItem[0] : null; }
            set { m_CellNextItem.Add(value); }
        }
        /// <summary>
        /// add a new first row path, it should be a absolute path and will be used in searching first row of a table.
        /// The sequence of first row path depends on the sequence of method calls. The first added item will be evaluated
        /// at first. These first row path will be evaluated sequentially until the first row is found.
        /// </summary>
        /// <param name="row"></param>
        public void addRowFirstPath(string row)
        {
            m_RowFirstItem.Add(row);
        }
        /// <summary>
        /// add a new next row path, it should be a relative path and will be used in searching next row of a table.
        /// The sequence of next row path depends on the sequence of method calls. The first added item will be evaluated
        /// at first. These next row path will be evaluated sequentially until the next row is found.
        /// </summary>
        /// <param name="row"></param>
        public void addRowNextPath(string row)
        {
            m_RowNextItem.Add(row);
        }
        /// <summary>
        /// add a new first cell path, it should be a relative path and will be used in searching first cell of a row.
        /// The sequence of first cell path depends on the sequence of method calls. The first added item will be evaluated
        /// at first. These first cell path will be evaluated sequentially until the first cell is found.
        /// </summary>
        /// <param name="path"></param>
        public void addCellFirstPath(string path)
        {
            m_CellFirstItem.Add(path);
        }

        /// <summary>
        /// add a new next cell path, it should be a relative path and will be used in searching next cell of a row.
        /// The sequence of next cell path depends on the sequence of method calls. The first added item will be evaluated
        /// at first. These next cell path will be evaluated sequentially until the next cell is found.
        /// </summary>
        /// <param name="path"></param>
        public void addCellNextPath(string path)
        {
            m_CellNextItem.Add(path);
        }
        /// <summary>
        /// remove first row path
        /// </summary>
        /// <param name="row"></param>
        public void removeRowFirstPath(string row)
        {
            if (m_RowFirstItem.Contains(row))
            {
                m_RowFirstItem.Remove(row);
            }
        }
        /// <summary>
        /// remove next row path
        /// </summary>
        /// <param name="row"></param>
        public void removeRowNextPath(string row)
        {
            if (m_RowNextItem.Contains(row))
            {
                m_RowNextItem.Remove(row);
            }
        }
        /// <summary>
        /// remove first cell path
        /// </summary>
        /// <param name="path"></param>
        public void removeCellFirstPath(string path)
        {
            if (m_CellFirstItem.Contains(path))
            {
                m_CellFirstItem.Remove(path);
            }
        }
        /// <summary>
        /// remove next cell path
        /// </summary>
        /// <param name="path"></param>
        public void removeCellNextPath(string path)
        {
            if (m_CellNextItem.Contains(path))
            {
                m_CellNextItem.Remove(path);
            }
        }
        /// <summary>
        /// parse cotent, this method calls parseContent((IHTMLDocument2)doc.DomDocument).
        /// </summary>
        /// <param name="doc"></param>
        protected override bool parseContent(System.Windows.Forms.HtmlDocument doc)
        {
            return parseContent((IHTMLDocument2)doc.DomDocument);
        }
        /// <summary>
        /// parse content.
        /// </summary>
        /// <param name="doc"></param>
        protected override bool parseContent(IHTMLDocument2 doc)
        {
            IHTMLDOMNode oRowNode = SpiderUtilities.queryHTMLNode(doc, m_RowFirstItem);
            if (oRowNode != null)
            {
                return processRow(oRowNode);
            }
            return false;
        }

        private bool processRow(IHTMLDOMNode row)
        {
            bool bReturn = false;
            IHTMLDOMNode oRowNode = row;
            while (oRowNode != null)
            {
                IHTMLDOMNode oCellNode = SpiderUtilities.queryHTMLNode(oRowNode, m_CellFirstItem);
                while (oCellNode != null)
                {
                    DataRow oRow = getDataTable().NewRow();
                    getDataTable().Rows.Add(oRow);
                    m_RowContentParser.setDataRow(oRow);
                    m_RowContentParser.parse(oCellNode);
                    if (getRelationProcessor() != null)
                    {
                        getRelationProcessor().bindRelation(oCellNode, oRow);
                    }
                    bool bOk = fireRowParsedEvent(oRow);
                    if (!bOk)
                        getDataTable().Rows.Remove(oRow);
                    bReturn = bReturn | bOk;
                    oCellNode = SpiderUtilities.queryHTMLNode(oRowNode, m_CellNextItem);
                }

                oRowNode = SpiderUtilities.queryHTMLNode(oRowNode, m_RowNextItem);
            }
            return bReturn;
        }
    }
}
