using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// TableExtractor parses each row of a html table to a data row of a data table.
    /// 
    /// </summary>
    public class TableExtractor : DocumentParser 
    {
        private string m_TablePath;
        private int m_StartRowIndex = 0;
        private int m_EndRowIndex = int.MaxValue;
        private IElementRowContentParser m_RowParser;
        /// <summary>
        /// a IElementRowContentParser will be used in parsing data row.
        /// </summary>
        public IElementRowContentParser RowContentParser
        {
            get { return m_RowParser; }
            set { m_RowParser = value; }
        }
        /// <summary>
        /// a index of the first row which need to parse as a data row in html table. The default value of this property is 0.
        /// </summary>
        public int StartRowIndex
        {
            get { return m_StartRowIndex; }
            set { m_StartRowIndex = value; }
        }
        /// <summary>
        /// a index of the last row which need to parse as a data row in html table. The default value of this property is int.MaxValue.
        /// </summary>
        public int EndRowIndex
        {
            get { return m_EndRowIndex; }
            set { m_EndRowIndex = value; }
        }
        /// <summary>
        /// a absolute spider path pointing to html table.
        /// </summary>
        public string TablePath
        {
            get { return m_TablePath; }
            set { m_TablePath = value; }
        }
        /// <summary>
        /// parse a data row
        /// </summary>
        /// <param name="row">TR</param>
        protected bool parseRow(IHTMLTableRow row)
        {
            DataRow oDataRow=getDataTable().NewRow();
            getDataTable().Rows.Add(oDataRow);
            m_RowParser.setDataRow(oDataRow);
            m_RowParser.parse((IHTMLDOMNode) row);
            if (getRelationProcessor() != null)
            {
                getRelationProcessor().bindRelation((IHTMLDOMNode)row, oDataRow);
            }
            bool bOk=fireRowParsedEvent(oDataRow);
            if (!bOk)
            {
                getDataTable().Rows.Remove(oDataRow);
            }
            return bOk;
        }
        /// <summary>
        /// get the html table from htmldocument.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>Table ©Î¬O null</returns>
        protected IHTMLDOMNode getTable(HtmlDocument doc)
        {
            return SpiderPath.selectSingleNode(doc, TablePath);
        }
        /// <summary>
        /// parse content
        /// </summary>
        /// <param name="doc"></param>
        protected override bool parseContent(HtmlDocument doc)
        {
            IHTMLDOMNode oTable = getTable(doc);
            if (oTable == null)
                return false;
            return processTable(oTable);
        }
        /// <summary>
        /// parse content
        /// </summary>
        /// <param name="doc"></param>
        protected override bool parseContent(IHTMLDocument2 doc)
        {
            //string sTmpPath = TablePath.Substring(6);
            IHTMLDOMNode oTable = SpiderPath.selectSingleNode(doc, TablePath);
            if (oTable == null)
                return false;
            return processTable(oTable);
        }
        /// <summary>
        /// parse table. if table argument is not a IHTMLTable, this method will not parse anthing.
        /// </summary>
        /// <param name="table"></param>
        private bool processTable(IHTMLDOMNode table)
        {
            if (!(table is IHTMLTable))
                return false;
            IHTMLTable oITable = (IHTMLTable)table;
            bool bReturn = false;
            for (int i = StartRowIndex; i < oITable.rows.length && i < EndRowIndex; i++)
            {
                bReturn = bReturn | parseRow((IHTMLTableRow)oITable.rows.item(i, null));
            }
            return bReturn;
        }

    }
}
