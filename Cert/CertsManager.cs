using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Сerts
{
    public partial class CertsManager : Form
    {
        CheckBox chkbox = new CheckBox();
        List<X509Certificate2> CertList = CryptoHelper.List;

        public CertsManager()
        {
            InitializeComponent();
            GetCertList(CertList);
        }

        /// <summary>
        /// Получаем список просроченных личных сертификатов
        /// </summary>
        private void GetCertList(List<X509Certificate2> certList)
        {

            dataGridViewCerts.Columns.Clear();

            dataGridViewCerts.DataSource = DataGridViewHelper.FillDataTable(certList);
            DataGridViewHelper.AddDataGridViewColumns(dataGridViewCerts);
            DataGridViewHelper.AddDataGridViewCheckBoxColumn(dataGridViewCerts);
            AddChechBoxInColumn();

            Cursor = Cursors.Default;
        }

        public void chkBoxChange(object sender, EventArgs e)
        {
            for (int k = 0; k <= dataGridViewCerts.RowCount - 1; k++)
            {
                this.dataGridViewCerts[0, k].Value = this.chkbox.Checked;
            }
            this.dataGridViewCerts.EndEdit();
        }

        /// <summary>
        /// Добавление CheckBox в первый заголовок первого столбца
        /// </summary>
        public void AddChechBoxInColumn()
        {
            var rect = this.dataGridViewCerts.GetCellDisplayRectangle(0, -1, true);
            chkbox.Size = new Size(13, 13);
            rect.Offset(5, 11);
            chkbox.Location = rect.Location;
            chkbox.CheckedChanged += chkBoxChange;
            dataGridViewCerts.Controls.Add(chkbox);
        }

        /// <summary>
        /// Удаление просроченных личных сертификатов пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelCerts_Click(object sender, EventArgs e)
        {
            var chk = 0;
            for (int i = 0; i < dataGridViewCerts.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridViewCerts.Rows[i].Cells[0].Value) == true)
                {
                    X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadWrite);
                    store.Remove(CertList[Convert.ToInt32(dataGridViewCerts.Rows[i].Cells["id"].Value)]);
                    store.Close();
                    chk++;
                }
            }
            if (chk == 0) MessageBox.Show("Не выбрано ни одного сертификата", "Удаление просроченных сертификатов", MessageBoxButtons.OK);
            else
            {
                MessageBox.Show("Выбранные сертификаты успешно удалены", "Удаление просроченных сертификатов", MessageBoxButtons.OK);
                CertsManager.ActiveForm.Close();
            }
        }

        /// <summary>
        /// При сортировке по столбцам снимать галку с CheckBox-ф из метода chkBoxChange
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewExpiredCerts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            chkbox.Checked = false;
        }
    }
}
