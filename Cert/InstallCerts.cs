using System;
using System.Drawing;
using System.Windows.Forms;

namespace Сerts
{
    public partial class InstallCerts : Form
    {
        static int providerType = 80; //Тип провайдера из [HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults\Provider\
        static string providerName = "Crypto-Pro GOST R 34.10-2012 Cryptographic Service Provider"; //Имя провайдера из [HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults\Provider\
        static string[] FQCN;
        static System.Timers.Timer aTimer;

        CheckBox chkbox = new CheckBox();
        
        public InstallCerts()
        {
            InitializeComponent();
            SelectStart();
            CryptoHelper.CheckExpiredMyCerts();
            aTimer = new System.Timers.Timer(int.MaxValue); //1 раз в 25 дней
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CryptoHelper.CheckExpiredMyCerts();
        }

        private static void SelectStart()
        {
            if (!NetworkHelper.CheckNwConnectoin())
            {
                if (ImDiskHelper.PathExist(ImDiskHelper.LocalPath))
                {
                    ImDiskHelper.MountImage(ImDiskHelper.LocalPath);
                    MessageBox.Show("Сетевая папка с актуальным образом недоступна\nСмонтирована последняя актуальная версия файла на локальном диске", "Ошибка подключения", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Отсутствует локальная копия образа диска в системе.\nОбраз диска не смонтирован\nВыполните монтирование вручную", "Отсутствует файл образа", MessageBoxButtons.OK);
            }
            else
            {
                var imagePath = ImDiskHelper.SelectImagePath();
                if (imagePath != null)
                {
                    ImDiskHelper.MountImage(imagePath);
                }
                else MessageBox.Show("У вас нет разрешения на использование сертификатов АТС\nОбраз диска не смонтирован", "Ошибка доступа", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Установка корневых сертификатов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstallRootCerts_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CryptoHelper.InstallCertificate();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Получение списка сертификатов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetCertsList_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
             
            dataGridViewCertsList.Columns.Clear();
            FQCN = CryptoHelper.GetContainerNames();

            if (FQCN!= null)
            {
                
                dataGridViewCertsList.DataSource = DataGridViewHelper.FillDataTable(providerType,providerName,FQCN);

                DataGridViewHelper.AddDataGridViewColumns(dataGridViewCertsList);
                DataGridViewHelper.AddDataGridViewCheckBoxColumn(dataGridViewCertsList);
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
            for (int k = 0; k <= dataGridViewCertsList.RowCount - 1; k++)
            {
                this.dataGridViewCertsList[0, k].Value = this.chkbox.Checked;
            }
            this.dataGridViewCertsList.EndEdit();
        }

        /// <summary>
        /// Добавление CheckBox в первый заголовок первого столбца
        /// </summary>
        public void AddChechBoxInColumn()
        {
            var rect = this.dataGridViewCertsList.GetCellDisplayRectangle(0, -1, true);
            chkbox.Size = new Size(13, 13);
            rect.Offset(3, 11);
            chkbox.Location = rect.Location;
            chkbox.CheckedChanged += chkBoxChange;
            dataGridViewCertsList.Controls.Add(chkbox);
        }

        /// <summary>
        /// Установка личного сертификата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstallPersonalCerts_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            var chk = 0;
            for (int i = 0; i < dataGridViewCertsList.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridViewCertsList.Rows[i].Cells[0].Value) == true)
                {
                    CryptoHelper.InstallCertificate(providerType, providerName, FQCN[Convert.ToInt32(dataGridViewCertsList.Rows[i].Cells["id"].Value)]);
                    chk++;
                }
            }
            if (chk == 0) MessageBox.Show("Не выбрано ни одного сертификата", "Установка сертификатов", MessageBoxButtons.OK);
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Выбранные сертификаты успешно установлены", "Установка сертификатов", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// При сортировке по столбцам снимать галку с CheckBox-ф из метода chkBoxChange
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewCertList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            chkbox.Checked = false;
        }

        /// <summary>
        /// Получаем список установленных сертификатов из MY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetInstalledCertsList_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            var certList = CryptoHelper.myCertsList();
            if (certList.Count != 0)
            {
                CryptoHelper.List = certList;
                var myInstalledCerts = new CertsManager();
                myInstalledCerts.Text = "Установленные сертификаты пользователя"; //Задаем заголовок формы CertsManager для списка установленных сертификатов
                myInstalledCerts.Show();
                Cursor = Cursors.Default;
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("В системе отсутствуют установленные сертификаты АТС");
            }
        }

        /// <summary>
        /// Выходим из приложения при выборе соответствующего пункта в контекстном меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripClose_Click(object sender, EventArgs e)
        {
            notifyIconCerts.Visible = false;
            Application.Exit();
        }

        /// <summary>
        /// Открываем главную форму при выборе Менеджера сертификатов в контекстном меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripManageCerts_Click(object sender, EventArgs e)
        {
            Opacity = 100;
            TopMost = true;
            Show();
        }

        /// <summary>
        /// Открываем главную форму при двойном клике мышки по иконке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Opacity = 100;
            TopMost = true;
            Show();
        }

        /// <summary>
        /// Прячем главную форму при загрузке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstallCerts_Shown(object sender, EventArgs e)
        {
            Opacity = 0;
            Hide();
        }

        /// <summary>
        /// Сворачиваем в трей при закрытиии формы "крестиком"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstallCerts_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                Visible = false;
            }
        }

        /// <summary>
        /// Отображение текущего пользователя в шапке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstallCerts_Load(object sender, EventArgs e)
        {
            Text = "Сертификаты " + ImDiskHelper.Department + ". Пользователь: " + Environment.UserName;
        }
    }
}
