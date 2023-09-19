using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    enum RoWState
    { 
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class FormLibrary : Form
    {
        private readonly CheckUser _user;

        ConnectionBD database = new ConnectionBD();
        int selectRow;
        //имя и пароль пользователя при авторизации
        string nameUser = DataBank.NickNameUser;
        string passUser = DataBank.PasswordUser;

        public FormLibrary(CheckUser user)
        {
            _user = user;
            InitializeComponent();
        }

        private void isAdmin()
        {
            // button_NewData.Enabled = _user.isAdmin;
            управлениеToolStripMenuItem.Enabled = _user.isAdmin;
            groupBox_Managment.Enabled = _user.isAdmin;
        }

        private void FormLibrary_Load(object sender, EventArgs e)
        {
            toolStripTextBox_UserStatus.Text = $"{_user.Login}: {_user.Status}";
            isAdmin();
            LoadProfile();
            ComboboxLoad();
            CreateColumnsBooks();
            CreateColumnsAuthors();
            CreateColumnsPublishers();
            CreateColumnsSearch();
            RefreshDataGrid(dataGridView1);
            RefreshDataGrid1(dataGridView2);
            RefreshDataGrid2(dataGridView3);
            RefreshDataGrid3(dataGridView4);
        }

        private void ComboboxLoad()
        {
            comboBox1.Items.Add("BookID");
            comboBox1.Items.Add("BookTitle");
            comboBox1.Items.Add("AuthorID");
            comboBox1.Items.Add("PublisherID");
            comboBox1.Items.Add("PublicationYear");

            comboBox2.Items.Add("AuthorID");
            comboBox2.Items.Add("FirstName");
            comboBox2.Items.Add("LastName");

            comboBox3.Items.Add("PublisherID");
            comboBox3.Items.Add("PublisherName");
        }

        private void ClearFields()
        {
            textBox_BookId.Text = "";
            textBox_BooksAuthorId.Text = "";
            textBox_BookTitle.Text = "";
            textBox_IdPublisher.Text = "";
            textBox_PublicationYear.Text = "";
            textBox_AuthorID.Text = "";
            textBox_AuthorFirstName.Text = "";
            textBox_AuthorSecondName.Text = "";
            textBox_PublisherID.Text = "";
            textBox_PublisherName.Text = "";
        }

        private void CreateColumnsBooks()
        {
            dataGridView1.Columns.Add("BookID", "Номер книги");
            dataGridView1.Columns.Add("BookTitle", "Название книги");
            dataGridView1.Columns.Add("AuthorID", "Номер автора");
            dataGridView1.Columns.Add("PublisherID", "Номер издания");
            dataGridView1.Columns.Add("PablicationYear", "Год публикации");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void CreateColumnsAuthors()
        {
            dataGridView2.Columns.Add("AuthorID", "Номер автора");
            dataGridView2.Columns.Add("FirstName", "Имя автора");
            dataGridView2.Columns.Add("LastName", "Фамилия автора");
            dataGridView2.Columns.Add("IsNew", String.Empty);
        }

        private void CreateColumnsPublishers()
        {
            dataGridView3.Columns.Add("PublisherID", "Номер издания");
            dataGridView3.Columns.Add("PublisherName", "Название издания");
            dataGridView3.Columns.Add("IsNew", String.Empty);
        }

        private void CreateColumnsSearch() 
        {
            dataGridView4.Columns.Add("FirstName", "Имя автора");
            dataGridView4.Columns.Add("LastName", "Фамилия автора");
            dataGridView4.Columns.Add("BookTitle", "Название книги");
        }


        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetInt32(3), record.GetInt32(4), RoWState.ModifiedNew);
        }

        private void ReadSingleRow1(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), RoWState.ModifiedNew);
        }

        private void ReadSingleRow2(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), RoWState.ModifiedNew);
        }

        private void ReadSingleRow3(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2));
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string querystring = $"select * from Books";
            SqlCommand command = new SqlCommand(querystring, database.GetConnection());
            database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
            database.CloseConnection();
        }
        private void RefreshDataGrid1(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string querystring = $"select * from Authors";
            SqlCommand command = new SqlCommand(querystring, database.GetConnection());
            database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow1(dgv, reader);
            }
            reader.Close();
            database.CloseConnection();
        }
        private void RefreshDataGrid2(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string querystring = $"select * from Publishers";
            SqlCommand command = new SqlCommand(querystring, database.GetConnection());
            database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow2(dgv, reader);
            }
            reader.Close();
            database.CloseConnection();
        }

        private void RefreshDataGrid3(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string querystring = $"select Authors.FirstName, Authors.LastName, Books.BookTitle" +
                                 $" from Authors, Books";
            SqlCommand command = new SqlCommand(querystring, database.GetConnection());
            database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow3(dgv, reader);
            }
            reader.Close();
            database.CloseConnection();
        }

        private void button_signout_Click(object sender, EventArgs e)
        {
            log_in log = new log_in();
            this.Close();
            log.Show();
        }

        private void Save_button_Profile_Click(object sender, EventArgs e)
        {
            log_in formlog = new log_in();

            //данные для изменения в профиле
            string Name = textBox_Name.Text;
            string SecondName = textBox_SecondName.Text;
            string Date = dateTimePicker1.Value.ToString();

            //изменение в label для профиля
            label_Name_Enter.Text = textBox_Name.Text;
            label_SecondName_Enter.Text = textBox_SecondName.Text;
            label_Date.Text = dateTimePicker1.Value.ToString();

            string queryString = $"UPDATE Readers SET FirstName = '{Name}', SecondName = '{SecondName}', DateOfBirth = '{Date}'" +
                                 $"WHERE NickName = '{nameUser}' and Pass = '{passUser}'";
            SqlCommand command = new SqlCommand(queryString, database.GetConnection());
            database.OpenConnection();
            command.ExecuteNonQuery();
            database.CloseConnection();
        }


        private void LoadProfile()
        {
            log_in formlog = new log_in();

            string queryprofileName = $"Select FirstName from Readers Where NickName = '{nameUser}' and Pass = '{passUser}'";
            string queryprofileSecondName = $"Select SecondName from Readers Where NickName = '{nameUser}' and Pass = '{passUser}'";
            string queryprofileDate = $"Select DateOfBirth from Readers Where NickName = '{nameUser}' and Pass = '{passUser}'";

            SqlCommand command1 = new SqlCommand(queryprofileName, database.GetConnection());
            SqlCommand command2 = new SqlCommand(queryprofileSecondName, database.GetConnection());
            SqlCommand command3 = new SqlCommand(queryprofileDate, database.GetConnection());

            database.OpenConnection();
            using (SqlDataReader reader1 = command1.ExecuteReader())
            {
                while (reader1.Read())
                {
                    label_Name_Enter.Text = reader1[0] as string;

                }
            }
            using (SqlDataReader reader2 = command2.ExecuteReader())
            {
                while (reader2.Read())
                {
                    label_SecondName_Enter.Text = reader2[0] as string;
                }
            }
            using (SqlDataReader reader3 = command3.ExecuteReader())
            {
                while (reader3.Read())
                {
                    label_Date.Text = reader3[0] as string;
                }
            }
            database.CloseConnection();
            //if (dataSet.Tables[0].Rows.Count == 1)
            //{
            //    byte[] data = (byte[])(dataSet.Tables[0].Rows[0]["icon"]);
            //    using (MemoryStream memoryStream = new MemoryStream(data, true))
            //    {
            //        memoryStream.Write(data, 0, data.Length);
            //        pictureBox_Profile.Image = Image.FromStream(memoryStream);
            //    }
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectRow];
            if (e.RowIndex >= 0)
            {
                textBox_BookId.Text = row.Cells[0].Value.ToString();
                textBox_BookTitle.Text = row.Cells[1].Value.ToString();
                textBox_BooksAuthorId.Text = row.Cells[2].Value.ToString();
                textBox_IdPublisher.Text = row.Cells[3].Value.ToString();
                textBox_PublicationYear.Text = row.Cells[4].Value.ToString();
            }
            comboBox1.SelectedIndex = comboBox1.FindStringExact(row.Cells[5].Value.ToString());
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[selectRow];
            if (e.RowIndex >= 0)
            {
                textBox_AuthorID.Text = row.Cells[0].Value.ToString();
                textBox_AuthorFirstName.Text = row.Cells[1].Value.ToString();
                textBox_AuthorSecondName.Text = row.Cells[2].Value.ToString();
            }
            comboBox2.SelectedIndex = comboBox2.FindStringExact(row.Cells[3].Value.ToString());
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;
            DataGridViewRow row = dataGridView3.Rows[selectRow];
            if (e.RowIndex >= 0)
            {
                textBox_PublisherID.Text = row.Cells[0].Value.ToString();
                textBox_PublisherName.Text = row.Cells[1].Value.ToString();
            }
            comboBox3.SelectedIndex = comboBox3.FindStringExact(row.Cells[2].Value.ToString());
        }

        private void SearchBooks(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"Select * from Books where concat (BookID, BookTitle, AuthorID, PublisherID, PublicationYear) like '%" + textBox_SearchBooks.Text + "%'";
            SqlCommand command = new SqlCommand(searchString, database.GetConnection());
            database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
            database.CloseConnection();
        }
        private void SearchAuthors(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"Select * from Authors where concat (AuthorID, FirstName, LastName) like '%" + textBox_SearchAuthors.Text + "%'";
            SqlCommand command = new SqlCommand(searchString, database.GetConnection());
            database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow1(dgv, reader);
            }
            reader.Close();
            database.CloseConnection();
        }
        private void SearchPublishers(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"Select * from Publishers where concat (PublisherID, PublisherName) like '%" + textBox_SearchPublishers.Text + "%'";
            SqlCommand command = new SqlCommand(searchString, database.GetConnection());
            database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow2(dgv, reader);
            }
            reader.Close();
            database.CloseConnection();
        }

        private void SearchALL(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"Select Aut.FirstName, Aut.LastName, boo.BookTitle from Books as boo " +
                                  $"left join Authors as aut ON boo.AuthorID = aut.AuthorID WHERE Aut.FirstName LIKE '%" + textBox_SearchingALL.Text + "%'";
                                  
            SqlCommand command = new SqlCommand(searchString, database.GetConnection());
            database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow3(dgv, reader);
            }
            reader.Close();
            database.CloseConnection();
        }

        private void textBox_SearchBooks_TextChanged(object sender, EventArgs e)
        {
            SearchBooks(dataGridView1);           
        }

        private void textBox_SearchAuthors_TextChanged(object sender, EventArgs e)
        {
            SearchAuthors(dataGridView2);
        }

        private void textBox_SearchPublishers_TextChanged(object sender, EventArgs e)
        {
            SearchPublishers(dataGridView3);
        }
        private void textBox_SearchingALL_TextChanged(object sender, EventArgs e)
        {
            SearchALL(dataGridView4);
        }

        private void deleteRow()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows[index].Visible = false;
                if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
                {
                    dataGridView1.Rows[index].Cells[5].Value = RoWState.Deleted;
                    return;
                }
                dataGridView1.Rows[index].Cells[5].Value = RoWState.Deleted;
            }
            if (tabControl1.SelectedIndex == 1)
            {
                int index = dataGridView2.CurrentCell.RowIndex;
                dataGridView2.Rows[index].Visible = false;
                if (dataGridView2.Rows[index].Cells[0].Value.ToString() == string.Empty)
                {
                    dataGridView2.Rows[index].Cells[3].Value = RoWState.Deleted;
                    return;
                }
                dataGridView2.Rows[index].Cells[3].Value = RoWState.Deleted;
            }
            if (tabControl1.SelectedIndex == 2)
            {
                int index = dataGridView3.CurrentCell.RowIndex;
                dataGridView3.Rows[index].Visible = false;
                if (dataGridView3.Rows[index].Cells[0].Value.ToString() == string.Empty)
                {
                    dataGridView3.Rows[index].Cells[2].Value = RoWState.Deleted;
                    return;
                }
                dataGridView3.Rows[index].Cells[2].Value = RoWState.Deleted;
            }
        }

        private void UpdateTable()
        {
            //ошибка в табпэйдж либо в отсутствии метода Change, который надо дописать
            if (tabControl1.SelectedIndex == 0)
            {
                database.OpenConnection();
                for (int index = 0; index < dataGridView1.Rows.Count-1; index++)
                {
                    RoWState rowState = (RoWState)dataGridView1.Rows[index].Cells[5].Value;
                    if (rowState == RoWState.Existed)
                    {
                        continue;
                    }
                    if (rowState == RoWState.Deleted)
                    {
                        var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                        var deleteQuery = $"delete from Books where BookID = {id}";
                        var command = new SqlCommand(deleteQuery, database.GetConnection());
                        command.ExecuteNonQuery();
                    }
                    if (rowState == RoWState.Modified)
                    {
                        var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                        var title = dataGridView1.Rows[index].Cells[1].Value.ToString();
                        var bookAuthorId = dataGridView1.Rows[index].Cells[2].Value.ToString();
                        var idPublishers = dataGridView1.Rows[index].Cells[3].Value.ToString();
                        var publication = dataGridView1.Rows[index].Cells[4].Value.ToString();

                        var changequery = $"update Books set BookTitle = '{title}', AuthorID = '{bookAuthorId}', PublisherID = {idPublishers}, PublicationYear = '{publication}' where BookID = '{id}'";
                        var command = new SqlCommand(changequery, database.GetConnection());
                        command.ExecuteNonQuery();
                    }
                }
                database.CloseConnection();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                database.OpenConnection();
                for (int index = 0; index < dataGridView2.Rows.Count-1; index++)
                {
                    var rowState = (RoWState)dataGridView2.Rows[index].Cells[3].Value;
                    if (rowState == RoWState.Existed)
                    {
                        continue;
                    }
                    if (rowState == RoWState.Deleted)
                    {
                        var id = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
                        var deleteQuery = $"delete from Authors where AuthorID = {id}";
                        var command = new SqlCommand(deleteQuery, database.GetConnection());
                        command.ExecuteNonQuery();
                    }
                    if (rowState == RoWState.Modified)
                    {
                        var Authorid = dataGridView2.Rows[index].Cells[0].Value.ToString();
                        var FirstName = dataGridView2.Rows[index].Cells[1].Value.ToString();
                        var LastName = dataGridView2.Rows[index].Cells[2].Value.ToString();

                        var changequery = $"update Authors set FirstName = '{FirstName}', LastName = '{LastName}' where Authorid = '{Authorid}'";
                        var command = new SqlCommand(changequery, database.GetConnection());
                        command.ExecuteNonQuery();
                    }
                }
                database.CloseConnection();
            }
            if (tabControl1.SelectedIndex == 2)
            {
                database.OpenConnection();
                for (int index = 0; index < dataGridView3.Rows.Count-1; index++)
                {
                    var rowState = (RoWState)dataGridView3.Rows[index].Cells[2].Value;
                    if (rowState == RoWState.Existed)
                    {
                        continue;
                    }
                    if (rowState == RoWState.Deleted)
                    {
                        var id = Convert.ToInt32(dataGridView3.Rows[index].Cells[0].Value);
                        var deleteQuery = $"delete from Publishers where PublisherID = {id}";
                        var command = new SqlCommand(deleteQuery, database.GetConnection());
                        command.ExecuteNonQuery();
                    }
                    if (rowState == RoWState.Modified)
                    {
                        var Publisherid = dataGridView3.Rows[index].Cells[0].Value.ToString();
                        var PublisherName = dataGridView3.Rows[index].Cells[1].Value.ToString();

                        var changequery = $"update Publishers set PublisherName = '{PublisherName}' where Publisherid = '{Publisherid}'";
                        var command = new SqlCommand(changequery, database.GetConnection());
                        command.ExecuteNonQuery();
                    }
                }
                database.CloseConnection();
            }
        }

        private void Change()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
                var Bookid = textBox_BookId.Text;
                var title = textBox_BookTitle.Text;
                var booksAuthorid = textBox_BooksAuthorId.Text;
                var idPublishers = textBox_IdPublisher.Text;
                var publication = textBox_PublicationYear.Text;

                if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
                {
                    dataGridView1.Rows[selectedRowIndex].SetValues(Bookid, title, booksAuthorid, idPublishers, publication);
                    dataGridView1.Rows[selectedRowIndex].Cells[5].Value = RoWState.Modified;
                }
            }
            if (tabControl1.SelectedIndex == 1)
            {
                var selectedRowIndex = dataGridView2.CurrentCell.RowIndex;
                var authorid = textBox_AuthorID.Text;
                var authorFirstName = textBox_AuthorFirstName.Text;
                var authorSecondName = textBox_AuthorSecondName.Text;

                if (dataGridView2.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
                {
                    dataGridView2.Rows[selectedRowIndex].SetValues(authorid, authorFirstName, authorSecondName);
                    dataGridView2.Rows[selectedRowIndex].Cells[3].Value = RoWState.Modified;
                }
            }
            if (tabControl1.SelectedIndex == 2)
            {
                var selectedRowIndex = dataGridView3.CurrentCell.RowIndex;
                var publisherId = textBox_PublisherID.Text;
                var publisherName = textBox_PublisherName.Text;

                if (dataGridView3.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
                {
                    dataGridView3.Rows[selectedRowIndex].SetValues(publisherId, publisherName);
                    dataGridView3.Rows[selectedRowIndex].Cells[2].Value = RoWState.Modified;
                }
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void button_Change_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
        }

        private void button_Save_Filter_Click(object sender, EventArgs e)
        {
            UpdateTable();
            ClearFields();
        }

        private void управлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Admin frmadmin = new Form_Admin();
            frmadmin.Show();
            this.Hide();
        }

        private void button_NewData_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                var Bookid = Convert.ToInt32(textBox_BookId.Text);
                var title = textBox_BookTitle.Text;
                var booksAuthorid = Convert.ToInt32(textBox_BooksAuthorId.Text);
                var idPublishers = Convert.ToInt32(textBox_IdPublisher.Text);
                var publication = Convert.ToInt32(textBox_PublicationYear.Text);

                database.OpenConnection();
                string searchString = $"Insert into Books (BookID, BookTitle, AuthorID,PublisherID,PublicationYear) values ('{Bookid}','{title}','{booksAuthorid}','{idPublishers}','{publication}');";
                SqlCommand command = new SqlCommand(searchString, database.GetConnection());
                command.ExecuteNonQuery();
                database.CloseConnection();
                RefreshDataGrid(dataGridView1);
            }
            if (tabControl1.SelectedIndex == 1)
            {
                var authorid = Convert.ToInt32(textBox_AuthorID.Text);
                var authorFirstName = textBox_AuthorFirstName.Text;
                var authorSecondName = textBox_AuthorSecondName.Text;


                database.OpenConnection();
                string searchString = $"Insert into Authors (AuthorID,FirstName,LastName) values ('{authorid}','{authorFirstName}','{authorSecondName}');";
                SqlCommand command = new SqlCommand(searchString, database.GetConnection());
                command.ExecuteNonQuery();
                database.CloseConnection();
                RefreshDataGrid1(dataGridView2);
            }
            if (tabControl1.SelectedIndex == 2)
            {
                var publisherId = Convert.ToInt32(textBox_PublisherID.Text);
                var publisherName = textBox_PublisherName.Text;

                database.OpenConnection();
                string searchString = $"Insert into Publishers (PublisherID, PublisherName) values ('{publisherId}','{publisherName}');";
                SqlCommand command = new SqlCommand(searchString, database.GetConnection());
                command.ExecuteNonQuery();
                database.CloseConnection();
                RefreshDataGrid2(dataGridView3);
            }
        }

        private void button_TakeBooks_Click(object sender, EventArgs e)
        {
            string takebook = dateTimePicker1.Value.ToString();
            string returnbook = dateTimePicker1.Value.ToString();
            var id = textBox_BookId.Text;
            var title = textBox_BookTitle.Text;
            label24.Text = takebook;
            label23.Text = returnbook;
            label26.Text = title;
            database.OpenConnection();
            string query = $"insert into BookLending (BookID, ReaderID, LendingDate, ReturnDate) values " +
                           $"('{id}',(select ReaderID from Readers where NickName = '{nameUser}' and Pass = '{passUser}'),'{takebook}','{returnbook}')";
            Console.WriteLine(query);
            SqlCommand command = new SqlCommand(query, database.GetConnection());
            command.ExecuteNonQuery();
            database.CloseConnection();
        }

        private void button_ReturnBook_Click(object sender, EventArgs e)
        {
            label24.Text = "";
            label23.Text = "";
            label26.Text = "";
            database.OpenConnection();
            string query = $"delete from BookLending where ReaderID = (select ReaderID from Readers where Pass = '{passUser}' and NickName = '{nameUser}')";
            SqlCommand command = new SqlCommand(query, database.GetConnection());
            command.ExecuteNonQuery();
            database.CloseConnection();
        }
    }
}
