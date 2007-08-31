using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider;
using fox.spider.runtime.interfaces;
using fox.spider.runtime.constants;
using fox.spider.runtime.pageturners;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// A provider creates DataTableWebPageTurner.
    /// </summary>
    public class DataTableWebPageTurnerProvider : AbstractProvider, IWebPageTurnerProvider
    {
        #region IWebPageTurnerProvider Members
        /// <summary>
        /// creates a DataTableWebPageTurner.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public IWebPageTurner createWebPageTurner(XmlElement elm)
        {
            IWebPageTurner oReturn = null;

            string sTable = this.getChildElementText(elm, "DataTable");
            if (sTable == null || "".Equals(sTable))
            {
                return oReturn;
            }

            string sField = this.getChildElementText(elm, "Field");
            DataTable oTable = GlobalResource.getTable(sTable);
            if (oTable !=null && sField != null && !"".Equals(sField))
            {
                DataTablePageTurner oTurner = new DataTablePageTurner();
                oTurner.Table = oTable;
                oTurner.Field = sField;
                oReturn = oTurner;
            }
            return oReturn;
        }

        #endregion
    }
}
