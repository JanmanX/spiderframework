using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace fox.spider.path.impl
{
    /// <summary>
    /// Pure preceding sibling. This is a back-compatible axis which supports {preceding-sibling} as its axis.
    /// PrecedingSiblingAxis returns previous sibling relative to context no matter what element type is.
    /// 
    /// This Axis supports predicates.
    /// </summary>
    public class PrecedingSiblingAxis : AbstractAxis
    {
        public const string TOKEN = "{preceding-sibling}";

        public override bool isSupport(object context, string p, string[] tokens, int index)
        {
            return context is IHTMLDOMNode && p.StartsWith(TOKEN);
        }

        public override object eval(object context, string p, string[] tokens, int index)
        {
            if (isSupport(context, p, tokens, index))
            {
                object oNode = ((IHTMLDOMNode)context).previousSibling;
                string sPredicate = this.extractPredicate(p);
                if (sPredicate != null)
                {
                    oNode = PredicateHelper.eval(oNode, sPredicate, tokens, index);
                }
                return oNode;
            }
            return null;
        }
    }
}
