namespace certs
{
    partial class Form1
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
            this.btnInstallRootCert = new System.Windows.Forms.Button();
            this.btnInstallPersonalCert = new System.Windows.Forms.Button();
            this.btnGetCertList = new System.Windows.Forms.Button();
            this.dataGridViewCertList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCertList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInstallRootCert
            // 
            this.btnInstallRootCert.Location = new System.Drawing.Point(12, 12);
            this.btnInstallRootCert.Name = "btnInstallRootCert";
            this.btnInstallRootCert.Size = new System.Drawing.Size(471, 23);
            this.btnInstallRootCert.TabIndex = 4;
            this.btnInstallRootCert.Text = "Установить корневые сертификаты";
            this.btnInstallRootCert.UseVisualStyleBackColor = true;
            this.btnInstallRootCert.Click += new System.EventHandler(this.btnInstallRootCert_Click);
            // 
            // btnInstallPersonalCert
            // 
            this.btnInstallPersonalCert.Location = new System.Drawing.Point(12, 412);
            this.btnInstallPersonalCert.Name = "btnInstallPersonalCert";
            this.btnInstallPersonalCert.Size = new System.Drawing.Size(471, 37);
            this.btnInstallPersonalCert.TabIndex = 7;
            this.btnInstallPersonalCert.Text = "Установить личные сертификаты";
            this.btnInstallPersonalCert.UseVisualStyleBackColor = true;
            this.btnInstallPersonalCert.Click += new System.EventHandler(this.btnInstallPersonalCert_Click);
            // 
            // btnGetCertList
            // 
            this.btnGetCertList.Location = new System.Drawing.Point(12, 41);
            this.btnGetCertList.Name = "btnGetCertList";
            this.btnGetCertList.Size = new System.Drawing.Size(471, 23);
            this.btnGetCertList.TabIndex = 6;
            this.btnGetCertList.Text = "Загрузить сертификаты из контейнера";
            this.btnGetCertList.UseVisualStyleBackColor = true;
            this.btnGetCertList.Click += new System.EventHandler(this.btnGetCertList_Click);
            // 
            // dataGridViewCertList
            // 
            this.dataGridViewCertList.AllowUserToAddRows = false;
            this.dataGridViewCertList.AllowUserToDeleteRows = false;
            this.dataGridViewCertList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCertList.Location = new System.Drawing.Point(12, 71);
            this.dataGridViewCertList.Name = "dataGridViewCertList";
            this.dataGridViewCertList.Size = new System.Drawing.Size(471, 335);
            this.dataGridViewCertList.TabIndex = 8;
            this.dataGridViewCertList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewCertList_ColumnHeaderMouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(495, 461);
            this.Controls.Add(this.dataGridViewCertList);
            this.Controls.Add(this.btnInstallPersonalCert);
            this.Controls.Add(this.btnGetCertList);
            this.Controls.Add(this.btnInstallRootCert);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(511, 500);
            this.MinimumSize = new System.Drawing.Size(511, 500);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сертификаты Трейдинг";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCertList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInstallRootCert;
        private System.Windows.Forms.Button btnInstallPersonalCert;
        private System.Windows.Forms.Button btnGetCertList;
        private System.Windows.Forms.DataGridView dataGridViewCertList;
    }
}

