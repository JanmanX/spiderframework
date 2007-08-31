using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace fox.spider
{
    /// <summary>
    /// IDocumentParser dispatches an event named RowParsed with this delegation, 
    /// while a data row is parsed. User may use this event dispatch delegation to check
    /// whether data is correct or not.
    /// </summary>
    /// <param name="sender">The object who sends this event.</param>
    /// <param name="row">a data row object parsed by sender. </param>
    /// <returns>if data in DataRow is correct, return true. Otherwise it returns false.</returns>
    /// <seealso cref="fox.spider.IDocumentParser"/>
    public delegate bool RowParsedEvent(object sender, DataRow row);
    /// <summary>
    /// IDocumentParser implementor is responsible for analyzing HTML Document to find out 
    /// data element which is a IHTMLDOMNode. Each data element maps to a data row. While
    /// a data element is parsed, IDocumentParser delegates data field analyzing to 
    /// IElementRowContentParser.
    /// <br/>
    /// IDocumentParser is responsible for finding out a data row from HTML Document, 
    /// and IElementRowContentParser is responsible for finding data field from IHTMLDOMNode
    /// </summary>
    /// <seealso cref="fox.spider.IElementRowContentParser"/>
    public interface IDocumentParser : IChain
    {
        /// <summary>
        /// analyze HTML Document in HtmlDocument type.
        /// </summary>
        /// <param name="doc">a HTML Document needs to parse.</param>
        bool parse(HtmlDocument doc);
        /// <summary>
        /// analyze HTML Document in mshtml.IHTMLDocument2 type. This is more preferrable.
        /// </summary>
        /// <param name="doc">a HTML Document needs to parse.</param>
        bool parse(mshtml.IHTMLDocument2 doc);

        /// <summary>
        /// set the relation processor which is used to process relationships between tables.
        /// </summary>
        /// <param name="relation">Relation Processor</param>
        void setRelationProcessor(IRelationProcessor relation);
        /// <summary>
        /// get the relation processor.
        /// </summary>
        /// <returns>the relation processor used by IDocumentParser currently.</returns>
        IRelationProcessor getRelationProcessor();
        /// <summary>
        /// set data table used by IDocumentParser. While a data element is parsed, IDocumentParser
        /// creates a data row of this data table.
        /// </summary>
        /// <param name="table">Table</param>
        void setDataTable(DataTable table);
        /// <summary>
        /// get the data table used by IDocumentParser.
        /// </summary>
        /// <returns></returns>
        DataTable getDataTable();
        /// <summary>
        /// While a data element is parsed, IDocumentParser dispatches this event.
        /// </summary>
        event RowParsedEvent RowParsed;
    }
}
