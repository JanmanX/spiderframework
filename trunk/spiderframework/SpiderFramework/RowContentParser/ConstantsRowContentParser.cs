using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using mshtml;


namespace fox.spider
{
    /// <summary>
    /// ConstantsRowContentParser fills data column with constant value.
    /// </summary>
    public class ConstantsRowContentParser : ElementRowContentParser
    {
        
        private object m_Field;
        private ConstantsType m_Type;
        private object m_DefaultValue;

        /// <summary>
        /// the constant value
        /// </summary>
        public object DefaultValue
        {
            get { return m_DefaultValue; }
            set { m_DefaultValue = value; }
        }
        /// <summary>
        /// the column name or index
        /// </summary>
        public object Field
        {
            get { return m_Field; }
            set { m_Field = value; }
        }
        /// <summary>
        /// type of constant. If you specify empty as Type property , it will fill 
        /// default value to data column. If yo specify URL as type property, it will
        /// fill the url from whom downloaded this html document.
        /// </summary>
        public ConstantsType Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }
        /// <summary>
        /// fill constant value in data column
        /// </summary>
        /// <param name="node"></param>
        protected override void parseContent(IHTMLDOMNode node)
        {
            object oValue = null;
            switch (Type)
            {
                case ConstantsType.URL:
                    IHTMLDOMNode2 oNode = (IHTMLDOMNode2)node;
                    IHTMLDocument2 oDoc = (IHTMLDocument2)oNode.ownerDocument;
                    oValue = oDoc.url;
                    break;
                default:
                    oValue = DefaultValue;
                    break;
            }
            this.setFieldValue(Field, oValue);
        }
        /// <summary>
        /// ConstantsType has two value, Empty, and URL. 
        /// </summary>
        public enum ConstantsType
        {
            Empty,
            URL
        }
    }
}

