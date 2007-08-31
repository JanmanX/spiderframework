using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider
{
    /// <summary>
    /// IWebPageTurner dispatches an event named PagerNotFound with this type while
    /// IWebPageTurner can't turn page currently.
    /// </summary>
    /// <param name="sender">an object who sends this event.</param>
    /// <param name="pagingInfo">the pagingInfo used by IWebPageTurner.</param>
    public delegate void WebPageTurnerUnpagedEvent(object sender, object pagingInfo);
    /// <summary>
    /// IWebPageTurner is responsible for turing to next page.
    /// </summary>
    public interface IWebPageTurner
    {
        /// <summary>
        /// turn to next page.
        /// </summary>
        /// <param name="doc">Html Document</param>
        /// <returns>returns true, if it turns correctly. </returns>
        bool nextPage(System.Windows.Forms.HtmlDocument doc);
        /// <summary>
        /// turn to next page.
        /// </summary>
        /// <param name="doc">Html Document</param>
        /// <returns>returns true, if it turns correctly. </returns>
        bool nextPage(mshtml.IHTMLDocument2 doc);
        /// <summary>
        /// sets the paging info used by IWebPageTurner.
        /// </summary>
        /// <param name="obj"></param>
        void setPagingInfo(object obj);
        /// <summary>
        /// gets the paging info used by IWebPageTurner.
        /// </summary>
        /// <returns></returns>
        object getPagingInfo();
        /// <summary>
        /// IWebPageTurner dispatches this event while it can't turn page.
        /// </summary>
        event WebPageTurnerUnpagedEvent PageNotFound;
    }
}
