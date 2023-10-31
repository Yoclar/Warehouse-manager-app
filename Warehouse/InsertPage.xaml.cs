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
using System.Text.RegularExpressions;

namespace Warehouse
{
    /// <summary>
    /// Interaction logic for InsertPage.xaml
    /// </summary>
    public partial class InsertPage : Window
    {
        public DataBaseConnect db = new DataBaseConnect();
        Selected selected_location = new Selected();
        public InsertPage()
        {
            InitializeComponent();
            // Open the connection
            db.OpenConnection();
            #region location selector
            string query = "SELECT * FROM `locations`";
            MySqlDataReader reader = db.ExecuteQuery(query);

            string result = string.Empty;
            Where.Items.Add("Please select a location");
            //until the table has 0 informations left 
            while (reader.Read())
            {
                //turning the table's column into string so we can write out it's content
                string columnValue = reader["name"].ToString()!;

                Where.Items.Add(columnValue);
            }
            reader.Close();
             Where.SelectedIndex = 0;
            #endregion
            #region category selector
            string query1 = "SELECT * FROM `categories`";
            MySqlDataReader reader1 = db.ExecuteQuery(query1);

            string result1 = string.Empty;
            Category.Items.Add("Please select a category");
            //until the table has 0 informations left 
            while (reader1.Read())
            {
                //turning the table's column into string so we can write out it's content
                string columnValue = reader1["name"].ToString()!;

                Category.Items.Add(columnValue);
            }
            reader1.Close();
            Category.SelectedIndex = 0;
            #endregion
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Allow only numeric input
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Mark the event as handled to prevent non-numeric input
            }
        }

        private bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, "^[0-9]+$"); //using regex to check numeric input only
        }
        private void insert_Click(object sender, RoutedEventArgs e)
        {
            BasicUserInterFace basicUserInterFace = new BasicUserInterFace();
            Insertnewelement ins = new Insertnewelement(db);
            ins.Insert(Category.SelectedIndex, name.Text, description.Text, brand.Text, qty.Text, Where.SelectedIndex);
            basicUserInterFace.Show();
            this.Close();
        }
    }
}
