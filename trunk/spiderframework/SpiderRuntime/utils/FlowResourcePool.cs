using System;
using System.Collections.Generic;
using System.Text;
using fox.spider.runtime.interfaces;

namespace fox.spider.runtime.utils
{
    /// <summary>
    /// The default implementation of IFlowResourceProvider.
    /// IFlowResourceProvider 的預設實作。
    /// </summary>
    public class FlowResourcePool : IFlowResourceProvider
    {
        private Dictionary<string, IDocumentParser> m_DocParserHash = new Dictionary<string, IDocumentParser>();
        private Dictionary<string, IElementRowContentParser> m_RowParserHash = new Dictionary<string, IElementRowContentParser>();
        private Dictionary<string, IRowFilter> m_RowFilterHash = new Dictionary<string, IRowFilter>();

        /// <summary>
        /// add a document parser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dp"></param>
        public void addDocumentParser(string id, IDocumentParser dp)
        {
            m_DocParserHash.Add(id, dp);
        }
        /// <summary>
        /// add a row content parser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rp"></param>
        public void addRowContentParser(string id, IElementRowContentParser rp)
        {
            m_RowParserHash.Add(id, rp);
        }
        /// <summary>
        /// add a row filter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rp"></param>
        public void addRowFilter(string id, IRowFilter rp)
        {
            m_RowFilterHash.Add(id, rp);
        }
        /// <summary>
        /// remove a document parser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool removeDocumentParser(string id)
        {
            return m_DocParserHash.Remove(id);
        }
        /// <summary>
        /// remove a row content parser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool removeRowContentParser(string id)
        {
            return m_RowParserHash.Remove(id);
        }
        /// <summary>
        /// remove a row filter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool removeRowFilter(string id)
        {
            return m_RowFilterHash.Remove(id);
        }

        #region IFlowResourceProvider Members
        /// <summary>
        /// get document parser by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IDocumentParser getDocumentParser(string id)
        {
            if (m_DocParserHash.ContainsKey(id))
            {
                return m_DocParserHash[id];
            }
            return null;
            
        }
        /// <summary>
        /// get all document parsers
        /// </summary>
        /// <returns></returns>
        public IDocumentParser[] getDocumentParsers()
        {
            IDocumentParser[] aReturn=new IDocumentParser[m_DocParserHash.Count];
            m_DocParserHash.Values.CopyTo(aReturn, 0);
            return aReturn;
        }
        /// <summary>
        /// get row content parser by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IElementRowContentParser getRowContentParser(string id)
        {
            if (m_RowParserHash.ContainsKey(id))
            {
                return m_RowParserHash[id];
            }
            return null;
        }
        /// <summary>
        /// get all row content parsers.
        /// </summary>
        /// <returns></returns>
        public IElementRowContentParser[] getRowContentParsers()
        {
            IElementRowContentParser[] aReturn = new IElementRowContentParser[m_RowParserHash.Count];
            m_RowParserHash.Values.CopyTo(aReturn, 0);
            return aReturn;
        }
        /// <summary>
        /// get row filter by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IRowFilter getRowFilter(string id)
        {
            if (m_RowFilterHash.ContainsKey(id))
            {
                return m_RowFilterHash[id];
            }
            return null;
        }
        /// <summary>
        /// get row filters.
        /// </summary>
        /// <returns></returns>
        public IRowFilter[] getRowFilters()
        {
            IRowFilter[] aReturn = new IRowFilter[m_RowFilterHash.Count];
            m_RowFilterHash.Values.CopyTo(aReturn, 0);
            return aReturn;
        }

        #endregion
    }
}
