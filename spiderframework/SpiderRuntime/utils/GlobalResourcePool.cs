using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using fox.spider;
using fox.spider.runtime.interfaces;

namespace fox.spider.runtime.utils
{
    /// <summary>
    /// A default implementation of IGlobalResourceProvider.
    /// IGlobalResourceProvider 的預設實作。
    /// </summary>
    public class GlobalResourcePool : IGlobalResourceProvider
    {
        private DataSet m_DataSet;
        private Dictionary<string, IRelationProcessor> m_RelProcessorHash = new Dictionary<string, IRelationProcessor>();
        private Dictionary<string, IWebPageTurner> m_PageTurnerHash = new Dictionary<string, IWebPageTurner>();
        private AxSHDocVw.AxWebBrowser m_WebBrowser;
        /// <summary>
        /// web browser
        /// </summary>
        public AxSHDocVw.AxWebBrowser WebBrowser
        {
            get { return m_WebBrowser; }
            set { m_WebBrowser = value; }
        }
        /// <summary>
        /// a data set hosting all data tables created by SpiderRuntime.
        /// </summary>
        public DataSet DataModel
        {
            get { return m_DataSet; }
            set { m_DataSet = value; }
        }
        /// <summary>
        /// add relation processor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="p"></param>
        public void addRelationProcessor(string id, IRelationProcessor p)
        {
            m_RelProcessorHash.Add(id, p);
        }
        /// <summary>
        /// add web page turner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="t"></param>
        public void addWebPageTurner(string id, IWebPageTurner t)
        {
            m_PageTurnerHash.Add(id, t);
        }
        /// <summary>
        /// remove relation processor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool removeRelationProcessor(string id)
        {
            return m_RelProcessorHash.Remove(id);
        }
        /// <summary>
        /// remove web page turner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool removeWebPageTurner(string id)
        {
            return m_PageTurnerHash.Remove(id);
        }

        #region IGlobalResourceProvider Members
        /// <summary>
        /// get web browser
        /// </summary>
        /// <returns></returns>
        public AxSHDocVw.AxWebBrowser getWebBrowser()
        {
            return WebBrowser;
        }
        /// <summary>
        /// get data set
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet getDataSet()
        {
            return DataModel;
        }
        /// <summary>
        /// get table by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public System.Data.DataTable getTable(string id)
        {
            if (DataModel.Tables.Contains(id))
            {
                return DataModel.Tables[id];
            }
            return null;
        }
        /// <summary>
        /// get all tables
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable[] getTables()
        {
            DataTable[] aReturn = new DataTable[DataModel.Tables.Count];
            DataModel.Tables.CopyTo(aReturn, 0);
            return aReturn;
        }
        /// <summary>
        /// get relation processor by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IRelationProcessor getRelationProcessor(string id)
        {
            if (m_RelProcessorHash.ContainsKey(id))
            {
                return m_RelProcessorHash[id];
            }
            return null;
        }
        /// <summary>
        /// get all relation processors 
        /// </summary>
        /// <returns></returns>
        public IRelationProcessor[] getRelationProcessors()
        {
            IRelationProcessor[] aReturn = new IRelationProcessor[m_RelProcessorHash.Count];
            m_RelProcessorHash.Values.CopyTo(aReturn, 0);
            return aReturn;
        }
        /// <summary>
        /// get web page turner by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IWebPageTurner getWebPageTurner(string id)
        {
            if (m_PageTurnerHash.ContainsKey(id))
            {
                return m_PageTurnerHash[id];
            }
            return null;
        }
        /// <summary>
        /// get web page turners.
        /// </summary>
        /// <returns></returns>
        public IWebPageTurner[] getWebPageTurners()
        {
            IWebPageTurner[] aReturn = new IWebPageTurner[m_PageTurnerHash.Count];
            m_PageTurnerHash.Values.CopyTo(aReturn, 0);
            return aReturn;
        }

        #endregion
    }
}
