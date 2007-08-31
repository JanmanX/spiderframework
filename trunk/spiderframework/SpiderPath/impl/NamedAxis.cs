using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace fox.spider.path.impl
{
    /// <summary>
    /// NamedAxis is used to get the node with specified element name. Such as "DIV" will return 
    /// the first DIV element, "SPAN[2]" will return the third SPAN elmement. If there is no element
    /// with specified name, returns null.
    /// 
    /// This Axis supports predicates.
    /// </summary>
    public class NamedAxis : AbstractAxis
    {
        public override bool isSupport(object context, string p, string[] tokens, int index)
        {
            return context is IHTMLDOMNode && ((IHTMLDOMNode) context).hasChildNodes();
        }

        public override object eval(object context, string p, string[] tokens, int index)
        {
            if (!isSupport(context, p, tokens, index))
            {
                return null;
            }
            IHTMLDOMNode oChild = ((IHTMLDOMNode)context).firstChild;
            string sPredicate = this.extractPredicate(p);
            string sNodeName=this.extractNonpredicate(p);
            while (oChild != null)
            {
                //to force html dom to parse child html text.
                if (oChild.hasChildNodes())
                {
                    IHTMLDOMNode oTmp = oChild.firstChild;
                }
                if (oChild.nodeName.Equals(sNodeName))
                {
                    if (sPredicate != null)
                    {
                        object oTmp2 = PredicateHelper.eval(oChild, sPredicate, tokens, index);
                        if (oTmp2 != null)
                        {
                            return oTmp2;
                        }
                    }
                    else
                    {
                        return oChild;
                    }
                }
                oChild = oChild.nextSibling;
            }
            return null;
        }
    }
}
