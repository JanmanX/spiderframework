using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace fox.spider.path.builder.ui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WebStructure());
        }
    }
}