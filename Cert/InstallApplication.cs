using Microsoft.Win32;
using System;
using System.Linq;
using System.ServiceProcess;

namespace Сerts
{
    class InstallApplication
    {
        public static bool ChkInstImDisk()
        {
            ServiceController servContr = new ServiceController("ImDskSvc");
            var serviceExist = ServiceController.GetServices().Any(s => s.ServiceName == "ImDskSvc");
            return serviceExist;
        }
        public static bool ChkInstCryptoPro()
        {
            var regKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(regKey);
            var chk = 0;

            foreach (String keyName in rk.GetSubKeyNames())
            {
                RegistryKey subkey = rk.OpenSubKey(keyName);
                var displayName = subkey.GetValue("DisplayName") as string;
                var displayVersion = subkey.GetValue("DisplayVersion") as string;

                if (displayName != null && displayName.Contains("КриптоПро CSP") && displayVersion.Substring(0, 1) == "4")
                    chk++;
            }
            return (chk != 0) ? true : false;
        }
    }
}
