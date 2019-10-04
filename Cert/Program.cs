using System;
using System.Threading;
using System.Windows.Forms;

namespace Сerts
{
    static class Program
    {
        public static Cursor Cursor { get; private set; }

        [STAThread]
        static void Main()
        {
            if (InstallApplication.ChkInstCryptoPro() && InstallApplication.ChkInstImDisk())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                bool isFirstInstance;
                Mutex mtx = new Mutex(true, "Сerts", out isFirstInstance);
                if (isFirstInstance)
                    Application.Run(new InstallCerts());
                else MessageBox.Show("Данное приложение уже запущено", "Повторный запуск приложение", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Для работы приложения необходимо установить следующее ПО:\nКриптоПро CSP версии 4.0 и выше,\nImDisk Toolkit", "Отсутствует установленное ПО", MessageBoxButtons.OK);
                Application.Exit();
            }
        }
    }
}
    