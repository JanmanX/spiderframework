using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace fox.spider.path.impl
{
    /// <summary>
    /// LastChildAxis supports "{last-child}" as its axis and returns ((IHTMLDOMNode)context).lastChild.
    /// 
    /// This Axis supports predicates.
    /// </summary>
    public class LastChildAxis : AbstractAxis
    {
        public const string TOKEN = "{last-child}";

        public override bool isSupport(object context, string p, string[] tokens, int index)
        {
            return context is IHTMLDOMNode && p.StartsWith(TOKEN);
        }

        public override object eval(object context, string p, string[] tokens, int index)
        {
            if (isSupport(context, p, tokens, index))
            {
                object oNode = ((IHTMLDOMNode)context).lastChild;
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
