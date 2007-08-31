using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.path.core
{
    /// <summary>
    /// A SPathPredicate is a filter of Axis result. It has an expression to check if 
    /// the node found by SPathAxis is compliant to this expression or not. Such as 
    /// TD[0], the [0] is an index predicate. 
    /// </summary>
    public interface SPathPredicate : SPathTokenProcessor
    {
        /// <summary>
        /// a collection of SPathFunction.
        /// </summary>
        TokenHelper<SPathFunction> FunctionHelper { get; set;}
    }
}
