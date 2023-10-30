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
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace Warehouse
{
    /// <summary>
    /// Interaction logic for Delete.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        private DataBaseConnect db;
        public DeleteWindow()
        {
            InitializeComponent();
            db = new DataBaseConnect();
        
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                db.OpenConnection();
                // Populate the ComboBox with items from the database
                string itemQuery = "SELECT name FROM items"; // Adjust your query as needed
                MySqlDataReader itemReader = db.ExecuteQuery(itemQuery);

                while (itemReader.Read())
                {
                    Items.Items.Add(itemReader["name"].ToString());
                }

                itemReader.Close();

                // Populate the ComboBox with locations from the database
                string locationQuery = "SELECT name FROM locations"; // Adjust your query as needed
                MySqlDataReader locationReader = db.ExecuteQuery(locationQuery);

                while (locationReader.Read())
                {
                    Locations.Items.Add(locationReader["name"].ToString());
                }

                locationReader.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occured while populating ComboBoxes: " + ex.Message);
            }
            finally
            { 
                db.CloseConnection(); 
            }
        }

          
        private int GetLocationId(string locationName)
        {
            int locationId = 0;
            try
            {
                string query = $"SELECT id FROM locations WHERE name = '{locationName}'";
                MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    locationId = Convert.ToInt32(reader["id"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred while getting location ID: " + ex.Message);
            }
            return locationId;
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string itemName = Items.SelectedItem.ToString()!;
                string locationName = Locations.SelectedItem.ToString()!;
                int quantity = int.Parse(Quantity.Text);

                db.OpenConnection();

                int locationId = GetLocationId(locationName);

                // Call the DeleteElement class to perform the delete operation
                DeleteElement deleteElement = new DeleteElement(db);
                deleteElement.DeleteItem(locationId, itemName, quantity); // Pass locationId instead of location
                BasicUserInterFace basicUserInterface = new BasicUserInterFace();
                basicUserInterface.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
    }
}
