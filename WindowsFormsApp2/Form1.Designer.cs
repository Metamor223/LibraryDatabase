namespace WindowsFormsApp2
{
    partial class log_in
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
            this.booksBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.libraryDataSet = new WindowsFormsApp2.LibraryDataSet();
            this.textBox_NameUser = new System.Windows.Forms.TextBox();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LogIn = new System.Windows.Forms.Button();
            this.Rigistration = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.readersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.booksTableAdapter = new WindowsFormsApp2.LibraryDataSetTableAdapters.BooksTableAdapter();
            this.tableAdapterManager = new WindowsFormsApp2.LibraryDataSetTableAdapters.TableAdapterManager();
            this.readersTableAdapter = new WindowsFormsApp2.LibraryDataSetTableAdapters.ReadersTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.booksBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.libraryDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.readersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // booksBindingSource
            // 
            this.booksBindingSource.DataMember = "Books";
            this.booksBindingSource.DataSource = this.libraryDataSet;
            // 
            // libraryDataSet
            // 
            this.libraryDataSet.DataSetName = "LibraryDataSet";
            this.libraryDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // textBox_NameUser
            // 
            this.textBox_NameUser.Location = new System.Drawing.Point(169, 116);
            this.textBox_NameUser.Name = "textBox_NameUser";
            this.textBox_NameUser.Size = new System.Drawing.Size(100, 20);
            this.textBox_NameUser.TabIndex = 1;
            // 
            // textBox_Password
            // 
            this.textBox_Password.Location = new System.Drawing.Point(169, 143);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(100, 20);
            this.textBox_Password.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Имя/NickName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Пароль/Password";
            // 
            // LogIn
            // 
            this.LogIn.Location = new System.Drawing.Point(179, 188);
            this.LogIn.Name = "LogIn";
            this.LogIn.Size = new System.Drawing.Size(75, 23);
            this.LogIn.TabIndex = 5;
            this.LogIn.Text = "Войти";
            this.LogIn.UseVisualStyleBackColor = true;
            this.LogIn.Click += new System.EventHandler(this.LogIn_Click);
            // 
            // Rigistration
            // 
            this.Rigistration.Location = new System.Drawing.Point(169, 217);
            this.Rigistration.Name = "Rigistration";
            this.Rigistration.Size = new System.Drawing.Size(90, 23);
            this.Rigistration.TabIndex = 6;
            this.Rigistration.Text = "Регистрация";
            this.Rigistration.UseVisualStyleBackColor = true;
            this.Rigistration.Click += new System.EventHandler(this.Rigistration_Click_1);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(276, 145);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(121, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Видимость пароля";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // readersBindingSource
            // 
            this.readersBindingSource.DataMember = "Readers";
            this.readersBindingSource.DataSource = this.libraryDataSet;
            // 
            // booksTableAdapter
            // 
            this.booksTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.AuthorsTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.BookLendingTableAdapter = null;
            this.tableAdapterManager.BooksTableAdapter = this.booksTableAdapter;
            this.tableAdapterManager.PublishersTableAdapter = null;
            this.tableAdapterManager.ReadersTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = WindowsFormsApp2.LibraryDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // readersTableAdapter
            // 
            this.readersTableAdapter.ClearBeforeFill = true;
            // 
            // log_in
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 286);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Rigistration);
            this.Controls.Add(this.LogIn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Password);
            this.Controls.Add(this.textBox_NameUser);
            this.Name = "log_in";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "log_in";
            this.Load += new System.EventHandler(this.log_in_Load);
            ((System.ComponentModel.ISupportInitialize)(this.booksBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.libraryDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.readersBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LibraryDataSet libraryDataSet;
        private System.Windows.Forms.BindingSource booksBindingSource;
        private LibraryDataSetTableAdapters.BooksTableAdapter booksTableAdapter;
        private LibraryDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TextBox textBox_NameUser;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LogIn;
        private System.Windows.Forms.Button Rigistration;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.BindingSource readersBindingSource;
        private LibraryDataSetTableAdapters.ReadersTableAdapter readersTableAdapter;
    }
}

