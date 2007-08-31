using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using mshtml;

namespace fox.spider.screenshot
{
    public delegate void DHTMLEvent(IHTMLEventObj obj);

    [ComVisible(true)]
    public class DHTMLEventHandler
    {
        public DHTMLEvent Handler;

        private IHTMLDocument2 Document;

        public DHTMLEventHandler(IHTMLDocument2 doc)
        {
            Document = doc;
        }

        [DispId(0)]
        public void Call()
        {
            Handler(Document.parentWindow.@event);
        }
    }
}
