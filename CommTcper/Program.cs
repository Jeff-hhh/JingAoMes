using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace CommTcper
{
    static class Program
    {
        private static  bool _bool;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mutex = new Mutex(true, "AAA", out _bool);
            if (_bool)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new Frm_Main());
                Application.Run(new Allocation());
            }

            //else
            //{
            //    MessageBox.Show("勿重复打开", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //mutex.ReleaseMutex();

        }
    }
}
