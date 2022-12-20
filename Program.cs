using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AmteCreator.Internal;

namespace AmteCreator
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            }
            catch (Exception e)
            {
                Application.Run(new BugReportForm("LASTQUERY=" + Server.DEBUG_LastQuery + "\r\nException=" + e));
            }

			Application.Run(new MainForm());
		}
    }
}
