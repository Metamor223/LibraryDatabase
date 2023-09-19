using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using md5;

namespace WindowsFormsApp2
{
    public partial class log_in : Form
    {
        ConnectionBD database = new ConnectionBD();

        public log_in()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void log_in_Load(object sender, EventArgs e)
        {
            //// TODO: данная строка кода позволяет загрузить данные в таблицу "libraryDataSet.Readers". При необходимости она может быть перемещена или удалена.
            //this.readersTableAdapter.Fill(this.libraryDataSet.Readers);
            //// TODO: данная строка кода позволяет загрузить данные в таблицу "libraryDataSet.Books". При необходимости она может быть перемещена или удалена.
            //this.booksTableAdapter.Fill(this.libraryDataSet.Books);
            textBox_Password.PasswordChar = '⚪';
            checkBox1.Checked = false;
            textBox_NameUser.MaxLength = 50;
            textBox_Password.MaxLength = 50;
        }

        private void Rigistration_Click_1(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }

        private void LogIn_Click(object sender, EventArgs e)
        {
            var loginUser = textBox_NameUser.Text;
            var PasswordUser = textBox_Password.Text;
            DataBank.NickNameUser = textBox_NameUser.Text;
            DataBank.PasswordUser = textBox_Password.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"select ReaderID, Pass, NickName, is_admin from Readers where NickName = '{loginUser}' and Pass = '{PasswordUser}'";

            SqlCommand command = new SqlCommand(queryString, database.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                CheckUser user = new CheckUser(table.Rows[0].ItemArray[2].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]));
                MessageBox.Show("Вы успешно вошли!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormLibrary frmlib = new FormLibrary(user);
                frmlib.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Нет аккаунта", "Создайте аккаунт", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        protected void SaveData()
        { 
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) 
            {
                textBox_Password.PasswordChar = '\0';
            }
            else
                textBox_Password.PasswordChar = '⚪';
        }
    }
}
