using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// IRowFilter checks if a data row is a valid data row.<br/>
    /// IRowFilter �|�ΨӹL�o�C�� DataRow �O�_���i������ DataRow �C
    /// </summary>
    public interface IRowFilter
    {
        /// <summary>
        /// This function will be called in RowParsed event listener of IDocumentParser to check if the data row
        /// parsed by IDocumentParser is a valid data row.</br>
        /// �o�� function �|�Q���W IDocumentParser �� RowParsed �ƥ�A�ΨӹL�o DataRow �O�_���i�k�� DataRow �C
        /// </summary>
        /// <param name="sender">sender should be a IDocumentParser</param>
        /// <param name="row">the DataRow parsed by IDocumentParser.</param>
        /// <returns>returns true, if DataRow isvalid, otherwise false.</returns>
        /// <see cref="fox.spider.IDocumentParser"/>
        bool filter(object sender, DataRow row);
    }
}
