using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using fox.spider;
using fox.spider.runtime.interfaces;
using fox.spider.runtime.constants;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// creates tables by an XmlElement.
    /// </summary>
    public class DefaultTableProvider : AbstractProvider, ITableProvider
    {
        private Type getType(string t)
        {
            if ("string".Equals(t))
            {
                return typeof(string);
            }
            else if ("int".Equals(t))
            {
                return typeof(int);
            }
            else if ("float".Equals(t))
            {
                return typeof(float);
            }
            else if ("double".Equals(t))
            {
                return typeof(double);
            }
            else
            {
                return null;
            }
        }

        #region ITableProvider Members
        /// <summary>
        /// create table.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public DataTable createTable(XmlElement elm)
        {
            DataTable oReturn = null;

            XmlNodeList oList = elm.GetElementsByTagName("Column", SpiderRuntimeConstants.DefaultNamespace);
            if (oList.Count > 0)
            {
                oReturn = new DataTable(elm.GetAttribute("id"));
                foreach (XmlNode n in oList)
                {
                    XmlElement oElem = (XmlElement)n;
                    string sName = oElem.GetAttribute("name");
                    Type oType = getType(oElem.GetAttribute("type"));
                    if (sName != null && oType != null)
                    {
                        oReturn.Columns.Add(sName, oType);
                    }
                }
            }

            return oReturn;
        }
        #endregion

    }
}
