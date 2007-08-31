using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider
{
    /// <summary>
    /// RowContentParser Utilities 
    /// </summary>
    public class RowContentParserUtilities
    {
        /// <summary>
        ///convenient function to create a AttributeRowContentParser
        /// </summary>
        /// <param name="path"></param>
        /// <param name="attr"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static AttributeRowContentParser createAttributeRowContentParser(string path, string attr, string col)
        {

            AttributeRowContentParser oAttParser = new AttributeRowContentParser();
            oAttParser.AttributeName = attr;
            oAttParser.ColumnName = col;
            oAttParser.NodePath = path;

            return oAttParser;
        }
        /// <summary>
        /// convenient function to create a AttributeRowContentParser
        /// </summary>
        /// <param name="path"></param>
        /// <param name="attr"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static AttributeRowContentParser createAttributeRowContentParser(string path, string attr, int col)
        {

            AttributeRowContentParser oAttParser = new AttributeRowContentParser();
            oAttParser.AttributeName = attr;
            oAttParser.ColumnPosition = col;
            oAttParser.NodePath = path;

            return oAttParser;
        }

        /// <summary>
        /// convenient function to create a XPathRowContentParser
        /// </summary>
        /// <param name="path"></param>
        /// <param name="col"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public static XPathRowContentParser createXPathRowContentParser(string[] path, string[] col, bool trim)
        {
            if (path.Length != col.Length)
            {
                throw new Exception("The length of Path and Col is different. They should be equal.");
            }

            XPathRowContentParser oXPathParser = new XPathRowContentParser();
            for (int i = 0; i < path.Length; i++)
            {
                oXPathParser.addMapping(path[i], col[i]);
            }
            oXPathParser.TrimAllText = trim;
            return oXPathParser;
        }

        /// <summary>
        /// convenient function to create a XPathRowContentParser
        /// </summary>
        /// <param name="path"></param>
        /// <param name="col"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public static XPathRowContentParser createXPathRowContentParser(string[] path, int[] col, bool trim)
        {
            if (path.Length != col.Length)
            {
                throw new Exception("The length of Path and Col is different. They should be equal.");
            }

            XPathRowContentParser oXPathParser = new XPathRowContentParser();
            for (int i = 0; i < path.Length; i++)
            {
                oXPathParser.addMapping(path[i], col[i]);
            }
            oXPathParser.TrimAllText = trim;
            return oXPathParser;
        }
        /// <summary>
        /// convenient function to create a StringRowContentParser
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static StringRowContentParser createStringRowContentParser(string path, string[] regex, string[] col)
        {
            /*if (regex.Length != col.Length)
            {
                throw new Exception("The length of regex and Col is different. They should be equal.");
            }

            StringRowContentParser oReturn = new StringRowContentParser();
            oReturn.NodePath = path;
            for (int i = 0; i < regex.Length; i++)
            {
                oReturn.addMapping(regex[i], col[i]);
            }
            return oReturn;*/
            return createStringRowContentParser(path, regex, col, false);
        }

        /// <summary>
        /// convenient function to create a StringRowContentParser
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static StringRowContentParser createStringRowContentParser(string path, string[] regex, int[] col)
        {
            return createStringRowContentParser(path, regex, col, false);
        }
        /// <summary>
        /// convenient function to create a StringRowContentParser
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static StringRowContentParser createStringRowContentParser(string path, string[] regex, string[] col, bool html)
        {
            if (regex.Length != col.Length)
            {
                throw new Exception("The length of regex and Col is different. They should be equal.");
            }

            StringRowContentParser oReturn = new StringRowContentParser();
            oReturn.NodePath = path;
            oReturn.MatchHTML = html;
            for (int i = 0; i < regex.Length; i++)
            {
                oReturn.addMapping(regex[i], col[i]);
            }
            return oReturn;
        }
        /// <summary>
        /// convenient function to create a StringRowContentParser
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static StringRowContentParser createStringRowContentParser(string path, string[] regex, int[] col, bool html)
        {
            if (regex.Length != col.Length)
            {
                throw new Exception("The length of regex and Col is different. They should be equal.");
            }

            StringRowContentParser oReturn = new StringRowContentParser();
            oReturn.NodePath = path;
            oReturn.MatchHTML = html;
            for (int i = 0; i < regex.Length; i++)
            {
                oReturn.addMapping(regex[i], col[i]);
            }
            return oReturn;
        }
        /// <summary>
        /// convenient function to create a PriorityRowContentParser
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static PriorityRowContentParser createPriorityRowContentParser(string[] xpath, string target)
        {
            PriorityRowContentParser oReturn = new PriorityRowContentParser();
            for (int i = 0; i < xpath.Length; i++)
            {
                oReturn.addXPath(xpath[i]);
            }

            oReturn.ColumnName = target;
            return oReturn;
        }
        /// <summary>
        /// convenient function to create a PriorityRowContentParser
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static PriorityRowContentParser createPriorityRowContentParser(string[] xpath, int target)
        {
            PriorityRowContentParser oReturn = new PriorityRowContentParser();
            for (int i = 0; i < xpath.Length; i++)
            {
                oReturn.addXPath(xpath[i]);
            }

            oReturn.ColumnPosition = target;
            return oReturn;
        }

        /// <summary>
        /// convenient function to create a ConstantsRowContentParser
        /// </summary>
        /// <param name="field"></param>
        /// <param name="type"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ConstantsRowContentParser createConstantRowContentParser(string field, 
            ConstantsRowContentParser.ConstantsType type, object defaultValue)
        {
            ConstantsRowContentParser oReturn = new ConstantsRowContentParser();
            oReturn.Field = field;
            oReturn.Type = type;
            oReturn.DefaultValue = defaultValue;
            return oReturn;
        }
    }
}
