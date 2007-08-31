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
    /// A provider creates XPathRowContentParser by an XmlElement.
    /// 建立 XPathRowContentParser 的實作。
    /// </summary>
    public class XPathRowContentParserProvider : AbstractFlowProvider, IRowContentParserProvider
    {
        #region IRowContentParserProvider Members

        public IElementRowContentParser createRowContentParser(XmlElement elm)
        {
            IElementRowContentParser oReturn = null;

            bool bTrim = elm.GetElementsByTagName("Trim", SpiderRuntimeConstants.DefaultNamespace).Count > 0;
            XmlNodeList oList = elm.GetElementsByTagName("Pair", SpiderRuntimeConstants.DefaultNamespace);
            if (oList.Count > 0)
            {
                List<string> aPath = new List<string>();
                List<string> aColumn = new List<string>();
                foreach (XmlNode n in oList)
                {
                    string sPath = getChildElementText((XmlElement)n, "Path");
                    string sColumn = getChildElementText((XmlElement)n, "ColumnName");
                    aPath.Add(sPath);
                    aColumn.Add(sColumn);
                }

                if (aPath.Count > 0 && aColumn.Count > 0)
                {
                    oReturn = RowContentParserUtilities.createXPathRowContentParser(aPath.ToArray(), aColumn.ToArray(), bTrim);
                }
            }
            return oReturn;
        }

        #endregion
    }
}
