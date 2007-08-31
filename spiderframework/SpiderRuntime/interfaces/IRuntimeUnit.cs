using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// the basic item lives in runtime. <br/>
    /// 每個會在 Spider Runtime 中執行的核心物件都必須實作這個方法。這個方法主要是提供一個 IGlobalResourceProvider 的物件。
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
