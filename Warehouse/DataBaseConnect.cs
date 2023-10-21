using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Warehouse
{
    public class DataBaseConnect
    {
        string server = "localhost";
        string database = "warehouse"; //Name of the databse
        string uid = "root";
        string password = "";
        private MySqlConnection connection;

        // Constructor
        public DataBaseConnect()
        {
            Initialize(server, database, uid, password);
        }

        // Initialize values
        private void Initialize(string server, string database, string uid, string password)
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            connection = new MySqlConnection(connectionString);
        }

        // Open the connection
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // Close the connection
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public MySqlDataReader ExecuteQuery(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            return cmd.ExecuteReader();
        }

        public int ExecuteInsert(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            return cmd.ExecuteNonQuery();

        }
    }
}