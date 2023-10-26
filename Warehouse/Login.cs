using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;


namespace Warehouse
{
    internal class Login
    {

        public Login()
        {

        }
        public bool Loginin(string username, DataBaseConnect db)
        {
            string userquery = $"SELECT * FROM users WHERE name LIKE \"{username}\"";
            MySqlDataReader reader = db.ExecuteQuery(userquery);
            bool foundUser = reader.Read();
            reader.Close();
            return foundUser;
        }
        public bool IsEmployee(string username, DataBaseConnect db)
        {
            string employeeQuery = $"SELECT employee FROM users WHERE name = @username";

            MySqlCommand cmd = new MySqlCommand(employeeQuery, db.GetConnection());
            cmd.Parameters.AddWithValue("username", username);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                byte employeeFlag = reader.GetByte(0);
                return employeeFlag == 1;
            }
            return false;
        }
    }
}
