using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider;
using fox.spider.runtime.interfaces;
using fox.spider.runtime.constants;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// A provider creates a NamedWebPageTurner by an XmlElement.
    /// 建立 NamedWebPageTurner 的實作
    /// </summary>
    public class NamedWebPageTurnerProvider : AbstractProvider, IWebPageTurnerProvider
    {
        #region IWebPageTurnerProvider Members
        /// <summary>
        /// creates a NamedWebPageTurner.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public IWebPageTurner createWebPageTurner(XmlElement elm)
        {
            IWebPageTurner oReturn = null;

            XmlNodeList oList = elm.GetElementsByTagName("Path", SpiderRuntimeConstants.DefaultNamespace);
            XmlNodeList oList2 = elm.GetElementsByTagName("Text", SpiderRuntimeConstants.DefaultNamespace);
            if (oList.Count > 0 && oList2.Count > 0)
            {
                string sPath = ((XmlElement)oList[0]).InnerText;
                string sText = ((XmlElement)oList2[0]).InnerText;

                NamedWebPageTurner oTurner = new NamedWebPageTurner();
                NamedWebPagingInfo oInfo = new NamedWebPagingInfo();
                oInfo.PagerName = sText;
                oInfo.StartPath = sPath;

                oTurner.setPagingInfo(oInfo);

                oReturn = oTurner;
            }
            return oReturn;
        }

        #endregion
    }
}
