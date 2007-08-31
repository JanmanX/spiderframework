using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using fox.spider;
using fox.spider.runtime.interfaces;
using fox.spider.runtime.constants;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// A provider creates a RepeaterRowContentParser by an XmlElement
    /// </summary>
    public class RepeaterRowContentParserProvider : AbstractFlowProvider, IRowContentParserProvider
    {
        /// <summary>
        /// initialize FirstPath and NextPath property of RepeaterRowContentParser.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="path"></param>
        protected void initParserPath(RepeaterRowContentParser parser, XmlNode path)
        {
            foreach (XmlNode n in path.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                XmlElement oElm = (XmlElement)n;
                if (oElm.LocalName.Equals("FirstPath") && oElm.InnerText != null)
                {
                    parser.addFirstItemPath(oElm.InnerText);
                }
                else if (oElm.LocalName.Equals("NextPath") && oElm.InnerText != null)
                {
                    parser.addNextItemPath(oElm.InnerText);
                }
            }
        }
        /// <summary>
        /// initialize parser infos.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="node"></param>
        protected void initParsingInfo(RepeaterRowContentParser parser, XmlNode node)
        {
            string sColumn = null;
            string sMatcherPath = null;
            string sMatcherRegex = null;
            string sValuePath = null;
            string sValueRegex = null;

            foreach (XmlNode n in node.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                XmlElement oElm = (XmlElement)n;
                if (oElm.LocalName.Equals("Column") && oElm.InnerText != null)
                {
                    sColumn = oElm.InnerText;
                }
                else if (oElm.LocalName.Equals("MatcherPath") && oElm.InnerText != null)
                {
                    sMatcherPath = oElm.InnerText;
                }
                else if (oElm.LocalName.Equals("MatcherRegex") && oElm.InnerText != null)
                {
                    sMatcherRegex = oElm.InnerText;
                }
                else if (oElm.LocalName.Equals("ValuePath") && oElm.InnerText != null)
                {
                    sValuePath = oElm.InnerText;
                }
                else if (oElm.LocalName.Equals("ValueRegex") && oElm.InnerText != null)
                {
                    sValueRegex = oElm.InnerText;
                }
            }

            if (sColumn != null && sMatcherPath != null & sValuePath != null)
            {
                parser.addParsingInfo(sColumn, 
                    sMatcherPath, sMatcherRegex == null ? null : new Regex(sMatcherRegex), 
                    sValuePath, sValueRegex == null ? null : new Regex(sValueRegex));
            }
        }
        /// <summary>
        /// initialize parser infos.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="info"></param>
        protected void initParsingInfo(RepeaterRowContentParser parser, XmlNodeList info)
        {
            foreach (XmlNode n in info)
            {
                initParsingInfo(parser, n);
            }
        }

        #region IRowContentParserProvider Members

        public IElementRowContentParser createRowContentParser(XmlElement elm)
        {
            IElementRowContentParser oReturn = null;
            XmlNodeList oPath = elm.GetElementsByTagName("Path", SpiderRuntimeConstants.DefaultNamespace);
            XmlNodeList oInfo = elm.GetElementsByTagName("ParsingInfo", SpiderRuntimeConstants.DefaultNamespace);
            if (oPath.Count == 0 || oInfo.Count == 0)
            {
                return oReturn;
            }
            RepeaterRowContentParser oParser = new RepeaterRowContentParser();
            try
            {
                initParserPath(oParser, oPath[0]);
                initParsingInfo(oParser, oInfo);
                oReturn = oParser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return oReturn;
        }

        #endregion

    }
}
