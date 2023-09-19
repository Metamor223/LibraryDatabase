using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using md5;

namespace WindowsFormsApp2
{
    public partial class Registration : Form
    {
        ConnectionBD database = new ConnectionBD();
        public Registration()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            textBox_Password.PasswordChar = '⚪';
            checkBox1.Checked = false;
            textBox_Name.MaxLength = 50;
            textBox_Password.MaxLength = 50;
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

        private void Back_Click(object sender, EventArgs e)
        {
            log_in frmlog = new log_in();
            this.Close();
            frmlog.Show();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            var loginUser = textBox_Name.Text;
            string PasswordUser = textBox_Password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"insert into Readers(Pass, NickName, is_admin) values('{PasswordUser}', '{loginUser}', 0)";

            SqlCommand command = new SqlCommand(queryString, database.GetConnection());

            database.OpenConnection();
            if (checkuser() == false)
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Регистрация успешна", "Заходим", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    log_in frmlog = new log_in();
                    this.Close();
                    frmlog.Show();
                }
            }
            database.CloseConnection();
        }

        private Boolean checkuser()
        { 
           var loginUser = textBox_Name.Text;
           var PasswordUser = textBox_Password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"select * from Readers where Pass = '{PasswordUser}' and NickName = '{loginUser}'";
            SqlCommand command = new SqlCommand(queryString, database.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Аккаунт уже существует", "гуляй лесом", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
                return false;
        }
    }
}
