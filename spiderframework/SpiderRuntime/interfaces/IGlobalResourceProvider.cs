using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using fox.spider;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// IGlobalResourceProvider provides all resources created or used by runtime, including
    /// WebBrowser, DataSet(DataTables), RelationProcessor, WebPageTurner, etc.
    /// <br/>
    /// 在一個 SpiderRuntime 中，所有的廣域資源的提供介面，它會包含由 SpiderRuntime 控制的 
    /// WebBrowser、DataSet（DataTables）、IRelationProcessor、及 IWebPageTurner …等物件。
    /// </summary>
    public interface IGlobalResourceProvider
    {
        /// <summary>
        /// get browser control.
        /// 
        /// </summary>
        /// <returns></returns>
        AxSHDocVw.AxWebBrowser getWebBrowser();

        /// <summary>
        /// get all tables 
        /// </summary>
        /// <returns></returns>
        DataSet getDataSet();

        /// <summary>
        /// get a table by id which is defined in configuration.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DataTable getTable(string id);
        /// <summary>
        /// get all tables created by runtime.
        /// </summary>
        /// <returns></returns>
        DataTable[] getTables();
        
        /// <summary>
        /// get relation processor by id which is defined in configuration.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IRelationProcessor getRelationProcessor(string id);
        /// <summary>
        /// get all relation processors
        /// </summary>
        /// <returns></returns>
        IRelationProcessor[] getRelationProcessors();

        /// <summary>
        /// get web page turner by id which is defined in configuration.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IWebPageTurner getWebPageTurner(string id);
        /// <summary>
        /// get all web page turners.
        /// </summary>
        /// <returns></returns>
        IWebPageTurner[] getWebPageTurners();
    }
}
