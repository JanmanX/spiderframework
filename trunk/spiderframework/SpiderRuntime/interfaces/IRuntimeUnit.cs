using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// the basic item lives in runtime. <br/>
    /// �C�ӷ|�b Spider Runtime �����檺�֤ߪ��󳣥�����@�o�Ӥ�k�C�o�Ӥ�k�D�n�O���Ѥ@�� IGlobalResourceProvider ������C
    /// </summary>
    /// <see cref="fox.spider.runtime.interfaces.IGlobalResourceProvider"/>
    public interface IRuntimeUnit
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        void setGlobalResource(IGlobalResourceProvider p);
    }
}
