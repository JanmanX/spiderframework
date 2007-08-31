using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider;
using fox.spider.runtime.constants;
using fox.spider.runtime.interfaces;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// A provider creates AttributeRowContentParser by an XmlElement.
    /// 建立 AttributeRowContentParser 的實作。
    /// </summary>
    public class AttributeRowContentParserProvider : AbstractFlowProvider, IRowContentParserProvider
    {

        #region IRowContentParserProvider Members
        /// <summary>
        /// create a AttributeRowContentParserProvider instance by an XmlElement.
        /// It copies the value of "Path", "Attribute", "ColumnName" elements, 
        /// who are child element of elm, to AttributeRowContentParser.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public IElementRowContentParser createRowContentParser(XmlElement elm)
        {
            IElementRowContentParser oReturn = null;
            string sPath = null, sAttribute = null, sColumn = null;
            for (int i = 0; i < elm.ChildNodes.Count; i++)
            {
                if (elm.ChildNodes.Item(i).LocalName.Equals("Path"))
                {
                    sPath = ((XmlElement)elm.ChildNodes.Item(i)).InnerText;
                }
                else if (elm.ChildNodes.Item(i).LocalName.Equals("Attribute"))
                {
                    sAttribute = ((XmlElement)elm.ChildNodes.Item(i)).InnerText;
                }
                else if (elm.ChildNodes.Item(i).LocalName.Equals("ColumnName"))
                {
                    sColumn = ((XmlElement)elm.ChildNodes.Item(i)).InnerText;
                }
            }

            if (sPath != null && sAttribute != null && sColumn != null)
            {
                oReturn = RowContentParserUtilities.createAttributeRowContentParser(sPath, sAttribute, sColumn);
            }

            return oReturn;
        }

        #endregion
    }
}
