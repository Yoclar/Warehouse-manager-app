using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Warehouse
{
    internal class DeleteElement
    {
        private DataBaseConnect db;

        public DeleteElement(DataBaseConnect db)
        {
            this.db = db;
        }

        public void DeleteItem(int location, string name, int quantity)
        {
            try
            {
                // Check if the item exists in the specified location
                string query = $"SELECT items.id, items.quantity FROM `items` " +
                    $"INNER JOIN loctem ON loctem.itemid = items.id " +
                    $"INNER JOIN locations ON loctem.locationid = locations.id " +
                    $"WHERE locations.id = {location} AND items.name = \"{name}\"";

                MySqlDataReader read = db.ExecuteQuery(query);

                if (read.Read())
                {
                    int itemId = Convert.ToInt32(read["id"]);
                    int itemQuantity = Convert.ToInt32(read["quantity"]);
                    read.Close();

                    if (itemQuantity >= quantity)
                    {
                        // Update the quantity of the item
                        string updateQuantity = $"UPDATE `items` SET `quantity` = {itemQuantity - quantity} WHERE id = {itemId}";
                        MySqlCommand update = new MySqlCommand(updateQuantity, db.GetConnection());
                        update.ExecuteNonQuery();
                        MessageBox.Show($"Successfully deleted {quantity} items of {name} in the specified location.");
                        
                    }
                    else
                    {
                        MessageBox.Show($"Not enough quantity of the item to delete. There are {itemQuantity} items matching the criteria.");
                       
                    }
                }
                else
                {
                    read.Close();
                    MessageBox.Show("Item not found in the specified location.");
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex);
                
            }
        }

      
    }
}
