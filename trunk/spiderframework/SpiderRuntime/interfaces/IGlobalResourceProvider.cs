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
    /// �b�@�� SpiderRuntime ���A�Ҧ����s��귽�����Ѥ����A���|�]�t�� SpiderRuntime ��� 
    /// WebBrowser�BDataSet�]DataTables�^�BIRelationProcessor�B�� IWebPageTurner �K������C
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
