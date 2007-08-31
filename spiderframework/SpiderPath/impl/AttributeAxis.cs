using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace fox.spider.path.impl
{
    /// <summary>
    /// AttributeAxis supports a token with @ as its prefix. Such @src
    /// 
    /// This Axis doesn't support predicates.
    /// </summary>
    public class AttributeAxis : AbstractAxis
    {
        /// <summary>
        /// check if p is starting with @ and context is an element. If yes, return true.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="p"></param>
        /// <param name="tokens"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public override bool isSupport(object context, string p, string[] tokens, int index)
        {
            return context is IHTMLElement && p.StartsWith("@");
        }
        /// <summary>
        /// AttributeAxis return an AttributeNode if attribute is found.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="p"></param>
        /// <param name="tokens"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public override object eval(object context, string p, string[] tokens, int index)
        {
            if (isSupport(context, p, tokens, index))
            {
                IHTMLAttributeCollection2 oAttCol = (IHTMLAttributeCollection2)((IHTMLDOMNode) context).attributes;
                return oAttCol.getNamedItem(p.Substring(1));
            }
            return null;
        }
    }
}
