using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// A IDocumentParser generator. IDocumentParserProvider uses a XmlElement as argument to create a IDocumentParser and 
    /// configure it.
    /// 
    /// </summary>
    /// <see cref="fox.spider.IDocumentParser"/>
    public interface IDocumentParserProvider : IFlowUnit
    {
        /// <summary>
        /// create a IDocumentParser
        /// </summary>
        /// <param name="elm">runtime argument</param>
        /// <returns> </returns>
        IDocumentParser createDocumentParser(XmlElement elm);
    }
}
