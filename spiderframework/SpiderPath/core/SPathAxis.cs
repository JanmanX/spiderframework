using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.path.core
{
    /// <summary>
    /// SPathAxis is a processor of each path token, such as {last-child}, TD, DIV, etc. 
    /// SPathAxis is responsible for processing each path token and returning the corresponding node.
    /// Each path token may shipped with functions or predicates, such as TD[0], etc. So, each
    /// implementor may tokenize each token to find out functions or predicates and use PredicateHelper 
    /// or FunctionHelper to deal with it.
    /// 
    /// 
    /// </summary>
    public interface SPathAxis : SPathTokenProcessor
    {
        /// <summary>
        /// A collection of SPathPredicate.
        /// </summary>
        TokenHelper<SPathPredicate> PredicateHelper { get; set;}
        /// <summary>
        /// A collection of SPathFunction.
        /// </summary>
        TokenHelper<SPathFunction> FunctionHelper { get; set;}
    }
}
