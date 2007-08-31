using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace fox.spider.screenshot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 1)
            {
                CameraForm oForm = new CameraForm();
                oForm.AutoURL = args[0];
                oForm.SaveLocation = args[1];
                oForm.Show();
                while (!oForm.AutoRunDone)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(100);
                }
            }
            else
            {
                Application.Run(new CameraForm());
            }

        }
    }
}