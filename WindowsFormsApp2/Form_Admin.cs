using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    public partial class Form_Admin : Form
    {
        ConnectionBD database = new ConnectionBD();
        public Form_Admin()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
        }

        private void Form_Admin_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ReaderID","ID");
            dataGridView1.Columns.Add("NickName","Имя");
            dataGridView1.Columns.Add("Pass","Пароль");
            var checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "IsAdmin";
            dataGridView1.Columns.Add(checkColumn);
        }

        private void ReadSingleRow(IDataRecord record)
        {
            dataGridView1.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2));
        }

        private void RefreshDataGrid()
        {
            dataGridView1.Rows.Clear();
            string query = $"Select ReaderID, NickName, Pass, is_admin from Readers";
            SqlCommand cmd = new SqlCommand(query, database.GetConnection());

            database.OpenConnection();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                ReadSingleRow(reader);
            } 
            reader.Close();
            database.CloseConnection();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            database.OpenConnection();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var id = dataGridView1.Rows[i].Cells[0].Value.ToString();
                var isadmin = dataGridView1.Rows[i].Cells[3].Value?.ToString();
                var changeQuery = $"update Readers set is_admin = '{isadmin}' where ReaderID = '{id}'";
                var comman = new SqlCommand(changeQuery, database.GetConnection());
                comman.ExecuteNonQuery();
            }
            database.CloseConnection();
        }

        private void button_Ban_Click(object sender, EventArgs e)
        {
            database.OpenConnection();
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells[0].Value);
            var deletequery = $"delete from Readers where ReaderID = '{id}'";
            var comman = new SqlCommand(deletequery, database.GetConnection());
            comman.ExecuteNonQuery();
            database.CloseConnection();
            RefreshDataGrid();
        }
    }
}
