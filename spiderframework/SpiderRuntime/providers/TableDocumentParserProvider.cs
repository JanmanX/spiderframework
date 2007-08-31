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
    /// A provider creates a TableExtractor which is TableDocumentParser in XML configuration file by an XmlElement.
    /// 建立 TableDocumentParser（TableExtractor）的實作
    /// </summary>
    public class TableDocumentParserProvider : AbstractDocumentParserProvider
    {
        public override IDocumentParser createDocumentParser(XmlElement elm)
        {
            IDocumentParser oReturn = null;
            string sRowId = getChildElementText(elm, "RowContentParserId");
            string sPath = getChildElementText(elm, "Path");
            string sStart = getChildElementText(elm, "StartRow");
            string sEnd = getChildElementText(elm, "EndRow");
            IElementRowContentParser oRowParser = FlowResource.getRowContentParser(sRowId);
            if (oRowParser != null && sPath != null)
            {
                int iStart = 0;
                int iEnd = int.MaxValue;
                if (sStart != null)
                {
                    try
                    {
                        iStart = int.Parse(sStart);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                if (sEnd != null)
                {
                    try
                    {
                        iEnd = int.Parse(sEnd);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }

                oReturn = DocumentParserUtilities.createTableExtractor(oRowParser, getDataTable(elm), 
                    getRelationProcessor(elm), sPath, iStart, iEnd);
                //oReturn.RowParsed += new RowParsedEvent(TypeCorrection);
            }

            return oReturn;
        }
    }
}
