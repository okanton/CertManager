using CryptoPro.Sharpei;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace Сerts
{
    public static class CryptoHelper
    {
        const uint GOST_R_3410_2012 = 0x00000050;
        const uint CRYPT_VERIFYCONTEXT = 0xF0000000;
        static readonly uint CSPKEYTYPE = 0;
        const uint PP_ENUMCONTAINERS = 0x00000002;
        const uint CRYPT_FIRST = 0x00000001;
        const uint CRYPT_UNIQUE = 0x00000008;

        public static List<X509Certificate2> List = new List<X509Certificate2>();

        /// <summary>
        /// Возвращает FQCN - полный уникальный путь к контейнеру ключа
        /// </summary>
        /// <returns></returns>
        public static string[] GetContainerNames()
        {
            int BUFFSIZE = 512;
            ArrayList containernames = new ArrayList();
            uint pcbData = 0;
            String provider = "Crypto-Pro GOST R 34.10-2012 Cryptographic Service Provider";
            String container = null;
            uint type = GOST_R_3410_2012;
            uint cspflags = CRYPT_VERIFYCONTEXT | CSPKEYTYPE;
            uint enumflags = PP_ENUMCONTAINERS;
            IntPtr hProv = IntPtr.Zero;
            uint dwFlags = CRYPT_FIRST | CRYPT_UNIQUE;

            bool gotcsp = Win32.CryptAcquireContext(ref hProv, container, provider, type, cspflags);
            if (!gotcsp)
            {
                MessageBox.Show("При загрузке сертификатов из контейнера возникла ошибка.\nПроверьте, установлен ли Крипто Про CSP 4.0.", "Возникла ошибка", MessageBoxButtons.OK);
                return null;
            }

            StringBuilder sb = null;
            Win32.CryptGetProvParam(hProv, enumflags, sb, ref pcbData, dwFlags);
            BUFFSIZE = (int)(2 * pcbData);
            sb = new StringBuilder(BUFFSIZE);

            //Получить имя контейнера
            dwFlags = CRYPT_FIRST | CRYPT_UNIQUE;
            while (Win32.CryptGetProvParam(hProv, enumflags, sb, ref pcbData, dwFlags))
            {
                dwFlags = 0;
                containernames.Add(sb.ToString());
            }
            if (hProv != IntPtr.Zero)
                Win32.CryptReleaseContext(hProv, 0);

            return (containernames.Count == 0) ? null : (string[])containernames.ToArray(Type.GetType("System.String"));
        }

        /// <summary>
        /// Получаеm сертификат по FQCN
        /// </summary>
        /// <param name="Тип провайдера"></param>
        /// <param name="Имя провайдера"></param>
        /// <param name="Полный путь до контейнера ключа - FQCN"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificate(int typeOfProvider, string nameOfProvider, string nameOfContainer)
        {
            var cspParameters = new CspParameters(typeOfProvider, nameOfProvider, nameOfContainer);

            try
            {
                var provider = new Gost3410_2012_256CryptoServiceProvider(cspParameters);
                return provider.ContainerCertificate;
            }
            catch
            {
                var provider = new Gost3410CryptoServiceProvider(cspParameters);
                return provider.ContainerCertificate;
            }
        }

        /// <summary>
        /// Установка открытых сертификатов ключа в личный контейнер
        /// </summary>
        /// <param name="Тип провайдера"></param>
        /// <param name="Имя провайдера"></param>
        /// <param name="Полный путь до контейнера ключа - FQCN"></param>
        public static void InstallCertificate(int providerType, string providerName, string containerName)
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            var cert = CryptoHelper.GetCertificate(providerType, providerName, containerName);
            store.Open(OpenFlags.ReadWrite);
            cert.FriendlyName = NamesHelper.SetShortName(cert);
            store.Add(cert);

            store.Close();
        }

        /// <summary>
        /// Установка корневых сертификатов в доверенные корневые центры сертификации
        /// </summary>
        public static void InstallCertificate()
        {
            var path = @"\\10.169.19.19\g\Work\CryptoPro_3_6\Сертификаты АТС";
            var di = new DirectoryInfo(path);
            if (di.Exists)
            {
                FileInfo[] fi = di.GetFiles();
                foreach (FileInfo fc in fi)
                {
                    var certCollection = new X509Certificate2Collection();
                    certCollection.Import(fc.FullName);
                    var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadWrite);
                    store.AddRange(certCollection);
                    store.Close();
                }
                MessageBox.Show("Сертификаты успешно установлены", "Корневые сертификаты", MessageBoxButtons.OK);
            }
            else MessageBox.Show("Сетевая папка с корневыми сертификатами недоступна", "Корневые сертификаты", MessageBoxButtons.OK);
        }

        /// <summary>
        /// Проверка просроченных сертификатов АТС в контейнере MY текущего пользователя
        /// </summary>
        public static void CheckExpiredMyCerts()
        {
            var installedCerts = myCertsList();
            var expiderCerts = new List<X509Certificate2>();

            foreach (var cert in installedCerts)
            {
                var dateCert = cert.NotBefore.Date.AddYears(1);

                if (dateCert.Date.CompareTo(DateTime.Now.Date) == -1) //
                    List.Add(cert);
            }
            if (List.Count != 0)
            {
                var myExpiredCerts = new CertsManager();
                myExpiredCerts.Text = "Просроченные сертификаты пользователя"; //Задаем заголовок формы CertsManager, если есть просроченные сертификаты
                myExpiredCerts.Show();
                MessageBox.Show("Некоторые сертификаты в хранилище пользователя устарели.", "Просроченные сертификаты", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Получаем список установленыых сертификатов АТС в контейнере MY текущего пользователя
        /// </summary>
        /// <returns></returns>
        public static List<X509Certificate2> myCertsList()
        {
            var publisherName1 = "АТС";
            var publisherName2 = "ТЕНЗОР";
            var publisherName3 = "РДК";

            var certcoll = new X509Certificate2Collection();
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            var myCertsList = new List<X509Certificate2>();

            store.Open(OpenFlags.ReadWrite);
            certcoll = store.Certificates;
            store.Close();

            foreach (var cert in certcoll)
            {
                var publisher = cert.Issuer;
                if (publisher.Contains(publisherName1) || publisher.Contains(publisherName2) || publisher.Contains(publisherName3))
                    myCertsList.Add(cert);
            }
            return myCertsList;
        }
    }
}