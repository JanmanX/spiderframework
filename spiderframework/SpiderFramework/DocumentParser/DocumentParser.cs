using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider
{
    /// <summary>
    /// DocumentParser is an abstract implementation of IDocumentParser. All document parser 
    /// should extend from this class. DocumentParser implements all IDocumentParser's interfaces, and 
    /// propose two abstract methods, parseContent, for its decendents. These two different parseContent
    /// methods are to parse content in HtmlDocument type which is a class in System.Windows.Forms and to 
    /// parse content in IHTMLDocument2 type which is a interface in mshtml. 
    /// 
    /// </summary>
    public abstract class DocumentParser : IDocumentParser
    {
        private System.Data.DataTable m_DataTable;
        private IDocumentParser m_Next;
        private IRelationProcessor m_RelationProcessor;
        /// <summary>
        /// parse Html Document in HtmlDocument type.
        /// </summary>
        /// <param name="doc"></param>
        abstract protected bool parseContent(System.Windows.Forms.HtmlDocument doc);
        /// <summary>
        /// parse Html Document in IHTMLDocument2 type
        /// </summary>
        /// <param name="doc"></param>
        abstract protected bool parseContent(mshtml.IHTMLDocument2 doc);
        /// <summary>
        /// fire RowParsed event
        /// </summary>
        /// <param name="row">parsed data row </param>
        /// <returns>the return value of event handlers</returns>
        protected bool fireRowParsedEvent(System.Data.DataRow row)
        {
            if (RowParsed != null)
            {
                return RowParsed(this, row);
            }
            return true;
        }

        #region IDocumentParserChain member
        /// <summary>
        /// parse content.
        /// the default implementation of parse content. DocumentParser will call parseContent method. 
        /// After that, it will check if it has next document parser or not. If it has next document 
        /// parser, calls parse method of next document parser.
        /// </summary>
        /// <param name="doc"></param>
        public bool parse(System.Windows.Forms.HtmlDocument doc)
        {
            bool bReturn = parseContent(doc);
            if (m_Next != null)
            {
                //m_Next.setDataTable(getDataTable());
                bReturn = bReturn | m_Next.parse(doc);
            }
            return bReturn;
        }
        /// <summary>
        /// parse content.
        /// the default implementation of parse content. DocumentParser will call parseContent method. 
        /// After that, it will check if it has next document parser or not. If it has next document 
        /// </summary>
        /// <param name="doc"></param>
        public bool parse(mshtml.IHTMLDocument2 doc)
        {
            bool bReturn = parseContent(doc);
            if (m_Next != null)
            {
                //m_Next.setDataTable(getDataTable());
                bReturn = bReturn | m_Next.parse(doc);
            }
            return bReturn;
        }
        /// <summary>
        /// set the data table associated with this document parser.
        /// </summary>
        /// <param name="table"></param>
        public void setDataTable(System.Data.DataTable table)
        {
            m_DataTable = table;
        }
        /// <summary>
        /// get the data table associated with this document parser.
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable getDataTable()
        {
            return m_DataTable;
        }
        /// <summary>
        /// set the relation processor associated with this document parser.
        /// </summary>
        /// <param name="relation"></param>
        public void setRelationProcessor(IRelationProcessor relation)
        {
            m_RelationProcessor = relation;
        }
        /// <summary>
        /// get the data table associated with this document parser.
        /// </summary>
        /// <returns></returns>
        public IRelationProcessor getRelationProcessor()
        {
            return m_RelationProcessor;
        }

        /// <summary>
        /// RowParsed event is fired when a document parser has parsed a data row.
        /// </summary>
        public event RowParsedEvent RowParsed;

        #endregion

        #region IChain member
        /// <summary>
        /// get next document parser.
        /// </summary>
        /// <returns></returns>
        public IChain next()
        {
            return m_Next;
        }
        /// <summary>
        /// set next document parser. This method only accepts instances of IDocumentParser type as its argument.
        /// </summary>
        /// <param name="next"></param>
        public void setNext(IChain next)
        {
            if (next is IDocumentParser)
                m_Next = (IDocumentParser)next;
        }

        #endregion
    }
}
