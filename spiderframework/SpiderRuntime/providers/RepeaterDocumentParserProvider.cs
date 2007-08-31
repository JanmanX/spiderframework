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
    /// Creats a RepeaterDocumentParser by an XmlElement.
    /// 建立 RepeaterDocumentParser 的實作
    /// </summary>
    public class RepeaterDocumentParserProvider : AbstractDocumentParserProvider
    {
        public override IDocumentParser createDocumentParser(XmlElement elm)
        {
            IDocumentParser oReturn = null;
            string sRowId = getChildElementText(elm, "RowContentParserId");
            IElementRowContentParser oRowParser = FlowResource.getRowContentParser(sRowId);

            XmlNodeList oList = elm.GetElementsByTagName("Path", SpiderRuntimeConstants.DefaultNamespace);
            if (oRowParser != null && oList.Count > 0)
            {
                XmlElement oPath = (XmlElement)oList[0];
                List<string> aFirst = new List<string>();
                List<string> aNext = new List<string>();
                foreach (XmlNode n in oPath.ChildNodes)
                {
                    if (n.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }

                    XmlElement oElm = (XmlElement)n;
                    if (oElm.LocalName.Equals("FirstPath") && oElm.InnerText!=null)
                    {
                        aFirst.Add(((XmlElement)n).InnerText);
                    }
                    else if (oElm.LocalName.Equals("NextPath") && oElm.InnerText != null)
                    {
                        aNext.Add(((XmlElement)n).InnerText);
                    }
                }

                oReturn = DocumentParserUtilities.createRepeaterContentParser(oRowParser, getDataTable(elm),
                    getRelationProcessor(elm), aFirst.ToArray(), aNext.ToArray());
                //oReturn.RowParsed += new RowParsedEvent(TypeCorrection);
            }

            return oReturn;
        }
    }
}
