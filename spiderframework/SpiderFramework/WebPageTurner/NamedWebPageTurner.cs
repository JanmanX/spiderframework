using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// NamedWebPageTurner will click an element with the specified content. 
    /// </summary>
    public class NamedWebPageTurner : IWebPageTurner
    {

        private NamedWebPagingInfo m_Info;

        #region IWebPageTurner member
        /// <summary>
        /// nextPage
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public bool nextPage(System.Windows.Forms.HtmlDocument doc)
        {
            return nextPage((IHTMLDocument2)doc.DomDocument);
        }
        /// <summary>
        /// nextPage
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public bool nextPage(IHTMLDocument2 doc)
        {

            IHTMLDOMNode oElem = SpiderPath.selectSingleNode(doc, m_Info.StartPath);
            if (oElem == null)
            {
                return false;
            }
            IHTMLDOMNode oIElement = oElem.firstChild;
            while (oIElement != null)
            {
                if (oIElement is IHTMLElement && ((IHTMLElement)oIElement).innerText != null &&
                    ((IHTMLElement)oIElement).innerText.Trim().Equals(m_Info.PagerName))
                {
                    ((IHTMLElement)oIElement).click();
                    return true;
                }
                oIElement = ((IHTMLDOMNode)oIElement).nextSibling;
            }
            if (PageNotFound != null)
            {
                PageNotFound(this, m_Info);

            }
            return false;
        }
        /// <summary>
        /// it is an instance of NamedWebPagingInfo
        /// </summary>
        /// <param name="obj"></param>
        public void setPagingInfo(object obj)
        {
            if (obj is NamedWebPagingInfo)
            {
                m_Info = (NamedWebPagingInfo)obj;
            }
        }
        /// <summary>
        /// it is an instance of NamedWebPagingInfo
        /// </summary>
        /// <returns></returns>
        public object getPagingInfo()
        {
            return m_Info;
        }
        /// <summary>
        /// 
        /// </summary>
        public event WebPageTurnerUnpagedEvent PageNotFound;

        #endregion
    }

    /// <summary>
    /// the info stores starting path and the content of a elment which will be clicked.
    /// </summary>
    public class NamedWebPagingInfo
    {
        private string m_StartPath;
        /// <summary>
        /// 
        /// </summary>
        public string StartPath
        {
            get { return m_StartPath; }
            set { m_StartPath = value; }
        }

        private string m_PagerName;
        /// <summary>
        /// 
        /// </summary>
        public string PagerName
        {
            get { return m_PagerName; }
            set { m_PagerName = value; }
        }
    }
}
