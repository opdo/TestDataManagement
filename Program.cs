using System;
using System.Windows.Forms;
using TestDataManagement.Forms;

namespace TestDataManagement
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
            Application.Run(new ManageTestcase());
        }
    }
}
