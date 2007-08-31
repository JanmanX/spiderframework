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
    /// A provider creates a StringRowContentParser which is RegexRowContentParser in XML configuration by an XmlElement.
    /// 建立 RegexRowContentParser（StringRowContentParser）的實作。
    /// </summary>
    public class RegexRowContentParserProvider : AbstractFlowProvider, IRowContentParserProvider
    {

        #region IRowContentParserProvider Members

        public IElementRowContentParser createRowContentParser(XmlElement elm)
        {
            IElementRowContentParser oReturn = null;
            string sPath = getChildElementText(elm, "Path");
            XmlNodeList oList = elm.GetElementsByTagName("Pair", SpiderRuntimeConstants.DefaultNamespace);
            if (oList.Count > 0 && sPath!=null)
            {
                List<string> aRegex = new List<string>();
                List<string> aColumn = new List<string>();
                foreach (XmlNode n in oList)
                {
                    string sRegex = getChildElementText((XmlElement)n, "Regex");
                    string sColumn = getChildElementText((XmlElement)n, "ColumnName");
                    aRegex.Add(sRegex);
                    aColumn.Add(sColumn);
                }
                
                if (aRegex.Count > 0 && aColumn.Count > 0)
                {
                    bool bMatchHTML = "true".Equals(elm.GetAttribute("matchHTML"));
                    oReturn = RowContentParserUtilities.createStringRowContentParser(sPath, aRegex.ToArray(), aColumn.ToArray(), bMatchHTML);
                }
            }

            return oReturn;
        }

        #endregion
    }
}
