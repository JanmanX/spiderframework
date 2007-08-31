using System;
using System.Collections.Generic;
using System.Text;
using fox.spider.runtime.interfaces;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// AbstractFlowProvider is an abstract implementation of IFlowUnit. All classes related to SpiderFlow should
    /// descend from this class.
    /// 在 Flow 的 Provider 的抽像化實作，這個類別繼承 AbstractProvider 又實作 IFlowUnit 。所有與 SpiderFlow 有關的
    /// Provider 都建議繼承這個類別。
    /// </summary>
    public abstract class AbstractFlowProvider : AbstractProvider, IFlowUnit
    {
        private IFlowResourceProvider m_FlowResource;
        /// <summary>
        /// get FlowResource
        /// </summary>
        protected IFlowResourceProvider FlowResource
        {
            get { return m_FlowResource; }
            set { m_FlowResource = value; }
        }

        #region IFlowUnit Members
        /// <summary>
        /// set flow resource provider
        /// </summary>
        /// <param name="p"></param>
        public void setFlowResourceProvider(IFlowResourceProvider p)
        {
            FlowResource = p;
        }

        #endregion
    }
}
