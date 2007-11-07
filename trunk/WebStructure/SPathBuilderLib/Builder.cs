using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace fox.spider.path.builder
{
    public class Builder
    {
        /// <summary>
        /// Build the path of a node.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string buildPath(IHTMLDOMNode n)
        {
            if (n == null)
            {
                return "";
            }
            if (n is IHTMLBodyElement)
            {
                return "/BODY";
            }
            ///
            /// Build the path of the parent.
            string sAncestor = buildPath(n.parentNode);
            int iIndex = getIndex(n);

            return sAncestor + "/" + n.nodeName + "[" + iIndex.ToString() + "]";
        }

        /// <summary>
        /// Build the path of an attribute node.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        
        public static string buildPath(IHTMLDOMNode parent, IHTMLDOMAttribute2 a)
        {
            string sAncestor = buildPath(parent);
            return sAncestor + "/@" + a.name;
        }

        /// <summary>
        /// get the index of a node. This method uses previousSibling to calc index.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static int getIndex(IHTMLDOMNode node)
        {
            int iReturn = 0;
            IHTMLDOMNode oNode = node.previousSibling;
            while (oNode != null && !oNode.Equals(node))
            {
                if (oNode.nodeName.Equals(node.nodeName))
                    iReturn++;
                oNode = oNode.previousSibling;
            }
            return iReturn;
        }
    }
}
