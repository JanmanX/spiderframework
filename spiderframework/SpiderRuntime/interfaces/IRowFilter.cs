using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// IRowFilter checks if a data row is a valid data row.<br/>
    /// IRowFilter 會用來過濾每個 DataRow 是否為可接受的 DataRow 。
    /// </summary>
    public interface IRowFilter
    {
        /// <summary>
        /// This function will be called in RowParsed event listener of IDocumentParser to check if the data row
        /// parsed by IDocumentParser is a valid data row.</br>
        /// 這個 function 會被接上 IDocumentParser 的 RowParsed 事件，用來過濾 DataRow 是否為可法的 DataRow 。
        /// </summary>
        /// <param name="sender">sender should be a IDocumentParser</param>
        /// <param name="row">the DataRow parsed by IDocumentParser.</param>
        /// <returns>returns true, if DataRow isvalid, otherwise false.</returns>
        /// <see cref="fox.spider.IDocumentParser"/>
        bool filter(object sender, DataRow row);
    }
}
