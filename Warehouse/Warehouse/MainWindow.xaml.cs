using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Warehouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string server = "localhost";
            string database = "warehouse";
            string uid = "root";
            string password = "";

            // Create an instance of the DataBaseConnect class
            var db = new DataBaseConnect(server, database, uid, password);

            // Open the connection
            db.OpenConnection();

            try
            {
                // Perform your database operations here
                // ...

                string query = "SELECT * FROM `items`";

                MySqlDataReader reader = db.ExecuteQuery(query);
                
                string result = string.Empty;

                while (reader.Read()) 
                {
                    string columnValue = reader["Name"].ToString()!;


                    result += columnValue + Environment.NewLine;
                }
                queryLabel.Content= result;

                

                UpdateStatusText("..Success");

                // Close the connection when done
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during database operations
                UpdateStatusText("An error occurred: " + ex.Message);

                // Ensure the connection is still closed
                db.CloseConnection();
            }
        }


        private void UpdateStatusText(string message)
        {
            // Update a TextBlock (statusTextBlock) or Label with the message
            statusTextBlock.Content = message;
        }
    }
}
