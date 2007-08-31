using System;
using System.Collections.Generic;
using System.Text;
using fox.spider.path.core;

namespace fox.spider.path.impl
{
    /// <summary>
    /// Abstract implementation for Axis.
    /// </summary>
    public abstract class AbstractAxis : SPathAxis
    {
        private TokenHelper<SPathPredicate> m_PredicateHelper;
        private TokenHelper<SPathFunction> m_FuncHelper;
        /// <summary>
        /// extractPredicate extracts the predicate part which is wrapped in "[" and "]".
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected string extractPredicate(string token)
        {
            if (token.IndexOf("[") > 0 && token.EndsWith("]"))
            {
                string sReturn=token.Substring(token.IndexOf("[") + 1);
                return sReturn.Substring(0, sReturn.Length - 1);

            }
            return null;
        }
        /// <summary>
        /// extractNonpredicate extracts the non-predicate part which may be a function or a name.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected string extractNonpredicate(string token)
        {
            if (token.IndexOf("[") > 0 && token.EndsWith("]"))
            {
                return token.Substring(0, token.IndexOf("["));
            }
            return token;
        }

        #region SPathAxis Members
        /// <summary>
        /// base implementation.
        /// </summary>
        public TokenHelper<SPathPredicate> PredicateHelper
        {
            get
            {
                return m_PredicateHelper;
            }
            set
            {
                m_PredicateHelper = value;
            }
        }
        /// <summary>
        /// base implementation.
        /// </summary>
        public TokenHelper<SPathFunction> FunctionHelper
        {
            get
            {
                return m_FuncHelper;
            }
            set
            {
                m_FuncHelper = value;
            }
        }

        #endregion

        #region SPathTokenProcessor Members

        public abstract bool isSupport(object context, string p, string[] tokens, int index);

        public abstract object eval(object context, string p, string[] tokens, int index);

        #endregion
    }
}
