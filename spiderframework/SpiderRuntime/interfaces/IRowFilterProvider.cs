using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// IRowFilterProvider creates a IRowFilter by a XmlElement.<br/>
    /// IRowFilterProvider 會使用 XmlElement 為參數來建立專屬的 IRowFilter 。
    /// </summary>
    public interface IRowFilterProvider : IFlowUnit
    {
        /// <summary>
        /// create row filter
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        IRowFilter createRowFilter(XmlElement elm);
    }
}
