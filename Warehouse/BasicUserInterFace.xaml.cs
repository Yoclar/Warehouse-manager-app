using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
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
                items.Items.Clear();
                //Getting all the information from the table
                var b = selected_location.itemcount(Where.SelectedValue.ToString()!, db);
                for (int i = 0; i < b; i++)
                {
                    var a = selected_location.List(Where.SelectedValue.ToString()!, db, i);
                    items.Items.Add(a[0] + " " + a[2] + " " + a[1]);
                }
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteWindow del = new DeleteWindow();
            del.Show();
            this.Close();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (items.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to update.");
                return;
            }

            // Get the selected item's name
            string selectedItem = items.SelectedItem.ToString();
            string[] itemParts = selectedItem.Split(' '); // Split the item name into its components

            if (itemParts.Length < 2)
            {
                MessageBox.Show("Invalid item format.");
                return;
            }

            string name = itemParts[0]; // Get the name part
            string brand = itemParts[1]; // Get the brand part

            // Create and open the UpdateElementWindow
            UpdateElementWindow update = new UpdateElementWindow();
            update.SelectedName = selectedItem; // Pass the selected item's name to the new window

            try
            {
                if (update.ShowDialog() == true)
                {
                    string newBrand = update.NewBrand.Text;
                    string newDescription = update.NewDescription.Text;

                    // Construct the SQL query based on user input
                    string updateQuery = "UPDATE items SET";
                    List<string> updateStatements = new List<string>();

                    if (!string.IsNullOrEmpty(newBrand))
                    {
                        updateStatements.Add($"brand = '{newBrand}'");
                    }

                    if (!string.IsNullOrEmpty(newDescription))
                    {
                        updateStatements.Add($"description = '{newDescription}'");
                    }

                    if (updateStatements.Count > 0)
                    {
                        updateQuery += " " + string.Join(", ", updateStatements);
                        updateQuery += $" WHERE name = '{name}' AND brand = '{brand}'";

                        // Execute the SQL query to update the item in the database
                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, db.GetConnection());
                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Item updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show($"Failed to update item '{name}'.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No changes to update.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Clear the current items
                items.Items.Clear();

                // Implement code to reload the displayed items
                // This code should be similar to what you do initially when loading items

                var b = selected_location.itemcount(Where.SelectedValue.ToString(), db);
                for (int i = 0; i < b; i++)
                {
                    var a = selected_location.List(Where.SelectedValue.ToString(), db, i);
                    items.Items.Add(a[0] + " " + a[2] + " " + a[1]);
                }

                MessageBox.Show("Items refreshed successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while refreshing items: " + ex.Message);
            }
        }
    }
}

      
    

