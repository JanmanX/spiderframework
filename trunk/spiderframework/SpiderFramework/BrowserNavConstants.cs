using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider
{
    /// <summary>
    /// BrowserNavConstants is contants used in Browser.Navigate.
    /// </summary>
    public enum BrowserNavConstants
    {
        /// <summary>
        /// Open the resource or file in a new window.
        /// </summary>
        navOpenInNewWindow = 0x1,
        /// <summary>
        /// Do not add the resource or file to the history list. The new page replaces the current page in the list.
        /// </summary>
        navNoHistory = 0x2,
        /// <summary>
        /// Not currently supported.
        /// </summary>
        navNoReadFromCache = 0x4,
        /// <summary>
        /// Not currently supported.
        /// </summary>
        navNoWriteToCache = 0x8,
        /// <summary>
        /// If the navigation fails, the autosearch functionality attempts to navigate common root domains (.com, .edu, and so on). If this also fails, the URL is passed to a search engine.
        /// </summary>
        navAllowAutosearch = 0x10,
        /// <summary>
        /// Causes the current Explorer Bar to navigate to the given item, if possible. 
        /// </summary>
        navBrowserBar = 0x20,
        /// <summary>
        /// If the navigation fails when a hyperlink is being followed, this constant specifies that the resource should then be bound to the moniker using the BINDF_HYPERLINK flag.
        /// </summary>
        navHyperlink = 0x40
    } 
}
