using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace fox.spider
{
    /// <summary>
    /// AttributeRowContentParser retrieves the value of a attribute of a html dom node and stores it 
    /// in data column.
    /// 
    /// </summary>
    public class AttributeRowContentParser : ElementRowContentParser
    {
        /// <summary>
        /// 
        /// </summary>
        [Obsolete("Use AttributeName property instead.")]
        public static string ATTRIBUTE = "LinkHref_ATTRIBUTE";
        /// <summary>
        /// 
        /// </summary>
        [Obsolete("Use ColumnName/ColumnIndex property instead.")]
        public static string COLUMN = "LinkHref_COLUMN";
        /// <summary>
        /// 
        /// </summary>
        [Obsolete("Use NodePath property instead.")]
        public static string NODEPATH = "LinkHref_NODEPATH";

        /// <summary>
        /// the attribute name.
        /// </summary>
        public string AttributeName
        {
            get
            {
                return ColumnMapping == null ? null : (string)ColumnMapping[ATTRIBUTE];
            }
            set
            {
                if (ColumnMapping == null)
                {
                    ColumnMapping = new System.Collections.Hashtable();
                }
                ColumnMapping.Add(ATTRIBUTE, value);
            }
        }
        /// <summary>
        /// the column of the data row
        /// </summary>
        public string ColumnName
        {
            get
            {
                if (ColumnMapping == null)
                {
                    return null;
                }

                if (ColumnMapping[COLUMN] == null)
                {
                    return null;
                }

                if (ColumnMapping[COLUMN] is string)
                {
                    return (string)ColumnMapping[COLUMN];
                }
                return null;
                
            }
            set
            {
                if (ColumnMapping == null)
                {
                    ColumnMapping = new System.Collections.Hashtable();
                }
                ColumnMapping.Add(COLUMN, value);
            }
        }
        /// <summary>
        /// the column index of the data row
        /// </summary>
        public int ColumnPosition
        {
            get
            {
                if (ColumnMapping == null)
                {
                    return -1;
                }

                if (ColumnMapping[COLUMN] == null)
                {
                    return -1;
                }

                if (ColumnMapping[COLUMN] is int)
                {
                    return (int)ColumnMapping[COLUMN];
                }
                return -1;
            }
            set
            {
                if (ColumnMapping == null)
                {
                    ColumnMapping = new System.Collections.Hashtable();
                }
                ColumnMapping.Add(COLUMN, value);
            }
        }

        /// <summary>
        /// the spider path indicating the html dom node.
        /// </summary>
        public string NodePath
        {
            get
            {
                return ColumnMapping == null ? null : (string)ColumnMapping[NODEPATH];
            }
            set
            {
                if (ColumnMapping == null)
                {
                    ColumnMapping = new System.Collections.Hashtable();
                }
                ColumnMapping.Add(NODEPATH, value);
            }
        }
        /// <summary>
        /// get the attribute which is named AttribtueName from the html node which is obtained by NodePath, and 
        /// store in ColumnName/ColumnPosition column of data row.
        /// </summary>
        /// <param name="node"></param>
        protected override void parseContent(mshtml.IHTMLDOMNode node)
        {
            string sAttName = (string)ColumnMapping[ATTRIBUTE];
            string sColName = (string)ColumnMapping[COLUMN];
            string sNodePath = (string)ColumnMapping[NODEPATH];
            if (sNodePath == null)
                return;
            IHTMLDOMNode oNode = SpiderPath.selectSingleNode(node, sNodePath);
            if (oNode == null || !(oNode is IHTMLElement))
                return;
            IHTMLElement oElem = (IHTMLElement)oNode;

            if (oElem != null && sAttName != null && sColName != null)
            {
                object oData = oElem.getAttribute(sAttName, 0);
                getDataRow()[sColName] = oData.ToString();
            }
        }
    }
}
