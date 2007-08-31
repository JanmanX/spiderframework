using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MSHTML;

namespace fox.spider
{
    /// <summary>
    /// 
    /// </summary>
    public class HXPath
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static HtmlElement selectSingleNodeByDoc(HtmlDocument doc, string path)
        {
            string[] aPath = path.Substring(1).Split('/');
            if (aPath[0].Equals(doc.Body.TagName))
            {
                return selectSingleNode (doc.Body, aPath, 1);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IHTMLDOMNode selectSingleNodeByDoc2(HtmlDocument doc, string path)
        {
            string[] aPath = path.Substring(1).Split('/');
            if (aPath[0].Equals(doc.Body.TagName))
            {
                return selectSingleNode(((IHTMLDOMNode)doc.Body.DomElement), aPath, 1);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static HtmlElement selectSingleNode(HtmlElement node, string path)
        {
            string[] aPath = formaizePath(node, path).Split('/');
            return selectSingleNode(node, aPath, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        [Obsolete("此版本不支援 Frame 不建議再使用")]
        public static string constructPath(HtmlElement node)
        {
            string sReturn = "";
            HtmlElement oNode = node;
            while (oNode != null && !oNode.TagName.Equals("BODY"))
            {
                sReturn = "/" + oNode.TagName + "[" + getIndex(oNode) + "]" + sReturn;
                oNode = oNode.Parent;
            }
            return "/BODY" + sReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        [Obsolete("此版本不支援 Frame 不建議再使用")]
        public static string constructPath(IHTMLDOMNode node)
        {
            string sReturn = "";
            IHTMLDOMNode oNode = node;
            while (oNode != null && !oNode.nodeName.Equals("BODY"))
            {
                sReturn = "/" + oNode.nodeName + "[" + getIndex(oNode) + "]" + sReturn;
                oNode = oNode.parentNode;
            }

            return "/BODY" + sReturn;
        }

        private static int getIndex(IHTMLDOMNode node)
        {
            int iReturn = 0;
            IHTMLDOMNode oParent = node.parentNode;
            IHTMLDOMNode oChild = oParent.firstChild;
            while (oChild != null && !oChild.Equals(node))
            {
                if (oChild.nodeName.Equals(node.nodeName))
                    iReturn++;

                oChild = oChild.nextSibling;
            }
            return iReturn;
        }


        private static int getIndex(HtmlElement node)
        {
            int iReturn = 0;
            HtmlElement oParent = node.Parent;
            for(int i=0;i<oParent.Children.Count && !oParent.Children[i].Equals(node);i++)
            {
                if (oParent.Children[i].TagName.Equals(node.TagName))
                    iReturn++;
            }
            return iReturn;
        }

        private static HtmlElement func_lastChild(HtmlElement node)
        {
            if (node.Children.Count > 0)
                return node.Children[node.Children.Count - 1];
            else
                return null;
        }

        private static HtmlElement selectSingleNode(HtmlElement node, string[] path, int index)
        {
            if (node == null || node.DomElement==null)
                return null;
            if (path.Length == index || path.Length < index)
                return node;
            if (path[index].Equals("."))
                return node;
            if (path[index].Equals(".."))
                return selectSingleNode(node.Parent, path, index + 1);
            if (path[index].Equals("{following-sibling}"))
                return selectSingleNode(node.NextSibling, path, index + 1);
            if (path[index].Equals("{last-child}"))
                return selectSingleNode(func_lastChild(node), path, index + 1);

            if (node.Children.Count == 0)
                return null;

            HtmlElement oChild = node.FirstChild;
            int iCount = 0;
            while (oChild != null)
            {
                if (oChild.DomElement == null)
                    break;

                if (oChild.TagName + "[" + iCount.ToString() + "]" == path[index])
                {
                    return HXPath.selectSingleNode(oChild, path, index + 1);
                }
                if (path[index].StartsWith(oChild.TagName))
                {
                    iCount++;
                }
                oChild = oChild.NextSibling;
            }
            return null;
        }
        /// <summary>
        /// 直接提供從 IHTMLDocument2 的介面開始查詢。這個方法支援 /BODY/xxxx 及
        /// /FRAME[0]/xxxx 的格式。
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IHTMLDOMNode selectSingleNode(IHTMLDocument2 doc, string path)
        {
            if (path.StartsWith("/BODY/"))
            {
                return selectSingleNode((IHTMLDOMNode)doc.body, path.Substring(6));
            }
            else if (path.StartsWith("/FRAME"))
            {
                string sTmpPath = path.Substring(1);
                string sFramePath= sTmpPath.Substring(sTmpPath.IndexOf("/"));
                sTmpPath = sTmpPath.Substring(0, sTmpPath.IndexOf("/"));
                
                FramesCollection aFrames = doc.frames;
                for (int i = 0; i < aFrames.length; i++)
                {
                    if (sTmpPath.Equals("FRAME[" + i + "]"))
                    {
                        object oObj = i;
                        IHTMLWindow2 oFrame = (IHTMLWindow2)aFrames.item(ref oObj);
                        return selectSingleNode(oFrame.document, sFramePath);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IHTMLDOMNode selectSingleNode(IHTMLDOMNode node, string path)
        {
            string[] aPath = formaizePath(node, path).Split('/');
            return selectSingleNode(node, aPath, 0);
        }

        private static string formaizePath(IHTMLDOMNode node, string path)
        {
            int iIndex = getIndex(node);
            string sResult = path.Replace("{pi-index}", iIndex.ToString());
            return sResult;
        }

        private static string formaizePath(HtmlElement node, string path)
        {
            int iIndex = getIndex(node);
            string sResult = path.Replace("{pi-index}", iIndex.ToString());
            return sResult;
        }

        private static IHTMLDOMNode selectSiblingToken(Match m, IHTMLDOMNode node, string[] path, int index)
        {
            string sSiblingToken = m.Groups[1].Value;
            string sTagToken = m.Groups[2].Value;
            IHTMLDOMNode oNode = node;
            if (sSiblingToken.Equals("following-sibling"))
            {
                oNode = oNode.nextSibling;
            }
            else
            {
                oNode = oNode.previousSibling;
            }

            while (oNode != null)
            {
                if (oNode.nodeName.Equals(sTagToken))
                {
                    return selectSingleNode(oNode, path, index + 1);
                }

                if (sSiblingToken.Equals("following-sibling"))
                {
                    oNode = oNode.nextSibling;
                }
                else
                {
                    oNode = oNode.previousSibling;
                }
            }
            return null;
        }

        private static IHTMLDOMNode selectSingleNode(IHTMLDOMNode node, string[] path, int index)
        {
            if (node == null)
                return null;
            if (path.Length == index || path.Length < index)
                return node;
            if (path[index].Equals("."))
                return node;
            if (path[index].Equals(".."))
                return selectSingleNode(node.parentNode, path, index + 1);
            if (path[index].Equals("{following-sibling}"))
                return selectSingleNode(node.nextSibling, path, index + 1);
            if (path[index].Equals("{preceding-sibling}"))
                return selectSingleNode(node.previousSibling, path, index + 1);
            if (path[index].Equals("{last-child}"))
                return selectSingleNode(node.lastChild, path, index + 1);
            if (path[index].StartsWith("@"))
            {
                if (node is IHTMLElement4)
                {
                    IHTMLAttributeCollection2 oAttCol = (IHTMLAttributeCollection2)node.attributes;
                    return (IHTMLDOMNode)oAttCol.getNamedItem(path[index].Substring(1));
                    //return (IHTMLDOMNode)((IHTMLElement4)node).getAttributeNode(path[index].Substring(1));
                }
                return null;
            }

            Regex oSpecialFilterRegex = new Regex("\\{(following\\-sibling|preceding\\-sibling)\\:([A-Z]+)\\}");
            if (oSpecialFilterRegex.IsMatch(path[index]))
                return selectSiblingToken(oSpecialFilterRegex.Match(path[index]), node, path, index);
            if (!node.hasChildNodes())
                return null;

            IHTMLDOMNode oChild = ((IHTMLDOMNode)node).firstChild;
            int iCount = 0;
            while (oChild != null)
            {
                if (oChild.hasChildNodes())
                {
                    IHTMLDOMNode oTmp=oChild.firstChild;
                }
                if (oChild.nodeName + "[" + iCount.ToString() + "]" == path[index])
                {
                    return HXPath.selectSingleNode(oChild, path, index + 1);
                }
                if (oChild.nodeName!=null && !"".Equals(oChild.nodeName) && 
                    path[index].StartsWith(oChild.nodeName))
                {
                    iCount++;
                }
                oChild = oChild.nextSibling;
            }
            return null;
        }
    }
}
