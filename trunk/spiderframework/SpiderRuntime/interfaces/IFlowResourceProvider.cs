using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.runtime.interfaces
{
    /// <summary>
    /// A IFlowResourceProvider provides all resources created or used by a flow, including IDocumentParser, 
    /// IElementRowContentParser and IRowFilter. 
    /// <br/>
    /// IFlowResourceProvider �O�@�� Flow ���Ҧ��� Resource Provider �A���|�]�t IDocumentParser�B
    /// IElementRowContentParser�B�� IRowFilter ������C
    /// </summary>
    public interface IFlowResourceProvider
    {
        /// <summary>
        /// get a document parser by id that is defined in configuration file. 
        /// <br/>
        /// �H id �ӷj�M IDocumentParser �C�o�̩ҥΪ� id �O IDocumentParserProvider �b���� 
        /// IDocumentParser ���ɭԡAXmlElement ���ҫ��w�� id �C
        /// </summary>
        /// <param name="id">the id in XmlElement</param>
        /// <returns>IDocumentParser</returns>
        IDocumentParser getDocumentParser(string id);
        /// <summary>
        /// get all IDocumentParser instances.
        /// </summary>
        /// <returns></returns>
        IDocumentParser[] getDocumentParsers();

        /// <summary>
        /// get a row content parser by id that is defined in configuration file.
        /// <br/>
        /// �H id �ӷj�M IElementRowContentParser �C�o�̩ҥΪ� id �O IRowContentParserProvider �b����
        /// IElementRowContentParser ���ɭԡAXmlElement ���ҫ��w�� id �C
        /// </summary>
        /// <param name="id">the id in XmlElement</param>
        /// <returns>IElementRowContentParser</returns>
        IElementRowContentParser getRowContentParser(string id);
        /// <summary>
        /// get all row content parsers.
        /// </summary>
        /// <returns></returns>
        IElementRowContentParser[] getRowContentParsers();

        /// <summary>
        /// get a row filter by id that is defined in configuration file.
        /// <br/>
        /// �H id �ӷj�M IRowFilter �C�o�̩ҥΪ� id �O IRowFilterProvider �b���� IRowFilter ���ɭԡA
        /// XmlElement ���ҫ��w�� id�C
        /// </summary>
        /// <param name="id">the id in XmlElement</param>
        /// <returns>IRowFilter</returns>
        IRowFilter getRowFilter(string id);
        /// <summary>
        /// get all IRowFilters�C
        /// </summary>
        /// <returns></returns>
        IRowFilter[] getRowFilters();
    }
}
