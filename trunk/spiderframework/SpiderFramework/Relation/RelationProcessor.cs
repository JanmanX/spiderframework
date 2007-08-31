using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider
{
    /// <summary>
    /// RelationProcessor is an abstract implementation of IRelationProcessor. RelationProcessor
    /// implements many of interfaces of IRelationProcessor. It publishes an abstract method named
    /// bind for its descendents.
    /// </summary>
    public abstract class RelationProcessor : IRelationProcessor
    {
        
        private IRelationProcessor m_Next;
        private System.Data.DataRow m_ContextRow;
        private mshtml.IHTMLDOMNode m_ContextNode;

        /// <summary>
        /// links parent node and parent row with child node and child row
        /// </summary>
        /// <param name="node"></param>
        /// <param name="row"></param>
        abstract protected void bind(mshtml.IHTMLDOMNode node, System.Data.DataRow row);


        #region IChain members
        /// <summary>
        /// returns next IRelationProcessor
        /// </summary>
        /// <returns></returns>
        public IChain next()
        {
            return m_Next;
        }
        /// <summary>
        /// sets next IRelationProcessor
        /// </summary>
        /// <param name="next"></param>
        public void setNext(IChain next)
        {
            if (next is IRelationProcessor)
                m_Next = (IRelationProcessor)next;
        }

        #endregion

        #region IRelationProcessor members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="childNode"></param>
        /// <param name="childRow"></param>
        public void bindRelation(mshtml.IHTMLDOMNode childNode, System.Data.DataRow childRow)
        {
            bind(childNode, childRow);
            if (m_Next != null)
                m_Next.bindRelation(childNode, childRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public void setContextRow(System.Data.DataRow parent)
        {
            m_ContextRow = parent;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Data.DataRow getContextRow()
        {
            return m_ContextRow;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentNode"></param>
        public void setContextNode(mshtml.IHTMLDOMNode parentNode)
        {
            m_ContextNode = parentNode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public mshtml.IHTMLDOMNode getContextNode()
        {
            return m_ContextNode;
        }

        #endregion
    }
}
