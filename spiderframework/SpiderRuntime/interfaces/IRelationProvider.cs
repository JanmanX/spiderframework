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
    /// IRelationProcessor �|�ϥ� XmlElement ���O�ѼơA�ӫإ߱M�ݪ� IRelationProcessor �C
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
