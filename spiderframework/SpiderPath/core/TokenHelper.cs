using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.path.core
{
    /// <summary>
    /// a specialized collection class for SPathTokenProcessor decendents.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TokenHelper<T> where T : SPathTokenProcessor
    {
        private List<T> m_List = new List<T>();

        /// <summary>
        /// add a T
        /// </summary>
        /// <param name="p"></param>
        public void add(T p)
        {
            if (p != null)
            {
                m_List.Add(p);
            }
        }
        /// <summary>
        /// remove a T
        /// </summary>
        /// <param name="p"></param>
        public void remove(T p)
        {
            if (p != null && m_List.Contains(p))
            {
                m_List.Remove(p);
            }
        }

        /// <summary>
        /// call eval method of SPathTokenProcessor.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="path"></param>
        /// <param name="tokens"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public object eval(object context, string path, string[] tokens, int index)
        {
            object oReturn = null;
            foreach (T p in m_List)
            {
                if (p.isSupport(context, path, tokens, index))
                {
                    oReturn = p.eval(context, path, tokens, index);
                    if (oReturn != null)
                    {
                        return oReturn;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// call isSupport method of SPathTokenProcessor.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="path"></param>
        /// <param name="tokens"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool isSupport(object context, string path, string[] tokens, int index)
        {
            bool bReturn = false;
            foreach (T p in m_List)
            {
                if (p.isSupport(context, path, tokens, index))
                {
                    bReturn=true;
                    break;
                }
            }
            return bReturn;
        }
    }
}
