/*
 * WinChecker 0.3.spata-alpha
 * Authors: ZAccess, adm1nn
 * (c) 2016
 */
using System;
using System.Windows.Forms;

namespace WinChecker
{
    static class Program
    {
        public const string wVersion = "0.3";
        public const string wVersionFull = "0.3.1.47";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Environment.OSVersion.Version.Major > 5)
                if (Environment.CommandLine.Contains("autolog"))
                    goto autolog;
                else
                    goto run;
            else
                if (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 1 && Environment.OSVersion.Version.Build == 2600)
                if (Environment.CommandLine.Contains("autolog"))
                    goto autolog;
                else
                    goto run;
            else
                MessageBox.Show("WinChecker не поддерживается Windows младше XP SP3!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ///////////////////////////////////////////////////////////////////////
            autolog:
            {
                API.Log.CreateLog();
                Application.Exit();
                return;
            }
            ///////////////////////////////////////////////////////////////////////
            run:
            {
                MainForm FormMain = new MainForm();
                FormMain.Text = System.IO.Path.GetRandomFileName().Replace(".", "");
                Application.Run(FormMain);
            }
        }
        ///////////////////////////////////////////////////////////////////////
    }
}
