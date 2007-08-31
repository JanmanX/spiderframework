using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// The basic item lives in a runtime flow.
    /// </summary>
    public interface IFlowUnit : IRuntimeUnit
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <see cref="fox.spider.runtime.interfaces.IFlowResourceProvider"/>
        void setFlowResourceProvider(IFlowResourceProvider p);
    }
}
