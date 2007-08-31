using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// LinkedWebPageTurner uses a path to find a node. If it finds, it will click it. 
    /// 
    /// <code>
    ///        LinkedWebPageTurner oTurner = new LinkedWebPageTurner();
    ///        oTurner.setPagingInfo("/BODY/DIV[0]/A[1]");
    /// </code>
    /// </summary>
    public class LinkedWebPageTurner : IWebPageTurner
    {
        private string m_PagingInfo;


        #region IWebPageTurner member
        /// <summary>
        /// turn to next page
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public bool nextPage(System.Windows.Forms.HtmlDocument doc)
        {
            return nextPage((IHTMLDocument2) doc.DomDocument);
        }
        /// <summary>
        /// turn to next page
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public bool nextPage(IHTMLDocument2 doc)
        {

            IHTMLDOMNode oElem = SpiderPath.selectSingleNode(doc, m_PagingInfo);
            if (oElem != null && oElem is IHTMLElement)
            {
                ((IHTMLElement)oElem).click();
                return true;
            }
            else if (PageNotFound != null)
            {
                PageNotFound(this, m_PagingInfo);
            }
            return false;
        }
        /// <summary>
        /// Paging Info must be a spider path.
        /// </summary>
        /// <param name="obj"></param>
        public void setPagingInfo(object obj)
        {
            if(obj is string)
                m_PagingInfo = (string) obj;
        }
        /// <summary>
        /// it is a string.
        /// </summary>
        /// <returns></returns>
        public object getPagingInfo()
        {
            return m_PagingInfo;
        }
        /// <summary>
        /// dispatches when page not found
        /// </summary>
        public event WebPageTurnerUnpagedEvent PageNotFound;
        #endregion
    }
}
