using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider
{
    /// <summary>
    /// ElementRowContentParser is an abstract implementation of IElementRowCOntentParser. 
    /// It implements all interfaces of IElementRowContentParser and propose a parseContent 
    /// method for its decendents. It is strongly encouraged to extend this class while you 
    /// want to create a new row content parser.
    /// </summary>
    public abstract class ElementRowContentParser : MarshalByRefObject, IElementRowContentParser
    {
        private IElementRowContentParser m_Next;
        private System.Data.DataRow m_DataRow;
        private System.Collections.Hashtable m_ColMappingHash = new System.Collections.Hashtable();

        /// <summary>
        /// </summary>
        [Obsolete("Obsolete property. You should use proprietary properties of each row content parser.")]
        public System.Collections.Hashtable ColumnMapping
        {
            get { return m_ColMappingHash; }
            set { m_ColMappingHash = value; }
        }
        /// <summary>
        /// analyze IHTMLDOMNode and fill some columns in data row. 
        /// </summary>
        /// <param name="node"></param>
        abstract protected void parseContent(mshtml.IHTMLDOMNode node);

        #region IElementRowContentParser member
        /// <summary>
        /// parse html node. If it has next row content parser, this method will pass data row, obtained by 
        /// previous row content parser or document parser, to next row content parser and call parse method
        /// of next row content parser.
        /// </summary>
        /// <param name="elem"></param>
        public void parse(mshtml.IHTMLDOMNode elem)
        {
            parseContent(elem);
            if (m_Next != null)
            {
                m_Next.setDataRow(getDataRow());
                m_Next.parse(elem);
            }
        }
        /// <summary>
        /// set data row
        /// </summary>
        /// <param name="row"></param>
        public void setDataRow(System.Data.DataRow row)
        {
            m_DataRow = row;
        }
        /// <summary>
        /// get data row
        /// </summary>
        /// <returns></returns>
        public System.Data.DataRow getDataRow()
        {
            return m_DataRow;
        }

        /// <summary>
        /// a convenient method for setting value of a data column.
        /// </summary>
        /// <param name="f">f may be a integer or a string. If f is a integer, f is used as an index. If f is 
        /// a string, f is used as a column name</param>
        /// <param name="v"></param>
        protected void setFieldValue(object f, object v)
        {
            if (f is int)
            {
                getDataRow()[(int)f] = v;
            }
            else if (f is string)
            {
                getDataRow()[(string)f] = v;
            }
        }

        #endregion

        #region IChain member
        /// <summary>
        /// get next row content parser.
        /// </summary>
        /// <returns></returns>
        public IChain next()
        {
            return m_Next;
        }
        /// <summary>
        /// set next row content parser.
        /// </summary>
        /// <param name="next"></param>
        public void setNext(IChain next)
        {
            if (next is IElementRowContentParser)
                m_Next = (IElementRowContentParser)next;
        }

        #endregion
    }
}
