using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using mshtml;

namespace fox.spider.path.impl
{
    /// <summary>
    /// FollowingPrecedingSiblingAxis supports "{[following-sibling|preceding-sibling]:[A-Z]+}" style token.
    /// You may supply a token like: {following-sibling:TD} to find next TD sibling.
    /// 
    /// This Axis supports predicates.
    /// </summary>
    public class FollowingPrecedingSiblingAxis : AbstractAxis
    {
        private Regex m_SpecialFilterRegex = new Regex("\\{(following\\-sibling|preceding\\-sibling)\\:([A-Z]+)\\}");

        /// <summary>
        /// check if context is a IHTMLDOMNode and p is matched by Regex("\\{(following\\-sibling|preceding\\-sibling)\\:([A-Z]+)\\}")
        /// </summary>
        /// <param name="context"></param>
        /// <param name="p"></param>
        /// <param name="tokens"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public override bool isSupport(object context, string p, string[] tokens, int index)
        {
            return context is IHTMLDOMNode && m_SpecialFilterRegex.IsMatch(p);
        }

        public override object eval(object context, string p, string[] tokens, int index)
        {
            if (isSupport(context, p, tokens, index))
            {
                return selectSiblingToken(m_SpecialFilterRegex.Match(p), (IHTMLDOMNode)context, tokens, index);
            }
            return null;
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
                    return oNode;
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
    }
}
