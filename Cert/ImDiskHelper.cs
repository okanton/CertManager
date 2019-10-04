using LTR.IO.ImDisk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Сerts
{
    class ImDiskHelper
    {
        public static string UpdatePath;
        public static string LocalPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Documents\Keys\Keys.VFD";
        public static string Department;

        /// <summary>
        /// На основании логина пользователя из базы данных выбираем папку обновления образа
        /// </summary>
        private static void SetUpdatePath()
        {
            var c = (int)DataBaseHelper.ImageType();
            switch (c)
            {
                case 1:
                    UpdatePath = @"\\ies\prm\KEYS_TRADING\Keys_Disp.VFD";
                    Department = "управления диспетчеризации и режимов";
                    break;

                case 2:
                    UpdatePath = @"\\ies\prm\KEYS_TRADING\Keys_Ilina.VFD";
                    Department = "управления сопровождения рынков";
                    break;

                case 3:
                    UpdatePath = @"\\ies\prm\KEYS_TRADING\Keys_Zaharchenko.VFD";
                    Department = "управления сопровождения рынков";
                    break;

                case 4:
                    UpdatePath = @"\\ies\prm\KEYS_TRADING\Keys_Karavashkina.VFD";
                    Department = "управления сопровождения рынков";
                    break;

                case 5:
                    UpdatePath = @"\\ies\prm\KEYS_TRADING\Keys_Mehryakova.VFD";
                    Department = "управления сопровождения рынков";
                    break;

                case 6:
                    UpdatePath = @"\\ies\prm\KEYS_TRADING\Keys_Sintcov.VFD";
                    Department = "управления торговых операций";
                    break;

                case 0:
                    Department = "";
                    break;
            }
        }

        /// <summary>
        /// Удаляем все виртуальные диски в системме
        /// </summary>
        /// <returns></returns>
        public static bool ForceRemoveDevice()
        {
            var deviceList = ImDiskAPI.GetDeviceList();

            try
            {
                foreach (var item in deviceList)
                {
                    ImDiskAPI.ForceRemoveDevice(Convert.ToUInt32(item));
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Монтируем виртуальный флоппи-диск в память
        /// </summary>
        /// <returns></returns>
        public static void MountImage(string imagePath)
        {
            var driveLetter = ImDiskAPI.FindFreeDriveLetter() + ":";
            uint deviceID = uint.MaxValue;
            var deviceList = ImDiskAPI.GetDeviceList();

            try
            {
                    foreach (var item in deviceList)
                    {
                        if (deviceID == item)
                            deviceID++;
                    }
                    ImDiskAPI.CreateDevice(0, ImDiskFlags.DeviceTypeFD | ImDiskFlags.TypeVM, imagePath, false, driveLetter, ref deviceID);
                    MessageBox.Show("Образ диска успешно смонтирован");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка доступа", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Сравниваем побитово два файла
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <returns></returns>
        private static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;

            var fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read);
            var fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read);

            do
            {
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            fs1.Close();
            fs2.Close();

            return ((file1byte - file2byte) == 0);
        }


        public static bool PathExist(string path)
        {
            var pathFileInfo = new FileInfo(path);
            return (pathFileInfo.Exists) ? true : false;
        }

        /// <summary>
        /// Проверяем актуальность и возвращаем путь до образа
        /// </summary>
        /// <returns></returns>
        public static string SelectImagePath()
        {
            SetUpdatePath();
            if (UpdatePath != null)
            {
                var keysDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Documents\Keys\";
                var tempPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Documents\Keys\temp.VFD";
                var tempFileInfo = new FileInfo(tempPath);

                if (!PathExist(LocalPath) && !PathExist(UpdatePath)) return null;
                if (PathExist(LocalPath) && !PathExist(UpdatePath)) return LocalPath;

                if (!PathExist(LocalPath) && PathExist(UpdatePath))
                {
                    Directory.CreateDirectory(keysDirectory);
                    File.Copy(UpdatePath, LocalPath, true);
                    return LocalPath;
                }
                else
                {
                    File.Copy(UpdatePath, tempPath, true);
                    ForceRemoveDevice();
                    if (FileCompare(tempPath, LocalPath))
                    {
                        File.Delete(tempPath);
                        return LocalPath;
                    }
                    else
                    {
                        File.Copy(tempPath, LocalPath, true);
                        File.Delete(tempPath);
                        return LocalPath;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Полчучаем список реально смонтированных дисков в системе
        /// </summary>
        /// <returns></returns>
        public static List<string> MountedDisks()
        {
            var deviceNumbers = ImDiskAPI.GetDeviceList();
            var deviceList = new List<string>();
            foreach (uint deviceNumber in deviceNumbers)
            {
                try
                {
                    var diskData = LTR.IO.ImDisk.ImDiskAPI.QueryDevice(deviceNumber);
                    deviceList.Add(diskData.DriveLetter+":\\ - " + "("+ diskData.Filename.Substring(4)+")");
                }
                catch{}
            }
            return deviceList;
        }
    }
}
