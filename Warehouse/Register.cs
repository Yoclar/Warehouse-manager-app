using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Warehouse
{
    class Register
    {
        private DataBaseConnect db;

        public Register(DataBaseConnect db)
        {
           this.db = db;

        }
        public bool RegisterUser(string username, bool isEmployed)
        {
            try
            {
                   // Open a database connection
                   db.OpenConnection();
                Login log = new Login();
                bool userExist = log.Loginin(username, db);
                if (!userExist) 
                {

                  // SQL query to insert a new user into the 'users' table
                  string insertUserQuery = "INSERT INTO users (employee, name) VALUES (@employee, @name)";

                   // Create a MySqlCommand to execute the query
                  MySqlCommand cmd = new MySqlCommand(insertUserQuery, db.GetConnection());

                 // Set the parameters for the query
                  cmd.Parameters.AddWithValue("@employee", isEmployed ? 1 : 0); // 1 for employee, 0 for non-employee
                  cmd.Parameters.AddWithValue("@name", username);

                  // Execute the SQL query and get the number of rows affected
                  int rowsAffected = cmd.ExecuteNonQuery();

                  // If at least one row is affected, return true indicating success
                  return rowsAffected > 0;
                }
                else 
                {
                    MessageBox.Show("The UserName already exists!");
                    return false; 
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, e.g., display an error message
                MessageBox.Show(ex.Message);

                // Return false to indicate registration failure
                return false;
            }
            finally
            {
                // Ensure the database connection is closed
                db.CloseConnection();
            }
        }
    }
}
