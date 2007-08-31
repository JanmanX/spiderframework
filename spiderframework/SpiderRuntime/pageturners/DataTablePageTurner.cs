using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using fox.spider;

namespace fox.spider.runtime.pageturners
{
    /// <summary>
    /// DataTablePageTurner turns web page by the record stored in DataTable. It maintains a cursor to point the current record.
    /// When turning, it use the specified field, supplied by configuration file, of record as url and let browser to navigate 
    /// that url.
    /// 
    /// Each turning event moves cursor to next record. So, if there are other urls need to navigate, you may append new record to 
    /// data table.
    /// 
    /// </summary>
    public class DataTablePageTurner : IWebPageTurner
    {
        private int m_Index = 0;
        private string m_Field;
        private DataTable m_Table;

        #region Properties
        /// <summary>
        /// current cursor
        /// </summary>
        public int Index
        {
            get { return m_Index; }
        }
        /// <summary>
        /// the data table for navigation.
        /// </summary>
        public DataTable Table
        {
            get { return m_Table; }
            set { m_Table = value; }
        }
        /// <summary>
        /// the field of a record.
        /// </summary>
        public string Field
        {
            get { return m_Field; }
            set { m_Field = value; }
        }

        #endregion

        #region Methods
        /// <summary>
        /// move cursor to 0
        /// </summary>
        public void resetCursor()
        {
            m_Index = 0;
        }
        /// <summary>
        /// get next url and move cursor to next
        /// </summary>
        /// <returns></returns>
        public string getNextUrl()
        {
            if (m_Index < Table.Rows.Count && Table.Columns.Contains(Field))
            {
                string sUrl = Table.Rows[m_Index][Field].ToString();
                m_Index++;
                return sUrl;
            }
            return null;
        }
        #endregion

        #region IWebPageTurner жин√
        /// <summary>
        /// turning page by data stored in the Field of the Index-th record of Table. 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public bool nextPage(System.Windows.Forms.HtmlDocument doc)
        {
            string sUrl = getNextUrl();
            if (sUrl != null)
            {
                doc.Window.Navigate(sUrl);
                return true;
            }
            if (PageNotFound != null)
            {
                PageNotFound(this, sUrl);
            }
            return false;
        }
        /// <summary>
        /// turning page by data stored in the Field of the Index-th record of Table. 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public bool nextPage(mshtml.IHTMLDocument2 doc)
        {
            string sUrl = getNextUrl();
            if (sUrl != null)
            {
                doc.parentWindow.navigate(sUrl);
                return true;
            }
            if (PageNotFound != null)
            {
                PageNotFound(this, sUrl);
            }
            return false;
        }
        /// <summary>
        /// You may supply data by this method or by Table or Field property. If obj is a DataTable, 
        /// obj is setted to Table. If obj is a string, obj is setted to Field.
        /// </summary>
        /// <param name="obj"></param>
        public void setPagingInfo(object obj)
        {
            if (obj is DataTable)
            {
                Table = (DataTable)obj;
            }
            else if (obj is string)
            {
                Field = (string)obj;
            }
        }
        /// <summary>
        /// always returns Table.
        /// </summary>
        /// <returns></returns>
        public object getPagingInfo()
        {
            return Table;
        }
        /// <summary>
        /// fired while cursor pointed to EOF of DataTable.
        /// </summary>
        public event WebPageTurnerUnpagedEvent PageNotFound;

        #endregion
    }
}
