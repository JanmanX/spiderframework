using System;
using System.Collections.Generic;
using System.Text;
using fox.spider.path.core;

namespace fox.spider.path.impl
{
    /// <summary>
    /// abstract implementation of SPathPredicate.
    /// </summary>
    public abstract class AbstractPredicate : SPathPredicate
    {
        private TokenHelper<SPathFunction> m_FuncHelper;

        #region SPathPredicate Members
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
