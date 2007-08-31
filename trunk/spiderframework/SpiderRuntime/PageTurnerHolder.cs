using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using fox.spider;
using fox.spider.runtime.interfaces;
using fox.spider.runtime.utils;
using fox.spider.runtime.constants;

namespace fox.spider.runtime
{
    /// <summary>
    /// An assembly-wide class for hosting a page turner. This class stores an url pattern,
    /// delay interval, and a created page turner.
    /// </summary>
    class PageTurnerHolder
    {

        private GlobalResourcePool m_ResourcePool;
        /// <summary>
        /// global resource
        /// </summary>
        public GlobalResourcePool ResourcePool
        {
            get { return m_ResourcePool; }
            set { m_ResourcePool = value; }
        }

        private IWebPageTurner m_PageTurner;
        /// <summary>
        /// a page turner created by this class.
        /// </summary>
        public IWebPageTurner PageTurner
        {
            get { return m_PageTurner; }
            set { m_PageTurner = value; }
        }

        private Regex m_UrlPattern;
        /// <summary>
        /// a url pattern used to match current url.
        /// </summary>
        public Regex UrlPattern
        {
            get { return m_UrlPattern; }
            set { m_UrlPattern = value; }
        }

        private int m_Delay=-1;
        /// <summary>
        /// a integer. All consumer should delay execution for this value.
        /// </summary>
        public int Delay
        {
            get { return m_Delay; }
            set { m_Delay = value; }
        }
        /// <summary>
        /// the constructor of PageTurnerHolder, in which it creates the web page turner.
        /// </summary>
        /// <param name="elm"></param>
        /// <param name="p"></param>
        public PageTurnerHolder(XmlElement elm, GlobalResourcePool p)
        {
            ResourcePool = p;
            init(elm);
        }
        /// <summary>
        /// init this class with an XmlElement.
        /// </summary>
        /// <param name="elm"></param>
        private void init(XmlElement elm)
        {
            initDelay(elm);
            PageTurner = createWebPageTurner(elm);
            ///create the url pattern
            XmlNodeList oList= elm.GetElementsByTagName("UrlPattern", SpiderRuntimeConstants.DefaultNamespace);
            if (oList != null && oList.Count > 0)
            {
                XmlElement oRegex = (XmlElement)oList.Item(0);
                UrlPattern = new Regex("\\{" + oRegex.InnerText + "\\}");
            }
        }
        /// <summary>
        /// init delay value which is stored in delay attribute.
        /// </summary>
        /// <param name="elm"></param>
        private void initDelay(XmlElement elm)
        {
            string sDelay = elm.GetAttribute("delay");
            if (sDelay != null && !"".Equals(sDelay))
            {
                try
                {
                    Delay = int.Parse(sDelay);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// create a web page turner
        /// «Ø¥ß WebPageTurner
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        private IWebPageTurner createWebPageTurner(XmlElement elem)
        {
            ///create the qualified name to get web page turner provider
            XmlQualifiedName oName = new XmlQualifiedName(elem.LocalName, elem.NamespaceURI);
            IWebPageTurnerProvider oP = ProviderFactory.Default.getWebPageTurnerProvider(oName);
            if (oP == null)
            {
                return null;
            }
            oP.setGlobalResource(ResourcePool);
            ///use web page turner provider to create web page turner instance.
            IWebPageTurner oParser = oP.createWebPageTurner(elem);
            return oParser;
        }
    }
}
