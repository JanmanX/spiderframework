using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// IRowFilterProvider creates a IRowFilter by a XmlElement.<br/>
    /// IRowFilterProvider �|�ϥ� XmlElement ���Ѽƨӫإ߱M�ݪ� IRowFilter �C
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
