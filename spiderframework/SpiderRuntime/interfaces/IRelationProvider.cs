using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// IRelationProcessor will create a IRelationProcessor by XmlElement which is a element in configuration file.
    /// <br/>
    /// IRelationProcessor 會使用 XmlElement 當成是參數，來建立專屬的 IRelationProcessor 。
    /// </summary>
    public interface IRelationProvider : IRuntimeUnit
    {
        /// <summary>
        /// create a relation processor.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns>IRelationProcessor</returns>
        IRelationProcessor createRelationProcessor(XmlElement elm);
    }
}
