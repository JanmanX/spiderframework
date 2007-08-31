using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider.runtime.interfaces;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// AbstractProvider is an abstract implementation of IRuntimeUnit. All classes running in SpiderRuntime
    /// should descend from this class.
    /// 實作 IRuntineUnit 的抽像化 Provider ，所有的 Provider 都建議繼承這個物件。
    /// </summary>
    public abstract class AbstractProvider : IRuntimeUnit
    {
        /// <summary>
        /// This is a convenient function. It searches for a direct child with specified local name, which is n.
        /// If it finds, returns innerText of child. Otherwise, return null.
        /// 搜尋 e 底下名叫 n 的Child Element的 InnerText，如果找不到 Child Element 時，則回傳 null。
        /// </summary>
        /// <param name="e">Parent Element</param>
        /// <param name="n">Child Element 的名稱(local name)</param>
        /// <returns></returns>
        protected string getChildElementText(XmlElement e, string n)
        {
            foreach (XmlNode node in e.ChildNodes)
            {
                if (node.LocalName.Equals(n) && node.NodeType == XmlNodeType.Element)
                {
                    return ((XmlElement)node).InnerText;
                }
            }
            return null;
        }

        private IGlobalResourceProvider m_GlobalResource;

        /// <summary>
        /// Global Resource
        /// </summary>
        protected IGlobalResourceProvider GlobalResource
        {
            get { return m_GlobalResource; }
            set { m_GlobalResource = value; }
        }

        #region IRuntimeUnit Members
        /// <summary>
        /// set global resource
        /// </summary>
        /// <param name="p"></param>
        public void setGlobalResource(IGlobalResourceProvider p)
        {
            GlobalResource = p;
        }

        #endregion
    }
}
