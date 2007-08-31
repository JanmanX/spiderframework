using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using fox.spider.path.utils;

namespace fox.spider.path.impl
{
    /// <summary>
    /// IndexPredicate checks if the position of a node is equal to predicate value.
    /// 
    /// Ex: TD[2] is the third TD.
    /// </summary>
    public class IndexPredicate : AbstractPredicate
    {
        public override bool isSupport(object context, string p, string[] tokens, int index)
        {
            try
            {
                return int.Parse(p) > -1;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override object eval(object context, string p, string[] tokens, int index)
        {
            if (isSupport(context, p, tokens, index))
            {
                int iIndex = Utilities.getIndex((IHTMLDOMNode)context);
                if (int.Parse(p) == iIndex)
                {
                    return context;
                }
            }
            return null;
        }
    }
}
