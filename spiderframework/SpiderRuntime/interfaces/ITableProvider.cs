using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// ITableProvider creates tables for runtime.
    /// </summary>
    public interface ITableProvider : IRuntimeUnit
    {
        /// <summary>
        /// create a table.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        DataTable createTable(XmlElement elm);
    }
}
