using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.path.core
{
    /// <summary>
    /// token processor base interface.
    /// </summary>
    public interface SPathTokenProcessor
    {
        /// <summary>
        /// check if token p is supported by this processor.
        /// </summary>
        /// <param name="context">current context node</param>
        /// <param name="p">a token which needs to apply to context node.</param>
        /// <param name="tokens">all tokes</param>
        /// <param name="index">current token index</param>
        /// <returns></returns>
        bool isSupport(object context, string p, string[] tokens, int index);
        /// <summary>
        /// evaluate the token and return a result.
        /// </summary>
        /// <param name="context">current context node</param>
        /// <param name="p">a token which needs to apply to context node.</param>
        /// <param name="tokens">all tokes</param>
        /// <param name="index">current token index</param>
        /// <returns></returns>
        object eval(object context, string p, string[] tokens, int index);
    }
}
