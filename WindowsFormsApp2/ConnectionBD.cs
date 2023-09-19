using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    internal class ConnectionBD
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source=DESKTOP-Q255CEJ;Initial Catalog=Library;Integrated Security=True");
        public void OpenConnection()
        {
            if (connectionString.State == System.Data.ConnectionState.Closed)
            {
                connectionString.Open();
            }
        }

        public void CloseConnection() {
            if (connectionString.State == System.Data.ConnectionState.Open)
            {
                connectionString.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return connectionString;
        }
    }
}
