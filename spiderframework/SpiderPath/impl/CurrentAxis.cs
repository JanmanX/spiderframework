using System;
using System.Collections.Generic;
using System.Text;
using fox.spider.path.core;

namespace fox.spider.path.impl
{
    /// <summary>
    /// CurrentAxis supports "." as its token and returns context node as its result.
    /// 
    /// This Axis doesn't support predicates.
    /// </summary>
    public class CurrentAxis : AbstractAxis
    {

        public override bool isSupport(object context, string p, string[] tokens, int index)
        {
            return p.Equals(".");
        }

        public override object eval(object context, string p, string[] tokens, int index)
        {
            return isSupport(context, p, tokens, index) ? context : null;
        }
    }
}
