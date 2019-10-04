using System;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace certs
{
    public partial class Form1 : Form
    {
        static int providerType = 80; //Тип провайдера из [HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults\Provider\
        static string providerName = "Crypto-Pro GOST R 34.10-2012 Cryptographic Service Provider"; //Имя провайдера из [HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults\Provider\
        static string[] FQCN;

        CheckBox chkbox = new CheckBox();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Установка корневых сертификатов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstallRootCert_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Cert.CryptoHelper.InstallCertificate();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Получение списка сертификатов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetCertList_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
             
            dataGridViewCertList.Columns.Clear();

            DataTable dt = new DataTable();
            DataRow dr = default(DataRow);
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("company");
            dt.Columns.Add("date");

            if (Cert.CryptoHelper.GetContainerNames()!= null)
            {
                FQCN = Cert.CryptoHelper.GetContainerNames();
                string[] str = new string[FQCN.Length];
                string[] temp = new string[str.Length];

                for (int i = 0; i < str.Length; i++)
                {
                    X509Certificate2 certificate = Cert.CryptoHelper.GetCertificate(providerType, providerName, FQCN[i]);
                    str[i] = Program.GetFriendlyNameCert(certificate);
                    temp = str[i].Split('/');
                    dr = dt.NewRow();
                    dr["id"] = i;
                    dr["name"] = temp[0];
                    dr["company"] = temp[1];
                    dr["date"] = (Convert.ToDateTime(temp[2])).AddYears(1); //Срок действия ключей один год
                    dt.Rows.Add(dr);
                }
                dataGridViewCertList.DataSource = dt;

                AddDataGridViewColumns();
                AddDataGridViewCheckBoxColumn();
                AddChechBoxInColumn();

                Cursor = Cursors.Default;
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("В результате выполнения возникла ошибка.\nПроверьте, вставлен ли ключевой носитель.", "Возникла ошибка", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Метод выделяет и снимает выделения со всех CheckBox элементов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxChange(object sender, EventArgs e)
        {
            for (int k = 0; k <= dataGridViewCertList.RowCount - 1; k++)
            {
                this.dataGridViewCertList[0, k].Value = this.chkbox.Checked;
            }
            this.dataGridViewCertList.EndEdit();
        }

        /// <summary>
        /// Добавление столбцов в dataGridViewCertList
        /// </summary>
        private void AddDataGridViewColumns()
        {
            dataGridViewCertList.Columns["name"].HeaderText = "Владелец";
            dataGridViewCertList.Columns["company"].HeaderText = "Субъект";
            dataGridViewCertList.Columns["date"].HeaderText = "Действителен до:";

            dataGridViewCertList.Columns["id"].Visible = false;
            dataGridViewCertList.RowHeadersVisible = false;
            dataGridViewCertList.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCertList.Columns["company"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCertList.Columns["date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCertList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCertList.AllowUserToResizeRows = false;
        }

        /// <summary>
        /// Добавление CheckBox в первый заголовок первого столбца
        /// </summary>
        private void AddChechBoxInColumn()
        {
            Rectangle rect = this.dataGridViewCertList.GetCellDisplayRectangle(0, -1, true);
            chkbox.Size = new Size(13, 13);
            rect.Offset(3, 11);
            chkbox.Location = rect.Location;
            chkbox.CheckedChanged += chkBoxChange;
            dataGridViewCertList.Controls.Add(chkbox);
        }

        /// <summary>
        /// Добавление столба CheckBox-ов
        /// </summary>
        private void AddDataGridViewCheckBoxColumn()
        {
            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            colCB.Name = "chkBoxColumn";
            colCB.HeaderText = "";
            colCB.Width = 25;
            colCB.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCertList.Columns.Insert(0, colCB);
        }

        /// <summary>
        /// Установка личного сертификата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstallPersonalCert_Click(object sender, EventArgs e)
        {
            int chk = 0;
            for (int i = 0; i < dataGridViewCertList.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridViewCertList.Rows[i].Cells[0].Value) == true)
                {
                    Cert.CryptoHelper.InstallCertificate(providerType, providerName, FQCN[Convert.ToInt32(dataGridViewCertList.Rows[i].Cells["id"].Value)]);
                    chk++;
                }
            }
            if (chk==0) MessageBox.Show("Не выбрано ни одного сертификата", "Установка сертификатов", MessageBoxButtons.OK);
            else MessageBox.Show("Выбранные сертификаты успешно установлены", "Установка сертификатов", MessageBoxButtons.OK);
        }

        /// <summary>
        /// При сортировке по столбцам снимать галку с CheckBox-ф из метода chkBoxChange
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewCertList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            chkbox.Checked = false;
        }
    }
}
