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
    /// A provider creates a PriorityXPathRowContentParser by an XmlElement.
    /// 建立 PriorityXPathRowContentParser 的實作
    /// </summary>
    public class PriorityXPathRowContentParserProvider : AbstractFlowProvider, IRowContentParserProvider
    {
        #region IRowContentParserProvider Members
        /// <summary>
        /// creates a PriorityXPathRowContentParser.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public IElementRowContentParser createRowContentParser(XmlElement elm)
        {
            IElementRowContentParser oReturn = null;

            List<string> aPath = new List<string>();
            string sColumn = null;

            foreach (XmlNode n in elm.ChildNodes)
            {
                if (n.LocalName.Equals("Path"))
                {
                    aPath.Add(((XmlElement)n).InnerText);
                }
                else if (n.LocalName.Equals("ColumnName"))
                {
                    sColumn = ((XmlElement)n).InnerText;
                }
            }

            if (aPath.Count > 0 && sColumn != null)
            {
                oReturn = RowContentParserUtilities.createPriorityRowContentParser(aPath.ToArray(), sColumn);
            }

            return oReturn;
        }

        #endregion
    }
}
