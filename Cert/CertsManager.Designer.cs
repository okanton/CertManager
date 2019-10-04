namespace Сerts
{
    partial class CertsManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertsManager));
            this.dataGridViewCerts = new System.Windows.Forms.DataGridView();
            this.btnDelCerts = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCerts)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCerts
            // 
            this.dataGridViewCerts.AllowUserToAddRows = false;
            this.dataGridViewCerts.AllowUserToDeleteRows = false;
            this.dataGridViewCerts.AllowUserToResizeColumns = false;
            this.dataGridViewCerts.AllowUserToResizeRows = false;
            this.dataGridViewCerts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewCerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCerts.Location = new System.Drawing.Point(13, 13);
            this.dataGridViewCerts.Name = "dataGridViewCerts";
            this.dataGridViewCerts.Size = new System.Drawing.Size(578, 357);
            this.dataGridViewCerts.TabIndex = 0;
            this.dataGridViewCerts.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewExpiredCerts_ColumnHeaderMouseClick);
            // 
            // btnDelCerts
            // 
            this.btnDelCerts.Location = new System.Drawing.Point(12, 376);
            this.btnDelCerts.Name = "btnDelCerts";
            this.btnDelCerts.Size = new System.Drawing.Size(579, 23);
            this.btnDelCerts.TabIndex = 1;
            this.btnDelCerts.Text = "Удалить";
            this.btnDelCerts.UseVisualStyleBackColor = true;
            this.btnDelCerts.Click += new System.EventHandler(this.btnDelCerts_Click);
            // 
            // CertsManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(603, 411);
            this.Controls.Add(this.btnDelCerts);
            this.Controls.Add(this.dataGridViewCerts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(619, 450);
            this.MinimumSize = new System.Drawing.Size(619, 450);
            this.Name = "CertsManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCerts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCerts;
        private System.Windows.Forms.Button btnDelCerts;
    }
}