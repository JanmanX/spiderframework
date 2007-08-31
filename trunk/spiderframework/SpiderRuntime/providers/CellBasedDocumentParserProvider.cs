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
    /// A provider creates CellBasedDocumentParser by an XmlElement.
    /// 建立 CellBasedDocumentParser 的實作
    /// </summary>
    public class CellBasedDocumentParserProvider : AbstractDocumentParserProvider
    {
        /// <summary>
        /// creates CellBasedDocumentParser. 
        /// This method loads RowFirstPath, RowNextPath, CellFirstPath, and CellNextPath, child elements, as an List of string.
        /// Ans use these list to create a CellBasedDocumentParser.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public override IDocumentParser createDocumentParser(XmlElement elm)
        {
            IDocumentParser oReturn = null;
            string sRowId = getChildElementText(elm, "RowContentParserId");
            IElementRowContentParser oRowParser = FlowResource.getRowContentParser(sRowId);

            XmlNodeList oList = elm.GetElementsByTagName("Path", SpiderRuntimeConstants.DefaultNamespace);
            if (oRowParser != null && oList.Count > 0)
            {
                XmlElement oPath = (XmlElement)oList[0];
                List<string> aRowFirst = new List<string>();
                List<string> aRowNext = new List<string>();
                List<string> aCellFirst = new List<string>();
                List<string> aCellNext = new List<string>();
                foreach (XmlNode n in oPath.ChildNodes)
                {
                    if (n.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }

                    XmlElement oElm = (XmlElement)n;
                    if (oElm.LocalName.Equals("RowFirstPath") && oElm.InnerText != null)
                    {
                        aRowFirst.Add(((XmlElement)n).InnerText);
                    }
                    else if (oElm.LocalName.Equals("RowNextPath") && oElm.InnerText != null)
                    {
                        aRowNext.Add(((XmlElement)n).InnerText);
                    }
                    else if (oElm.LocalName.Equals("CellFirstPath") && oElm.InnerText != null)
                    {
                        aCellFirst.Add(((XmlElement)n).InnerText);
                    }
                    else if (oElm.LocalName.Equals("CellNextPath") && oElm.InnerText != null)
                    {
                        aCellNext.Add(((XmlElement)n).InnerText);
                    }
                }

                oReturn = DocumentParserUtilities.createCellBasedDocumentParser(oRowParser, getDataTable(elm),
                    getRelationProcessor(elm), aRowFirst.ToArray(), aRowNext.ToArray(), aCellFirst.ToArray(), 
                    aCellNext.ToArray());
                //oReturn.RowParsed += new RowParsedEvent(TypeCorrection);
            }

            return oReturn;
        }
    }
}
