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
    /// ��@ IRuntineUnit ���⹳�� Provider �A�Ҧ��� Provider ����ĳ�~�ӳo�Ӫ���C
    /// </summary>
    public abstract class AbstractProvider : IRuntimeUnit
    {
        /// <summary>
        /// This is a convenient function. It searches for a direct child with specified local name, which is n.
        /// If it finds, returns innerText of child. Otherwise, return null.
        /// �j�M e ���U�W�s n ��Child Element�� InnerText�A�p�G�䤣�� Child Element �ɡA�h�^�� null�C
        /// </summary>
        /// <param name="e">Parent Element</param>
        /// <param name="n">Child Element ���W��(local name)</param>
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
