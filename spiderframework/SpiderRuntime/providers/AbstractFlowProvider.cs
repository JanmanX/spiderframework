using System;
using System.Collections.Generic;
using System.Text;
using fox.spider.runtime.interfaces;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// AbstractFlowProvider is an abstract implementation of IFlowUnit. All classes related to SpiderFlow should
    /// descend from this class.
    /// �b Flow �� Provider ���⹳�ƹ�@�A�o�����O�~�� AbstractProvider �S��@ IFlowUnit �C�Ҧ��P SpiderFlow ������
    /// Provider ����ĳ�~�ӳo�����O�C
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
