using System;
using System.Collections.Generic;
using System.Text;

namespace fox.spider.path.core
{
    /// <summary>
    /// SPathFunction is a function token used by Axis or Predicate. If this is used in Axis, path may 
    /// look like: /xxx/func()
    /// </summary>
    public interface SPathFunction : SPathTokenProcessor
    {
    }
}
