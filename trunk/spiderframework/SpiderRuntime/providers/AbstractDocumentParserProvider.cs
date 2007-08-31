using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using fox.spider;
using fox.spider.runtime.interfaces;

namespace fox.spider.runtime.providers
{
    /// <summary>
    /// An abstract implementation of IDocumentParserProvider. It provides a lot of convenient function.
    /// All decendents are suggested to extend this class.
    /// IDocumentParserProvider 的抽像實作。
    /// </summary>
    public abstract class AbstractDocumentParserProvider : AbstractFlowProvider, IDocumentParserProvider
    {
        /// <summary>
        /// get a DataTable with specified id from GlobalResource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected DataTable getDataTable(string id)
        {
            return id != null ? GlobalResource.getTable(id) : null;
        }

        /// <summary>
        /// get a DataTable by getDataTable(id) where the id is extracted from "table" attribute of elm.
        /// If elm doesn't have table attribute, it returns null.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        protected DataTable getDataTable(XmlElement elm)
        {
            return getDataTable(elm.GetAttribute("table"));
        }
        /// <summary>
        /// get a RelationProcessor with specified id from GlobalResource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected IRelationProcessor getRelationProcessor(string id)
        {
            return id != null ? GlobalResource.getRelationProcessor(id) : null;
        }
        /// <summary>
        /// get a RelationProcessor by getRelationProcessor(id) where the id is 
        /// extracted from "relation" attribute of elm. If elm doesn't have relation
        /// attribute, it turns null.
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        protected IRelationProcessor getRelationProcessor(XmlElement elm)
        {
            return getRelationProcessor(elm.GetAttribute("relation"));
        }

        //protected bool TypeCorrection(object sender, DataRow row)
        //{
        //    return true;
        //}


        #region IDocumentParserProvider Members
        /// <summary>
        /// extended from IDocumentParserProvider
        /// </summary>
        /// <param name="elm"></param>
        /// <returns></returns>
        public abstract IDocumentParser createDocumentParser(XmlElement elm);
       
        #endregion
    }
}
