using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using fox.spider.runtime.interfaces;
using fox.spider.runtime.providers;
using fox.spider.runtime.constants;

namespace fox.spider.runtime
{
    /// <summary>
    /// 
    /// ProviderFactory records a list of mapping between XmlQualifiedName and IRuntineUnit. 
    /// All consumers can use Default to get the default instance, and use getXXXXXX to retrieve 
    /// an instance of XXXXXX provider. In general, the XmlQualifiedName mapped to xxx provider 
    /// will equals to the qualified name declared in XML configuration.
    /// 
    /// 
    /// 
    /// ProviderFactory 會記錄一串 XmlQualifiedName 與 IRuntineUnit 的對應，然後，這裡的每個 IRuntimeUnit 都應該
    /// 會是 XX Provider。Spider Runtime 及 Spider Flow 會使用這個物件來取得 XXX Provider 。
    /// 
    /// 一個 Virtual Machine 只能有一個 Provider Factory ，所以要使用 ProviderFactory.Default 來取得物件。
    /// </summary>
    public class ProviderFactory
    {
        private static ProviderFactory m_Default;

        public static ProviderFactory Default
        {
            get
            {
                if (m_Default == null)
                {
                    m_Default = new ProviderFactory();

                    m_Default.addRuntimeUnit(new XmlQualifiedName("Table", SpiderRuntimeConstants.DefaultNamespace),
                        new DefaultTableProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("ForeignKeyRelationProcessor", SpiderRuntimeConstants.DefaultNamespace),
                        new ForeignKeyRelationProcessorProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("LinkedWebPageTurner", SpiderRuntimeConstants.DefaultNamespace),
                        new LinkedWebPageTurnerProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("NamedWebPageTurner", SpiderRuntimeConstants.DefaultNamespace),
                        new NamedWebPageTurnerProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("CellBasedDocumentParser", SpiderRuntimeConstants.DefaultNamespace),
                        new CellBasedDocumentParserProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("RepeaterDocumentParser", SpiderRuntimeConstants.DefaultNamespace),
                        new RepeaterDocumentParserProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("TableDocumentParser", SpiderRuntimeConstants.DefaultNamespace),
                        new TableDocumentParserProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("AttributeRowContentParser", SpiderRuntimeConstants.DefaultNamespace),
                        new AttributeRowContentParserProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("PriorityXPathRowContentParser", SpiderRuntimeConstants.DefaultNamespace),
                        new PriorityXPathRowContentParserProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("RegexRowContentParser", SpiderRuntimeConstants.DefaultNamespace),
                        new RegexRowContentParserProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("XPathRowContentParser", SpiderRuntimeConstants.DefaultNamespace),
                        new XPathRowContentParserProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("ValueFilter", SpiderRuntimeConstants.DefaultNamespace),
                        new ValueFilterProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("UrlWebPageTurner", SpiderRuntimeConstants.DefaultNamespace),
                        new UrlWebPageTurnerProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("DataTableWebPageTurner", SpiderRuntimeConstants.DefaultNamespace),
                        new DataTableWebPageTurnerProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("ConstantsRowContentParser", SpiderRuntimeConstants.DefaultNamespace),
                        new ConstantsRowContentParserProvider());

                    m_Default.addRuntimeUnit(new XmlQualifiedName("RepeaterRowContentParser", SpiderRuntimeConstants.DefaultNamespace),
                        new RepeaterRowContentParserProvider());
                }
                return m_Default;
            }
            
        }

        public ProviderFactory()
        {
            if (m_Default != null)
            {
                throw new Exception("A virtual machine should only have one ProviderFacotry. You should use ProviderFactory.Default instead.");
            }
        }

        private Dictionary<XmlQualifiedName, IRuntimeUnit> m_Hash = new Dictionary<XmlQualifiedName, IRuntimeUnit>();

        /// <summary>
        /// add a mapping info between RuntineUnit and XmlQualifiedName.
        /// 加入 RuntineUnit 與 XmlQualifiedName 的對應。
        /// </summary>
        /// <param name="n"></param>
        /// <param name="u"></param>
        public void addRuntimeUnit(XmlQualifiedName n, IRuntimeUnit u)
        {
            if (m_Hash.ContainsKey(n))
            {
                m_Hash.Remove(n);
            }
            m_Hash.Add(n, u);
        }

        /// <summary>
        /// remove the mapping with XmlQualifiedName.
        /// 移除 XmlQualifiedName 的對應。
        /// </summary>
        /// <param name="n"></param>
        public void removeRuntimeUnit(XmlQualifiedName n)
        {
            if (m_Hash.ContainsKey(n))
            {
                m_Hash.Remove(n);
            }
        }
        
        /// <summary>
        /// get a IRuntimeUnit by XmlQualifiedName. You should check the type before using it.
        /// 以 XmlQualifiedName 來搜尋 IRuntimeUnit
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IRuntimeUnit getRuntimeUnit(XmlQualifiedName n)
        {
            if (m_Hash.ContainsKey(n))
            {
                return m_Hash[n];
            }
            return null;
        }

        /// <summary>
        /// get a IDocumentParserProvider by XmlQualifiedName.
        /// 以 XmlQualifiedName 來搜尋 IDocumentParserProvider 。
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IDocumentParserProvider getDocumentParserProvider(XmlQualifiedName n)
        {
            IDocumentParserProvider oReturn=null;
            if (m_Hash.ContainsKey(n) && m_Hash[n] is IDocumentParserProvider)
            {
                oReturn = (IDocumentParserProvider)m_Hash[n];
            }
            return oReturn;
        }
        /// <summary>
        /// get a IRelationProvider by XmlQualifiedName.
        /// 以 XmlQualifiedName 來搜尋 IRelationProvider
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IRelationProvider getRelationProvider(XmlQualifiedName n)
        {
            IRelationProvider oReturn = null;
            if (m_Hash.ContainsKey(n) && m_Hash[n] is IRelationProvider)
            {
                oReturn = (IRelationProvider)m_Hash[n];
            }
            return oReturn;
        }
        /// <summary>
        /// get a IRowContentParserProvider by XmlQualifiedName.
        /// 以 XmlQualifiedName 來搜尋 IRowContentParserProvider
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IRowContentParserProvider getRowContentParserProvider(XmlQualifiedName n)
        {
            IRowContentParserProvider oReturn = null;
            if (m_Hash.ContainsKey(n) && m_Hash[n] is IRowContentParserProvider)
            {
                oReturn = (IRowContentParserProvider)m_Hash[n];
            }
            return oReturn;
        }
        /// <summary>
        /// get a ITableProvider by XmlQualifiedName.
        /// 以 XmlQualifiedName 來搜尋 ITableProvider
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public ITableProvider getTableProvider(XmlQualifiedName n)
        {
            ITableProvider oReturn = null;
            if (m_Hash.ContainsKey(n) && m_Hash[n] is ITableProvider)
            {
                oReturn = (ITableProvider)m_Hash[n];
            }
            return oReturn;
        }
        /// <summary>
        /// get a IWebPageTurnerProvider by XmlQualifiedName.
        /// 以 XmlQualifiedName 來搜尋 IWebPageTurnerProvider
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IWebPageTurnerProvider getWebPageTurnerProvider(XmlQualifiedName n)
        {
            IWebPageTurnerProvider oReturn = null;
            if (m_Hash.ContainsKey(n) && m_Hash[n] is IWebPageTurnerProvider)
            {
                oReturn = (IWebPageTurnerProvider)m_Hash[n];
            }
            return oReturn;
        }
        /// <summary>
        /// get a IRowFilterProvider by XmlQualifiedName.
        /// 以 XmlQualifiedName 來搜尋 IRowFilterProvider
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IRowFilterProvider getRowFilterProvider(XmlQualifiedName n)
        {
            IRowFilterProvider oReturn = null;
            if (m_Hash.ContainsKey(n) && m_Hash[n] is IRowFilterProvider)
            {
                oReturn = (IRowFilterProvider)m_Hash[n];
            }
            return oReturn;
        }
        
    }
}
