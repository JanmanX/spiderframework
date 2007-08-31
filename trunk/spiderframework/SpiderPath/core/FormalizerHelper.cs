using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.path.core
{
    /// <summary>
    /// FormalizerHelper is an assembly-wide class and hosts a lot of SPathFormalizer. This class also provide a 
    /// convenient function to invoke formalize method of SPathFormalizer.
    /// </summary>
    class FormalizerHelper
    {
        private List<SPathFormalizer> m_List = new List<SPathFormalizer>();

        public void add(SPathFormalizer p)
        {
            if (p != null)
            {
                m_List.Add(p);
            }
        }

        public void remove(SPathFormalizer p)
        {
            if (p != null && m_List.Contains(p))
            {
                m_List.Remove(p);
            }
        }


        public string formalize(object context, string path)
        {
            string sReturn = path;
            foreach (SPathFormalizer p in m_List)
            {
                if (p.isSupport(context, sReturn))
                {
                    sReturn = p.formalize(context, sReturn);
                }
            }
            return sReturn;
        }
    }
}
