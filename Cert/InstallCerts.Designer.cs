namespace Сerts
{
    partial class InstallCerts
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallCerts));
            this.btnInstallRootCerts = new System.Windows.Forms.Button();
            this.btnInstallPersonalCerts = new System.Windows.Forms.Button();
            this.btnGetCertsList = new System.Windows.Forms.Button();
            this.dataGridViewCertsList = new System.Windows.Forms.DataGridView();
            this.btnGetInstalledCertsList = new System.Windows.Forms.Button();
            this.notifyIconCerts = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripCerts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripManageCerts = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTest = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCertsList)).BeginInit();
            this.contextMenuStripCerts.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInstallRootCerts
            // 
            this.btnInstallRootCerts.Location = new System.Drawing.Point(12, 12);
            this.btnInstallRootCerts.Name = "btnInstallRootCerts";
            this.btnInstallRootCerts.Size = new System.Drawing.Size(579, 23);
            this.btnInstallRootCerts.TabIndex = 4;
            this.btnInstallRootCerts.Text = "Установить корневые сертификаты";
            this.btnInstallRootCerts.UseVisualStyleBackColor = true;
            this.btnInstallRootCerts.Click += new System.EventHandler(this.btnInstallRootCerts_Click);
            // 
            // btnInstallPersonalCerts
            // 
            this.btnInstallPersonalCerts.Location = new System.Drawing.Point(12, 512);
            this.btnInstallPersonalCerts.Name = "btnInstallPersonalCerts";
            this.btnInstallPersonalCerts.Size = new System.Drawing.Size(579, 37);
            this.btnInstallPersonalCerts.TabIndex = 7;
            this.btnInstallPersonalCerts.Text = "Установить личные сертификаты";
            this.btnInstallPersonalCerts.UseVisualStyleBackColor = true;
            this.btnInstallPersonalCerts.Click += new System.EventHandler(this.btnInstallPersonalCerts_Click);
            // 
            // btnGetCertsList
            // 
            this.btnGetCertsList.Location = new System.Drawing.Point(12, 70);
            this.btnGetCertsList.Name = "btnGetCertsList";
            this.btnGetCertsList.Size = new System.Drawing.Size(579, 23);
            this.btnGetCertsList.TabIndex = 6;
            this.btnGetCertsList.Text = "Загрузить сертификаты из контейнера";
            this.btnGetCertsList.UseVisualStyleBackColor = true;
            this.btnGetCertsList.Click += new System.EventHandler(this.btnGetCertsList_Click);
            // 
            // dataGridViewCertsList
            // 
            this.dataGridViewCertsList.AllowUserToAddRows = false;
            this.dataGridViewCertsList.AllowUserToDeleteRows = false;
            this.dataGridViewCertsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCertsList.Location = new System.Drawing.Point(13, 99);
            this.dataGridViewCertsList.Name = "dataGridViewCertsList";
            this.dataGridViewCertsList.Size = new System.Drawing.Size(578, 407);
            this.dataGridViewCertsList.TabIndex = 8;
            this.dataGridViewCertsList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewCertList_ColumnHeaderMouseClick);
            // 
            // btnGetInstalledCertsList
            // 
            this.btnGetInstalledCertsList.Location = new System.Drawing.Point(12, 41);
            this.btnGetInstalledCertsList.Name = "btnGetInstalledCertsList";
            this.btnGetInstalledCertsList.Size = new System.Drawing.Size(579, 23);
            this.btnGetInstalledCertsList.TabIndex = 9;
            this.btnGetInstalledCertsList.Text = "Просмотреть установленные сертификаты";
            this.btnGetInstalledCertsList.UseVisualStyleBackColor = true;
            this.btnGetInstalledCertsList.Click += new System.EventHandler(this.btnGetInstalledCertsList_Click);
            // 
            // notifyIconCerts
            // 
            this.notifyIconCerts.ContextMenuStrip = this.contextMenuStripCerts;
            this.notifyIconCerts.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconCerts.Icon")));
            this.notifyIconCerts.Text = "Сертификаты Трейдинг";
            this.notifyIconCerts.Visible = true;
            this.notifyIconCerts.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStripCerts
            // 
            this.contextMenuStripCerts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripManageCerts,
            MenuStripHelper.AddMenuDiskManager(),
            this.toolStripClose});
            this.contextMenuStripCerts.Name = "contextMenuStripCerts";
            this.contextMenuStripCerts.ShowImageMargin = false;
            this.contextMenuStripCerts.Size = new System.Drawing.Size(188, 70);
            this.contextMenuStripCerts.Text = "Открыть";
            // 
            // toolStripManageCerts
            // 
            this.toolStripManageCerts.Name = "toolStripManageCerts";
            this.toolStripManageCerts.Size = new System.Drawing.Size(187, 22);
            this.toolStripManageCerts.Text = "Менеджер сертификатов";
            this.toolStripManageCerts.Click += new System.EventHandler(this.toolStripManageCerts_Click);
            // 
            // toolStripClose
            // 
            this.toolStripClose.Name = "toolStripClose";
            this.toolStripClose.Size = new System.Drawing.Size(187, 22);
            this.toolStripClose.Text = "Выход";
            this.toolStripClose.Click += new System.EventHandler(this.toolStripClose_Click);
            // 
            // toolStripTest
            // 
            this.toolStripTest.Name = "toolStripTest";
            this.toolStripTest.Size = new System.Drawing.Size(32, 19);
            this.toolStripTest.Text = "Test";
            // 
            // InstallCerts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(603, 561);
            this.Controls.Add(this.btnGetInstalledCertsList);
            this.Controls.Add(this.dataGridViewCertsList);
            this.Controls.Add(this.btnInstallPersonalCerts);
            this.Controls.Add(this.btnGetCertsList);
            this.Controls.Add(this.btnInstallRootCerts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(619, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(619, 600);
            this.Name = "InstallCerts";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сертификаты Трейдинг";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InstallCerts_FormClosing);
            this.Load += new System.EventHandler(this.InstallCerts_Load);
            this.Shown += new System.EventHandler(this.InstallCerts_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCertsList)).EndInit();
            this.contextMenuStripCerts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInstallRootCerts;
        private System.Windows.Forms.Button btnInstallPersonalCerts;
        private System.Windows.Forms.Button btnGetCertsList;
        private System.Windows.Forms.DataGridView dataGridViewCertsList;
        private System.Windows.Forms.Button btnGetInstalledCertsList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCerts;
        private System.Windows.Forms.ToolStripMenuItem toolStripManageCerts;
        private System.Windows.Forms.ToolStripMenuItem toolStripClose;
        private System.Windows.Forms.NotifyIcon notifyIconCerts;
        private System.Windows.Forms.ToolStripMenuItem toolStripTest;
    }
}

