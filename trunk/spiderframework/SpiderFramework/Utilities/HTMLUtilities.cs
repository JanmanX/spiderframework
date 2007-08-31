using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// HTML Utilities.
    /// </summary>
    public class HTMLUtilities
    {
        /// <summary>
        /// convenient function to get the content of a node. If n is an element, returns innerText. Otherwise, returns nodeValue.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string getNodeValue(IHTMLDOMNode n)
        {
            if (n is IHTMLElement)
            {
                return ((IHTMLElement)n).innerText;
            }
            else if (n.nodeValue != null)
            {
                return n.nodeValue.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
