using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// A IFlowResourceProvider provides all resources created or used by a flow, including IDocumentParser, 
    /// IElementRowContentParser and IRowFilter. 
    /// <br/>
    /// IFlowResourceProvider 是一個 Flow 內所有的 Resource Provider ，它會包含 IDocumentParser、
    /// IElementRowContentParser、及 IRowFilter 的物件。
    /// </summary>
    public interface IFlowResourceProvider
    {
        /// <summary>
        /// get a document parser by id that is defined in configuration file. 
        /// <br/>
        /// 以 id 來搜尋 IDocumentParser 。這裡所用的 id 是 IDocumentParserProvider 在產生 
        /// IDocumentParser 的時候，XmlElement 中所指定的 id 。
        /// </summary>
        /// <param name="id">the id in XmlElement</param>
        /// <returns>IDocumentParser</returns>
        IDocumentParser getDocumentParser(string id);
        /// <summary>
        /// get all IDocumentParser instances.
        /// </summary>
        /// <returns></returns>
        IDocumentParser[] getDocumentParsers();

        /// <summary>
        /// get a row content parser by id that is defined in configuration file.
        /// <br/>
        /// 以 id 來搜尋 IElementRowContentParser 。這裡所用的 id 是 IRowContentParserProvider 在產生
        /// IElementRowContentParser 的時候，XmlElement 中所指定的 id 。
        /// </summary>
        /// <param name="id">the id in XmlElement</param>
        /// <returns>IElementRowContentParser</returns>
        IElementRowContentParser getRowContentParser(string id);
        /// <summary>
        /// get all row content parsers.
        /// </summary>
        /// <returns></returns>
        IElementRowContentParser[] getRowContentParsers();

        /// <summary>
        /// get a row filter by id that is defined in configuration file.
        /// <br/>
        /// 以 id 來搜尋 IRowFilter 。這裡所用的 id 是 IRowFilterProvider 在產生 IRowFilter 的時候，
        /// XmlElement 中所指定的 id。
        /// </summary>
        /// <param name="id">the id in XmlElement</param>
        /// <returns>IRowFilter</returns>
        IRowFilter getRowFilter(string id);
        /// <summary>
        /// get all IRowFilters。
        /// </summary>
        /// <returns></returns>
        IRowFilter[] getRowFilters();
    }
}
