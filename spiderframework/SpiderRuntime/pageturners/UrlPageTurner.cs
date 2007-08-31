using System;
using System.Collections.Generic;
using System.Text;
using fox.spider;

namespace fox.spider.runtime.pageturners
{
    /// <summary>
    /// UrlPageTurner turns page by a list of url. It likes DataTablePageTurner but more static. 
    /// Instead of DataTable, you need to add a static url to UrlPageTurner. With a list of url, 
    /// UrlPageTurner turns page from first added item to last added item.
    /// 
    /// 
    /// UrlPageTurner 使用一個 Queue 來記錄一連串的 url ，每次呼叫 nextPage 的時候，它會將
    /// 畫面轉到下一個 url 。
    /// </summary>
    public class UrlPageTurner : IWebPageTurner
    {
        private Queue<string> m_UrlQueue = new Queue<string>();

        /// <summary>
        /// add next url.
        /// </summary>
        /// <param name="url"></param>
        public void addUrl(string url)
        {
            m_UrlQueue.Enqueue(url);
        }

        /// <summary>
        /// peek next url
        /// </summary>
        /// <returns></returns>
        public string peekUrl()
        {
            return m_UrlQueue.Peek();
        }

        /// <summary>
        /// return an array of urls which haven't navigated.
        /// </summary>
        /// <returns></returns>
        public string[] urls()
        {
            return m_UrlQueue.ToArray();
        }

        /// <summary>
        /// get next url. 
        /// </summary>
        /// <returns></returns>
        protected string nextUrl()
        {
            if (m_UrlQueue.Count > 0)
            {
                return m_UrlQueue.Dequeue();
            }
            return null;
        }


        #region IWebPageTurner Members
        /// <summary>
        /// turn page
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public bool nextPage(System.Windows.Forms.HtmlDocument doc)
        {
            string sUrl = nextUrl();
            if (sUrl != null)
            {
                doc.Window.Navigate(sUrl);
                return true;
            }
            if (PageNotFound != null)
            {
                PageNotFound(this, sUrl);
            }
            return false;
        }
        /// <summary>
        /// turn page
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public bool nextPage(mshtml.IHTMLDocument2 doc)
        {
            string sUrl = nextUrl();
            if (sUrl != null)
            {
                doc.parentWindow.navigate(sUrl);
                return true;
            }
            if (PageNotFound != null)
            {
                PageNotFound(this, sUrl);
            }
            return false;
        }
        /// <summary>
        /// useless currently. 
        /// </summary>
        /// <param name="obj"></param>
        public void setPagingInfo(object obj)
        {
            
        }

        /// <summary>
        /// useless currently. 
        /// </summary>
        /// <returns></returns>
        public object getPagingInfo()
        {
            return null;
        }

        /// <summary>
        /// fires this event when UrlPageTurner has no more url.
        /// </summary>
        public event WebPageTurnerUnpagedEvent PageNotFound;

        #endregion
    }
}
