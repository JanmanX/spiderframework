using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// IRowContentParserProvider creates a IElementRowContentParser by XmlElement.
    /// </summary>
    /// <see cref="fox.spider.IElementRowContentParser"/>
    public interface IRowContentParserProvider : IFlowUnit
    {
        IElementRowContentParser createRowContentParser(XmlElement elm);
    }
}
