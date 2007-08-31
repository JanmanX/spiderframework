using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider
{
    /// <summary>
    /// IElementRowContentParser is responsible for analyzing IHTMLDOMNode 
    /// to retrieve data for each data field.
    /// <br/>
    /// IDocumentParser is responsible for finding out a data row from HTML Document, 
    /// and IElementRowContentParser is responsible for finding data field from IHTMLDOMNode 
    /// </summary>
    public interface IElementRowContentParser : IChain
    {
        /// <summary>
        /// parses a IHTMLDOMNode to find data out and stores in the current data row.
        /// </summary>
        /// <param name="elem"></param>
        void parse(mshtml.IHTMLDOMNode elem);
        /// <summary>
        /// sets the data row which stores parsed data.
        /// </summary>
        /// <param name="row"></param>
        void setDataRow(System.Data.DataRow row);
        /// <summary>
        /// get the data row which stores parsed data.
        /// </summary>
        /// <returns></returns>
        System.Data.DataRow getDataRow();
    }
}
