using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using fox.spider.path.core;
using fox.spider.path.utils;

namespace fox.spider.path.impl
{
    /// <summary>
    /// IndexFormalizer formalizes a spider path which contains "{pi-index}" token.
    /// IndexFormalizer will replace "{pi-index}" as context node position in its parent.
    /// </summary>
    public class IndexFormalizer : SPathFormalizer
    {
        public const string TOKEN = "{pi-index}";

        #region SPathFormalizer Members

        public bool isSupport(object context, string p)
        {
            return context is IHTMLDOMNode && p.IndexOf(TOKEN) > -1;
        }

        public string formalize(object context, string p)
        {
            if (isSupport(context, p))
            {
                int iIndex = Utilities.getIndex((IHTMLDOMNode)context);
                return p.Replace(TOKEN, iIndex.ToString());
            }
            return string.Empty;
        }

        #endregion

    }
}
