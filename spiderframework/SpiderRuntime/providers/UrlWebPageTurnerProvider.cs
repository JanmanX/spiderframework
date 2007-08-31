using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider;
using fox.spider.runtime.interfaces;
using fox.spider.runtime.constants;
using fox.spider.runtime.pageturners;


namespace fox.spider.runtime.providers
{
    /// <summary>
    /// A provider creates a UrlWebPageTurner by an XmlElement.
    /// 建立 UrlWebPageTurner 的實作
    /// </summary>
    public class UrlWebPageTurnerProvider : AbstractProvider, IWebPageTurnerProvider
    {
        #region IWebPageTurnerProvider Members

        public IWebPageTurner createWebPageTurner(XmlElement elm)
        {
            IWebPageTurner oReturn = null;
            XmlNodeList aList = elm.GetElementsByTagName("url", SpiderRuntimeConstants.DefaultNamespace);
            if (aList.Count > 0)
            {
                UrlPageTurner oTurner = new UrlPageTurner();
                foreach (XmlNode n in aList)
                {
                    string sTxt = ((XmlElement)n).InnerText;
                    oTurner.addUrl(sTxt);
                }
                oReturn = oTurner;
            }
            return oReturn;
        }

        #endregion
    }
}
