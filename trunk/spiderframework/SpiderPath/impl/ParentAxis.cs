using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace fox.spider.path.impl
{
    /// <summary>
    /// ParentAxis uses ".." as its axis. It just returns the parent node.
    /// 
    /// This Axis doesn't support predicates.
    /// </summary>
    public class ParentAxis : AbstractAxis
    {
        public override bool isSupport(object context, string p, string[] tokens, int index)
        {
            return p.Equals("..") && (context is IHTMLDOMNode || context is IHTMLDOMAttribute2);
        }

        public override object eval(object context, string p, string[] tokens, int index)
        {
            if (isSupport(context, p, tokens, index))
            {
                if (context is IHTMLDOMNode)
                {
                    IHTMLDOMNode oNode = (IHTMLDOMNode)context;
                    return oNode.parentNode;
                }
                else if (context is IHTMLDOMAttribute2)
                {
                    IHTMLDOMAttribute2 oAtt = (IHTMLDOMAttribute2)context;
                    return oAtt.parentNode;
                }
            }
            return null;
        }
    }
}
