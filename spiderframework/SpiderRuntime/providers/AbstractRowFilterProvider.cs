using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider;
using fox.spider.runtime.interfaces;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// An abstract implementation of IRowFilterProvider.
    /// IRowFilterProvider ���⹳��@
    /// </summary>
    public abstract class AbstractRowFilterProvider:AbstractFlowProvider, IRowFilterProvider
    {
        /// <summary>
        /// This is a convenient function which hooks row filter to a specified document parser, 
        /// whose id is docId. The "hook" means add RowParsedEvent listener, that is filer, 
        /// to document parser.
        /// �N IRowFilter ���W IDocumentParser �� RowParsed �ƥ�C
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="filter"></param>
        protected void hookFilter(string docId, IRowFilter filter)
        {
            IDocumentParser oDocParser = FlowResource.getDocumentParser(docId);
            if (oDocParser != null)
            {
                oDocParser.RowParsed += new RowParsedEvent(filter.filter);
            }
        }


        #region IRowFilterProvider Members
        /// <summary>
        /// extended from IRowFilterProvider.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public abstract IRowFilter createRowFilter(XmlElement elm);
        
        #endregion
    }
}
