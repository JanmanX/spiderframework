using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using fox.spider.path;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// Spider Path Utilities
    /// </summary>
    public class SpiderPath
    {
        private static SPath m_Path = new SPath();
        /// <summary>
        /// convenient function to SPath.selectSingleNode
        /// </summary>
        /// <param name="n"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IHTMLDOMNode selectSingleNode(IHTMLDOMNode n, string p)
        {
            object oReturn=m_Path.selectSingleNode(n, p);
            if (oReturn is IHTMLDOMNode)
            {
                return (IHTMLDOMNode)oReturn;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// convenient function to SPath.selectSingleNode
        /// </summary>
        /// <param name="n"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IHTMLDOMNode selectSingleNode(IHTMLDocument2 n, string p)
        {
            object oReturn = m_Path.selectSingleNode(n, p);
            if (oReturn is IHTMLDOMNode)
            {
                return (IHTMLDOMNode)oReturn;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// convenient function to SPath.selectSingleNode
        /// </summary>
        /// <param name="n"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IHTMLDOMNode selectSingleNode(HtmlDocument n, string p)
        {
            object oReturn = m_Path.selectSingleNode(n, p);
            if (oReturn is IHTMLDOMNode)
            {
                return (IHTMLDOMNode)oReturn;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// convenient function to SPath.selectSingleNode
        /// </summary>
        /// <param name="n"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IHTMLDOMAttribute selectSingleAttribute(IHTMLDOMNode n, string p)
        {
            object oReturn = m_Path.selectSingleNode(n, p);
            if (oReturn is IHTMLDOMAttribute)
            {
                return (IHTMLDOMAttribute)oReturn;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// convenient function to SPath.selectSingleNode
        /// </summary>
        /// <param name="n"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IHTMLDOMAttribute selectSingleAttribute(IHTMLDocument2 n, string p)
        {
            object oReturn = m_Path.selectSingleNode(n, p);
            if (oReturn is IHTMLDOMAttribute)
            {
                return (IHTMLDOMAttribute)oReturn;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// convenient function to SPath.selectSingleNode
        /// </summary>
        /// <param name="n"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IHTMLDOMAttribute selectSingleAttribute(HtmlDocument n, string p)
        {
            object oReturn = m_Path.selectSingleNode(n, p);
            if (oReturn is IHTMLDOMAttribute)
            {
                return (IHTMLDOMAttribute)oReturn;
            }
            else
            {
                return null;
            }
        }
    }
}
