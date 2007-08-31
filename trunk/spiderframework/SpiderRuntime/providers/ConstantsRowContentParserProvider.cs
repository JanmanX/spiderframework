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
    /// A provider creates ConstantsRowContentParser.
    /// </summary>
    public class ConstantsRowContentParserProvider : AbstractFlowProvider, IRowContentParserProvider
    {
        #region IRowContentParserProvider Members
        /// <summary>
        /// creates a ConstantsRowContentParser. This method use Type, DefaultValue, Field child elements as 
        /// argument to create ConstantsRowContentParser.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public IElementRowContentParser createRowContentParser(XmlElement elm)
        {
            IElementRowContentParser oReturn = null;
            string sType = getChildElementText(elm, "Type");
            string sDefault = getChildElementText(elm, "DefaultValue");
            string sField = getChildElementText(elm, "Field");


            if (sType != null && sField != null)
            {
                ConstantsRowContentParser.ConstantsType eType = ConstantsRowContentParser.ConstantsType.Empty;
                if (sType.Equals("URL"))
                {
                    eType = ConstantsRowContentParser.ConstantsType.URL;
                }
                oReturn = RowContentParserUtilities.createConstantRowContentParser(sField, eType, sDefault);
            }

            return oReturn;
        }

        #endregion
    }
}
