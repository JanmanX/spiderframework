using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using mshtml;
using fox.spider.path.core;
using fox.spider.path.impl;

namespace fox.spider.path
{
    /// <summary>
    /// SPath is the core Spider Path class. SPath provides a mechanism to evaluate a XPath like path. 
    /// You may also build a customized path system by changing axis, predicate, and formalizer of SPath.
    /// 
    /// By default, SPath supports CurrentAxis, ParentAxis, FollowingSiblingAxis, PrecedingSiblingAxis, 
    /// LastChildAxis, FollowingPrecedingSiblingAxis, AttributeAxis, and NamedAxis as its axis, 
    /// IndexFormalizer as its formalizer, and IndexPredicate as its predicate by default. 
    /// 
    /// 
    /// </summary>
    public class SPath
    {

        private TokenHelper<SPathAxis> m_AxesHelper = new TokenHelper<SPathAxis>();
        private TokenHelper<SPathFunction> m_FuncHelper = new TokenHelper<SPathFunction>();
        private TokenHelper<SPathPredicate> m_PredicateHelper = new TokenHelper<SPathPredicate>();

        private FormalizerHelper m_FormalizerHelper = new FormalizerHelper();

        public SPath()
        {
            initDefaultSupports();
        }
        /// <summary>
        /// initialize default supports. if you don't want it, just override it.
        /// </summary>
        protected void initDefaultSupports()
        {
            this.addAxis(new CurrentAxis());
            this.addAxis(new ParentAxis());
            this.addAxis(new FollowingSiblingAxis());
            this.addAxis(new PrecedingSiblingAxis());
            this.addAxis(new LastChildAxis());
            this.addAxis(new FollowingPrecedingSiblingAxis());
            this.addAxis(new AttributeAxis());
            this.addAxis(new NamedAxis());

            this.addFormalizer(new IndexFormalizer());

            this.addPredicate(new IndexPredicate());
        }

        /// <summary>
        /// add an Axis
        /// </summary>
        /// <param name="a"></param>
        public void addAxis(SPathAxis a)
        {
            if (a != null)
            {
                m_AxesHelper.add(a);
                a.FunctionHelper = m_FuncHelper;
                a.PredicateHelper = m_PredicateHelper;
            }
        }
        /// <summary>
        /// add a function
        /// </summary>
        /// <param name="f"></param>
        public void addFunction(SPathFunction f)
        {
            if (f != null)
            {
                m_FuncHelper.add(f);
            }
        }
        /// <summary>
        /// add a predicate
        /// </summary>
        /// <param name="p"></param>
        public void addPredicate(SPathPredicate p)
        {
            if (p != null)
            {
                m_PredicateHelper.add(p);
                p.FunctionHelper = m_FuncHelper;
            }
        }
        /// <summary>
        /// add a formalizer
        /// </summary>
        /// <param name="f"></param>
        public void addFormalizer(SPathFormalizer f)
        {
            if (f != null)
            {
                m_FormalizerHelper.add(f);
            }
        }
        /// <summary>
        /// select single node
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public object selectSingleNode(IHTMLDocument2 doc, string path)
        {
            string sBodyPath = path.Substring(6);
            return selectSingleNode(doc.body, sBodyPath);
        }
        /// <summary>
        /// select single node
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public object selectSingleNode(HtmlDocument doc, string path)
        {
            string sBodyPath = path.Substring(6);
            return selectSingleNode(doc.Body.DomElement, sBodyPath);
        }
        /// <summary>
        /// select single node
        /// </summary>
        /// <param name="context"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public object selectSingleNode(object context, string path)
        {
            string sFormalPath = m_FormalizerHelper.formalize(context, path);
            string[] aToken = sFormalPath.Split('/');
            object oContext = context;
            for (int i = 0; i < aToken.Length && oContext!=null; i++)
            {
                if (aToken.Equals(""))
                {
                    oContext=null;
                    break;
                }
                else if (m_AxesHelper.isSupport(oContext, aToken[i], aToken, i))
                {
                    oContext = m_AxesHelper.eval(oContext, aToken[i], aToken, i);
                }
                else if (m_FuncHelper.isSupport(oContext, aToken[i], aToken, i))
                {
                    oContext = m_FuncHelper.eval(oContext, aToken[i], aToken, i);
                }
                else//Nobody supports this kind of token. a dummy token...
                {
                    oContext = null;
                    break;
                }
            }
            return oContext;
        }
    }
}
