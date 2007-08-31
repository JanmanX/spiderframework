using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// a filter used by searchChildElementByContent
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public delegate bool ElementFilter(IHTMLDOMNode node);

    /// <summary>
    /// Spider Utilities
    /// </summary>
    public class SpiderUtilities
    {
        /// <summary>
        /// search a child node by its content. This method will search all decendents of elem to find a node whose
        /// content is txt. The filter may be used to filter the node with the same content which is not we want.
        /// </summary>
        /// <param name="txt">text</param>
        /// <param name="elem">searching root</param>
        /// <param name="filter">node filter</param>
        /// <returns>a node with txt as its content, or null while not found.</returns>
        public static IHTMLDOMNode searchChildElementByContent(string txt, IHTMLElement elem, ElementFilter filter)
        {
            if (!elem.innerText.Trim().ToLower().Contains(txt.ToLower()))
            {
                return null;
            }

            IHTMLDOMNode oNode = (IHTMLDOMNode)elem;
            IHTMLDOMNode oChild = oNode.firstChild;

            while (oChild != null)
            {
                if (oChild.nodeType == 1 && ((IHTMLElement)oChild).innerText != null &&
                    ((IHTMLElement)oChild).innerText.Trim().ToLower().Equals(txt.ToLower()))
                {
                    if (filter == null || (filter != null && filter(oChild)))
                    {
                        return oChild;
                    }
                    else
                    {
                        IHTMLDOMNode oChildReturn = searchChildElementByContent(txt, (IHTMLElement)oChild, filter);
                        if (oChildReturn != null)
                        {
                            return oChildReturn;
                        }
                        else
                        {
                            oChild = oChild.nextSibling;
                        }
                    }
                }
                else if (oChild.nodeType == 1 && ((IHTMLElement)oChild).innerText != null &&
                    ((IHTMLElement)oChild).innerText.Trim().ToLower().Contains(txt.ToLower()))
                {
                    IHTMLDOMNode oChildReturn = searchChildElementByContent(txt, (IHTMLElement)oChild, filter);
                    if (oChildReturn != null)
                    {
                        return oChildReturn;
                    }
                    else
                    {
                        oChild = oChild.nextSibling;
                    }
                }
                else
                {
                    oChild = oChild.nextSibling;
                }
            }
            return null;
        }
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="attr"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        [Obsolete("Use RowContentParserUtilities instead")]
        public static AttributeRowContentParser createAttributeRowContentParser(string path, string attr, string col)
        {

            return RowContentParserUtilities.createAttributeRowContentParser(path, attr, col);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="col"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        [Obsolete("Use RowContentParserUtilities instead")]
        public static XPathRowContentParser createXPathRowContentParser(string[] path, string[] col, bool trim)
        {
            return RowContentParserUtilities.createXPathRowContentParser(path, col, trim);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="col"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        [Obsolete("Use RowContentParserUtilities instead")]
        public static XPathRowContentParser createXPathRowContentParser(string[] path, int[] col, bool trim)
        {
            return RowContentParserUtilities.createXPathRowContentParser(path, col, trim);
        }

        /// <summary>
        /// turn off all javascript error and popup.
        /// </summary>
        /// <param name="browser"></param>
        public static void turnOffJavascriptErrorAndPopup(AxSHDocVw.AxWebBrowser browser)
        {
            browser.NavigateComplete2 += new AxSHDocVw.DWebBrowserEvents2_NavigateComplete2EventHandler(browser_NavigateComplete2);
            
        }
        /// <summary>
        /// mute browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void browser_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
        {
            IHTMLDocument2 oDoc = (IHTMLDocument2)((SHDocVw.IWebBrowser2)e.pDisp).Document;
            oDoc.parentWindow.execScript("window.onerror=function(msg,l,line){event.returnValue=true;};", "javascript");
            oDoc.parentWindow.execScript("window.open=function(){};", "javascript");
            oDoc.parentWindow.execScript("window.alert=function(){};", "javascript");
            oDoc.parentWindow.execScript("window.prompt=function(){};", "javascript");
            oDoc.parentWindow.execScript("window.showModalDialog=function(){};", "javascript");
            oDoc.parentWindow.execScript("window.showModelessDialog=function(){};", "javascript");

        }

        /// <summary>
        /// navigate a browser with navNoReadFromCache, navNoHistory, and navNoWriteToCache arguments.
        /// </summary> 
        /// <param name="browser"></param>
        /// <param name="url"></param>
        public static void navigateUrlWithoutCache(AxSHDocVw.AxWebBrowser browser, string url)
        {
            Int32 oFlags = new Int32();
            oFlags = (Int32)(BrowserNavConstants.navNoHistory & BrowserNavConstants.navNoReadFromCache & BrowserNavConstants.navNoWriteToCache);
            object oNullObj = null;
            object oValue = (object)oFlags;
            browser.Navigate(url, ref oValue, ref oNullObj, ref oNullObj, ref oNullObj);
        }

        /// <summary>
        /// evaluates a list of path sequentially and returns resul
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IHTMLDOMNode queryHTMLNode(System.Windows.Forms.HtmlDocument doc, List<string> path)
        {
            if (path == null)
            {
                return null;
            }
            IHTMLDOMNode oReturn = null;
            for (int i = 0; i < path.Count && oReturn == null; i++)
            {

                oReturn = SpiderPath.selectSingleNode(doc, path[i]);
            }

            return oReturn;
        }

        /// <summary>
        /// evaluates a list of path sequentially and returns resul
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IHTMLDOMNode queryHTMLNode(IHTMLDocument2 doc, List<string> path)
        {
            if (path == null)
            {
                return null;
            }
            IHTMLDOMNode oReturn = null;
            for (int i = 0; i < path.Count && oReturn==null; i++)
            {
                oReturn = SpiderPath.selectSingleNode(doc, path[i]);
            }

            return oReturn;
        }

        /// <summary>
        /// evaluates a list of path sequentially and returns result.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IHTMLDOMNode queryHTMLNode(IHTMLDOMNode n, List<string> path)
        {
            if (path == null)
            {
                return null;
            }
            IHTMLDOMNode oReturn = null;
            for (int i = 0; i < path.Count && oReturn == null; i++)
            {
                oReturn = SpiderPath.selectSingleNode(n, path[i]);
            }

            return oReturn;
        }
    }
}
