using System.Data.SqlClient;

namespace SantoGost
{
    class Database
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=BUSCOSEXO\SQLEXPRESS;Initial Catalog=ParkGorkogo;Integrated Security=True;");

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
        }

        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open) sqlConnection.Close();
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }
    }
}
