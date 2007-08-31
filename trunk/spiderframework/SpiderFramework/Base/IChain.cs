using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider
{
    /// <summary>
    /// IChain uses Chain of Responsibility pattern. Each IChain implementor 
    /// has its owned logics and storage model. Every IChain implementor may
    /// have equivalent power to process reponsible job.
    /// </summary>
    public interface IChain
    {
        /// <summary>
        /// Returns next IChain implementor. If it doesn't have it, return null.
        /// </summary>
        /// <returns>next IChain object.</returns>
        IChain next();
        /// <summary>
        /// set next IChain implementor.
        /// </summary>
        /// <param name="next">next IChan implementor</param>
        void setNext(IChain next);
    }
}
