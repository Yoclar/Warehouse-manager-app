using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace Warehouse
{
    /// <summary>
    /// Interaction logic for BasicUserInterFace.xaml
    /// </summary>
    public partial class BasicUserInterFace : Window
    {
        // Create an instance of the DataBaseConnect class
        public DataBaseConnect db = new DataBaseConnect();
        Selected selected_location = new Selected();


        public BasicUserInterFace()
        {

            InitializeComponent();
            // Open the connection
            db.OpenConnection();
            string query = "SELECT * FROM `locations`";
            MySqlDataReader reader = db.ExecuteQuery(query);

            string result = string.Empty;
            Where.Items.Add("All the items");
            //until the table has 0 informations left 
            while (reader.Read())
            {
                //turning the table's column into string so we can write out it's content
                string columnValue = reader["name"].ToString()!;

                Where.Items.Add(columnValue);
            }
            reader.Close();
            Where.SelectedIndex = 0;
        }




        private void Where_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Getting all the information from the table

                var a = selected_location.List(Where.SelectedValue.ToString(), db);
                ItemName.Content = a[0];
                ItemCount.Content = a[1];
                //if we succesfully connecte to the database we get a success text

            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occured: " + ex.Message);


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            InsertPage ins = new InsertPage();
            ins.Show();
            this.Close();
        }
    }
}
