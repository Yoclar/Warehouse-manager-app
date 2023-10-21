using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using Microsoft.VisualBasic;

namespace Warehouse
{
    internal class Login
    {
        
        public Login()
        {
            
        }
        public bool Loginin(string username,DataBaseConnect db)
        {
            string userquery = $"SELECT * FROM users WHERE name LIKE \"{username}\"";
            MySqlDataReader reader = db.ExecuteQuery(userquery);
            bool beszéd_s = reader.Read();
            reader.Close();
            return beszéd_s;
        }
    }
}
