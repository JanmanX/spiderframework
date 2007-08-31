using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider
{
    /// <summary>
    /// IRelationProcessor analyzes parent or child data row and IHTMLDOMNode to configure 
    /// data relationship between these two data row. Such as a foreign key processor  
    /// maintains the foreign key relationship between two data row.
    /// <br/>
    /// IRelationProcessor may change some field in parent or child data row.
    /// </summary>
    public interface IRelationProcessor : IChain
    {
        /// <summary>
        /// binds the relationships between parent and child.
        /// </summary>
        /// <param name="childNode">a IHTMLDOMNode which is parsed by IDocumentParser.</param>
        /// <param name="childRow">a DataRow which contains data parsed by IElementRowContentParser.</param>
        void bindRelation(mshtml.IHTMLDOMNode childNode, System.Data.DataRow childRow);
        /// <summary>
        /// sets the parent data row.
        /// </summary>
        /// <param name="parent"></param>
        void setContextRow(System.Data.DataRow parent);
        /// <summary>
        /// gets the parent data row.
        /// </summary>
        /// <returns></returns>
        System.Data.DataRow getContextRow();
        /// <summary>
        /// sets the parent IHTMLDOMNode.
        /// </summary>
        /// <param name="parentNode"></param>
        void setContextNode(mshtml.IHTMLDOMNode parentNode);
        /// <summary>
        /// gets the parent IHTMLDOMNode.
        /// </summary>
        /// <returns></returns>
        mshtml.IHTMLDOMNode getContextNode();
    }
}
