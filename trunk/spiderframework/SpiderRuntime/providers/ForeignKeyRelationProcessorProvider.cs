using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using fox.spider;
using fox.spider.runtime.interfaces;
using fox.spider.runtime.constants;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// A provider to create a ForeignKeyRelationProcessor.
    /// 建立 ForeignKeyRelationProcessor 的實作
    /// </summary>
    public class ForeignKeyRelationProcessorProvider : AbstractProvider, IRelationProvider
    {
        private DataTable getTable(XmlElement e)
        {
            XmlNodeList oTableList = e.GetElementsByTagName("Table", SpiderRuntimeConstants.DefaultNamespace);
            if (oTableList.Count == 0)
            {
                return null;
            }
            XmlElement oTable = (XmlElement)oTableList[0];
            string sTableId = oTable.InnerText;
            return GlobalResource.getTable(sTableId);
        }

        private string getColumn(XmlElement e)
        {
            XmlNodeList oColumnList = e.GetElementsByTagName("Column", SpiderRuntimeConstants.DefaultNamespace);
            if (oColumnList.Count == 0)
            {
                return null;
            }
            XmlElement oColumn = (XmlElement)oColumnList[0];
            return oColumn.InnerText;
        }

        #region IRelationProvider Members
        /// <summary>
        /// create a ForeignKeyRelationProcessor.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public IRelationProcessor createRelationProcessor(XmlElement elm)
        {
            XmlNodeList oParentList = elm.GetElementsByTagName("Parent", SpiderRuntimeConstants.DefaultNamespace);
            if (oParentList.Count == 0)
            {
                return null;
            }

            XmlNodeList oChildList = elm.GetElementsByTagName("Child", SpiderRuntimeConstants.DefaultNamespace);
            if (oChildList.Count == 0)
            {
                return null;
            }
            string sId=elm.GetAttribute("id");

            IRelationProcessor oReturn = null;

            XmlElement oParent = (XmlElement)oParentList[0];
            XmlElement oChild = (XmlElement)oChildList[0];

            DataTable oParentTable = getTable(oParent);
            DataTable oChildTable = getTable(oChild);

            string sParentColumn = getColumn(oParent);
            string sChildColumn = getColumn(oChild);

            if (oParentTable != null && oChildTable != null && sParentColumn != null && sChildColumn != null &&
                oParentTable.Columns.Contains(sParentColumn) && oChildTable.Columns.Contains(sChildColumn))
            {
                DataRelation oRelation = new DataRelation(sId, oParentTable.Columns[sParentColumn], oChildTable.Columns[sChildColumn]);
                GlobalResource.getDataSet().Relations.Add(oRelation);

                ForeignKeyRelationProcessor oProcessor = new ForeignKeyRelationProcessor();
                oProcessor.DataRelation = oRelation;
                oReturn = oProcessor;
            }
            return oReturn;

        }

        #endregion

        
    }
}
