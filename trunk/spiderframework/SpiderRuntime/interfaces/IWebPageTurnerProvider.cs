using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// IWebPageTurnerProvider creates web page turner instance for runtime.
    /// IWebPageTurnerProvider 會使用 XmlElement 為參數來產生專屬的 IWebPageTurner 。
    /// </summary>
    public interface IWebPageTurnerProvider : IRuntimeUnit
    {
        /// <summary>
        /// to create a web page turner instance by XmlElement
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        IWebPageTurner createWebPageTurner(XmlElement elm);
    }
}
