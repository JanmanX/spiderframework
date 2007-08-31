using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace fox.spider.path.utils
{
    class Utilities
    {
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
            //IHTMLDOMNode oParent = node.parentNode;
            //IHTMLDOMNode oChild = oParent.firstChild;
            //while (oChild != null && !oChild.Equals(node))
            //{
            //    if (oChild.nodeName.Equals(node.nodeName))
            //        iReturn++;

            //    oChild = oChild.nextSibling;
            //}
            return iReturn;
        }
    }
}
