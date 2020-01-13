using System.Security.Cryptography.X509Certificates;

namespace Сerts
{
    class NamesHelper
    {
        /// <summary>
        /// Получаем понятное для пользователя имя контейнера
        /// </summary>
        /// <param name="X509Certificate2 сертификат"></param>
        /// <returns></returns>
        public static string GetFriendlyNameCert(X509Certificate2 cert)
        {
            var str = cert.Subject;

            //using (StreamWriter sr = new StreamWriter(@"\\10.169.19.19\Log$\2.txt", true))
            //{
            //    sr.WriteLine(cert.SerialNumber + "_" + cert.Subject + "_" + cert.Issuer);
            //    sr.Close();
            //}

            return GetNameCertOwner(str) + "/" + GetNameCompany(str) + "/" + cert.NotBefore;
        }

        /// <summary>
        /// Формируем краткое имя сертификата
        /// </summary>
        /// <param name="cert"></param>
        /// <returns></returns>
        public static string SetShortName(X509Certificate2 cert)
        {
            var str = cert.Subject;
            var shortOwnerName = GetNameCertOwner(str).Split();
            return shortOwnerName[0] + " - " + GetShortNameCompany(str);
        }

        /// <summary>
        /// Получаем понтяное для пользователя неструктурированное имя субъекта
        /// </summary>
        /// <param name="Имя субъекта"></param>
        /// <returns></returns>
        static string GetNameCompany(string company)
        {
            if (company.Contains("6315376946") && !company.Contains("VOLGSTGK")) return @"ООО ""Тензор""";
            if (company.Contains("SUNVETER")) return @"АО ""Солнечный ветер""";
            if (company.Contains("VOLGSTGK")) return @"ПАО ""Т Плюс""";
            if (company.Contains("VORKUTTC")) return @"ООО ""Воркутинские ТЭЦ""";
            if (company.Contains("CHIMPROM")) return @"ПАО ""Химпром""";
            if (company.Contains("Гарант")) return @"ООО ""ЕЭС-Гарант""";
            return company;
        }


        /// <summary>
        /// Получаем краткое имя субъекта
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        static string GetShortNameCompany(string company)
        {
            if (company.Contains("6315376946") && !company.Contains("VOLGSTGK")) return @"Тензор";
            if (company.Contains("SUNVETER")) return @"СВ";
            if (company.Contains("VOLGSTGK")) return @"Т+";
            if (company.Contains("VORKUTTC")) return @"ВТ";
            if (company.Contains("CHIMPROM")) return @"ХП";
            if (company.Contains("Гарант")) return @"Гарант";
            return company;
        }

        /// <summary>
        /// Получаем понтяное для пользователя имя владельца ключа
        /// </summary>
        /// <param name="certOwner"></param>
        /// <returns></returns>
        static string GetNameCertOwner(string certOwner)
        {
            if (certOwner.Contains("Рыжов")) return "Рыжов Алексей Владимирович";
            if (certOwner.Contains("Баев")) return "Баев Александр Николаевич";
            if (certOwner.Contains("Синцов")) return "Синцов Антон Александрович";
            if (certOwner.Contains("Казацкий")) return "Казацкий Константин Борисович";
            if (certOwner.Contains("Верховский")) return "Верховский Илья Владимирович";
            if (certOwner.Contains("Штерман")) return "Штерман Татьяна Викторовна";
            if (certOwner.Contains("Ильина")) return "Ильина Галина Игоревна";
            if (certOwner.Contains("Захарченко")) return "Захарченко Лидия Васильевна";
            if (certOwner.Contains("Каравашкина")) return "Каравашкина Елена Александровна";
            if (certOwner.Contains("Мехрякова")) return "Мехрякова Елена Сергеевна";
            if (certOwner.Contains("Дегтерев")) return "Дегтерев Григорий Александрович";
            if (certOwner.Contains("Логинова")) return "Логинова Яна Ильдаровна";
            return certOwner;
        }
    }
}
