using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Сerts
{
    class DataGridViewHelper
    {

        /// <summary>
        /// Добавление столбцов в dataGridViewCertList
        /// </summary>
        public static void AddDataGridViewColumns(DataGridView dataGridView)
        {
            dataGridView.Columns["name"].HeaderText = "Владелец";
            dataGridView.Columns["company"].HeaderText = "Субъект";
            dataGridView.Columns["date"].HeaderText = "Дата выдачи:";
            dataGridView.Columns["expdate"].HeaderText = "Действителен до:";

            dataGridView.Columns["id"].Visible = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["company"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView.Columns["date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns["expdate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.AllowUserToResizeRows = false;
        }

        /// <summary>
        /// Добавление столбца CheckBox-ов
        /// </summary>
        public static void AddDataGridViewCheckBoxColumn(DataGridView dataGridView)
        {
            var colCB = new DataGridViewCheckBoxColumn();
            colCB.Name = "chkBoxColumn";
            colCB.HeaderText = "";
            colCB.Width = 25;
            colCB.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView.Columns.Insert(0, colCB);
        }


        /// <summary>
        /// Заполняем DataTable из списка просроченных сертификатов пользователя
        /// </summary>
        /// <param name="Список сертификатов"></param>
        /// <returns></returns>
        public static DataTable FillDataTable(List<X509Certificate2> expCerts)
        {
            var dt = new DataTable();
            var dr = default(DataRow);
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("company");
            dt.Columns.Add("date");
            dt.Columns.Add("expdate");

            string[] temp = new string[expCerts.Count];

            for (int i = 0; i < expCerts.Count; i++)
            {
                var certificate = expCerts[i];
                temp = NamesHelper.GetFriendlyNameCert(certificate).Split('/');
                dr = dt.NewRow();
                dr["id"] = i;
                dr["name"] = temp[0];
                dr["company"] = temp[1];
                dr["date"] = temp[2];
                dr["expdate"] = (Convert.ToDateTime(temp[2])).AddYears(1); //Срок действия ключей один год
                dt.Rows.Add(dr);
            }
            return dt;
        }


        /// <summary>
        /// Заполняем DataTable сертификатами из контейнера
        /// </summary>
        /// <param name="Тип провайдера"></param>
        /// <param name="Имя провайдера"></param>
        /// <param name="Уникальный путь до закрытого ключа"></param>
        /// <returns></returns>
        public static DataTable FillDataTable(int providerType, string providerName, string[] FQCN)
        {
            var dt = new DataTable();
            var dr = default(DataRow);
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("company");
            dt.Columns.Add("date");
            dt.Columns.Add("expdate");
            FQCN = CryptoHelper.GetContainerNames();
            string[] str = new string[FQCN.Length];
            string[] temp = new string[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                var certificate = CryptoHelper.GetCertificate(providerType, providerName, FQCN[i]);
                str[i] = NamesHelper.GetFriendlyNameCert(certificate);
                temp = str[i].Split('/');
                dr = dt.NewRow();
                dr["id"] = i;
                dr["name"] = temp[0];
                dr["company"] = temp[1];
                dr["date"] = temp[2];
                dr["expdate"] = (Convert.ToDateTime(temp[2])).AddYears(1); //Срок действия ключей один год
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
