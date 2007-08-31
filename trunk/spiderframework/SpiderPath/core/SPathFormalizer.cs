using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.path.core
{
    /// <summary>
    /// SPathFormalizer is used to formalize a SpiderPath string. Why do we need formalizer?
    /// Think about this scenario: if your want to specify a path which contains some properties, 
    /// such as position in its parent, child count, etc, from context node. What should you do??
    /// 
    /// You may write a SpiderPath like this: ../{following-sibling}/TD[{pi-index}]. If the position
    /// of context node is 2, the path will be evaluated like this: ../{following-sibling/TD[2].
    /// </summary>
    public interface SPathFormalizer
    {
        /// <summary>
        /// check if this context or path can be formalized
        /// </summary>
        /// <param name="context">context node</param>
        /// <param name="p">path</param>
        /// <returns></returns>
        bool isSupport(object context, string p);
        /// <summary>
        /// formalize the path.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        string formalize(object context, string p);
    }
}
