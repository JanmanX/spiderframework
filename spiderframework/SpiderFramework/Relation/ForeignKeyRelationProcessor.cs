using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider
{
    /// <summary>
    /// ForeignKeyRelationProcessor uses DataRelation to know the relationships between parent and child table.
    /// While binding, it copies data from primary key field to foreign key field.
    /// </summary>
    public class ForeignKeyRelationProcessor : RelationProcessor
    {

        private System.Data.DataRelation m_DataRelation;
        /// <summary>
        /// DataRelation used to tell ForeignKeyRelationProcessor the relationships between parent row and child row.
        /// </summary>
        public System.Data.DataRelation DataRelation
        {
            get { return m_DataRelation; }
            set { m_DataRelation = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="row"></param>
        protected override void bind(mshtml.IHTMLDOMNode node, System.Data.DataRow row)
        {
            string sParent = DataRelation.ParentColumns[0].ColumnName;
            string sChild = DataRelation.ChildColumns[0].ColumnName;
            row[sChild] = getContextRow()[sParent];
        }
    }
}
