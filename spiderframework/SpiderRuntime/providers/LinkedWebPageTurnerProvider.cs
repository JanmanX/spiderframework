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
    /// A provider creates a LinkedWebPageTurner by an XmlElement.
    /// 建立 LinkedWebPageTurner 的實作
    /// </summary>
    public class LinkedWebPageTurnerProvider : AbstractProvider, IWebPageTurnerProvider
    {
        #region IWebPageTurnerProvider Members
        /// <summary>
        /// create a LinkedWebPageTurner.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public IWebPageTurner createWebPageTurner(XmlElement elm)
        {
            IWebPageTurner oReturn = null;

            XmlNodeList oList=elm.GetElementsByTagName("Path", SpiderRuntimeConstants.DefaultNamespace);
            if (oList.Count > 0)
            {
                string sPath = ((XmlElement)oList[0]).InnerText;
                LinkedWebPageTurner oTurner = new LinkedWebPageTurner();
                oTurner.setPagingInfo(sPath);
                oReturn = oTurner;
            }
            return oReturn;
        }

        #endregion
    }
}
