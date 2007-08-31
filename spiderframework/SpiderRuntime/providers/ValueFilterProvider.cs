using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider.runtime.filter;
using fox.spider.runtime.interfaces;
using fox.spider.runtime.constants;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// A provider creates a ValueFilter by an XmlElement
    /// 建立 ValueFilter 的實作。
    /// </summary>
    public class ValueFilterProvider : AbstractRowFilterProvider
    {
        private ValueFilter createValueFilter(XmlNodeList list)
        {
            ValueFilter oReturn = null;

            for (int i = 0; i < list.Count; i++)
            {
                XmlElement oElem = (XmlElement)list[i];
                if (oElem.GetAttributeNode("FieldName") != null && oElem.FirstChild != null)
                {
                    if (oReturn == null)
                    {
                        oReturn = new ValueFilter();
                    }
                    if (oElem.FirstChild.LocalName.Equals("NotNull"))
                    {
                        oReturn.addNotNullFields(oElem.GetAttribute("FieldName"));
                    }
                    else if(oElem.FirstChild.LocalName.Equals("Regexp"))
                    {
                        oReturn.addRegexField(oElem.GetAttribute("FieldName"), ((XmlElement)oElem.FirstChild).InnerText);
                    }
                }
            }

            return oReturn;
        }

        #region IRowFilterProvider Members
        /// <summary>
        /// creates a row filter and hooks it to a document parser.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public override IRowFilter createRowFilter(XmlElement elm)
        {
            IRowFilter oReturn = null;
            XmlNodeList oList = elm.GetElementsByTagName("Constraint", SpiderRuntimeConstants.DefaultNamespace);
            if (oList.Count > 0)
            {
                oReturn = createValueFilter(oList);
                if (oReturn != null)
                {
                    this.hookFilter(elm.GetAttribute("docparser"), oReturn);
                }
            }
            return oReturn;
        }

        #endregion
    }
}
