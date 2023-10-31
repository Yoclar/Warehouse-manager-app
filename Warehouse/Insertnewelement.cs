using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Warehouse
{
    internal class Insertnewelement
    {
        private DataBaseConnect db;
        public Insertnewelement(DataBaseConnect db)
        {
            this.db = db;
        }
        public void Insert(int category, string name, string desc, string brand, string quantity,int location)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(brand) || string.IsNullOrWhiteSpace(quantity))
                {
                    MessageBox.Show("Please fill in all required fields");
                    return;
                }
                if (int.Parse(quantity) <= 0)
                {
                    MessageBox.Show("You can't add negative amount of item(s), Please visit your doctor");
                }
                int qty = int.Parse(quantity);

                //megnézi hogy van e már ilyen tárgy
                string query = $"SELECT * FROM `items`" +
                    $"Inner Join loctem on loctem.itemid=items.id " +
                    $"INNER Join locations On loctem.locationid=locations.id " +
                    $" WHERE categoryid={category} AND items.name = \"{name}\" AND brand=\"{brand}\" AND locations.id={location} ";
                MySqlDataReader read = db.ExecuteQuery(query);
                if (read.Read())
                {
                    string id = read["id"].ToString()!;
                    read.Close();
                    string query2= $"SELECT SUM(items.quantity) as endqty FROM `items`Inner Join loctem on loctem.itemid=items.id INNER Join locations On loctem.locationid=locations.id WHERE locations.id={location} GROUP BY loctem.locationid;";
                    MySqlDataReader read2 = db.ExecuteQuery(query2);
                    read2.Read();
                    int endqty = int.Parse(read2["endqty"].ToString()!);
                    read2.Close();
                    string query3 = $"Select * from `locations` WHERE id = {location}";
                    MySqlDataReader read3 = db.ExecuteQuery(query3);
                    read3.Read();
                    int maxqty = int.Parse(read3["capacity"].ToString()!);
                    read3.Close();
                    if (maxqty >= endqty+qty)
                    {
                    string update_quantity = $"UPDATE `items` SET `quantity`='{(endqty+qty)}' WHERE id={id}";
                    MySqlCommand update = new MySqlCommand(update_quantity,db.GetConnection());
                        update.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("The location can't hold that many items");
                    }
                }
                else
                {
                    read.Close();
                    string query2 = $"SELECT SUM(items.quantity) as endqty FROM `items`Inner Join loctem on loctem.itemid=items.id INNER Join locations On loctem.locationid=locations.id WHERE locations.id={location} GROUP BY loctem.locationid;";
                    MySqlDataReader read2 = db.ExecuteQuery(query2);
                    read2.Read();
                    int endqty = int.Parse(read2["endqty"].ToString()!);
                    read2.Close();
                    string query3 = $"Select * from `locations` WHERE id = {location}";
                    MySqlDataReader read3 = db.ExecuteQuery(query3);
                    read3.Read();
                    int maxqty = int.Parse(read3["capacity"].ToString()!);
                    read3.Close();
                    if (maxqty >= endqty + qty)
                    {
                        string newitem = "INSERT INTO `items`( `categoryid`, `name`, `description`, `brand`, `quantity`) VALUES (@categoryid,@name,@desc,@brand,@qty)";
                        MySqlCommand newitemcmd = new MySqlCommand(newitem,db.GetConnection());
                        newitemcmd.Parameters.AddWithValue("@categoryid", category);
                        newitemcmd.Parameters.AddWithValue("@name", name);
                        newitemcmd.Parameters.AddWithValue("@desc", desc);
                        newitemcmd.Parameters.AddWithValue("@brand", brand);
                        newitemcmd.Parameters.AddWithValue("@qty", qty);
                        newitemcmd.ExecuteNonQuery();
                        string item = $"SELECT * FROM `items` WHERE name=\"{name}\" and brand=\"{brand}\"";
                        MySqlDataReader read4 = db.ExecuteQuery(item);
                        read4.Read();
                        string id = read4["id"].ToString()!;
                        read4.Close();
                        string newloctem = $"INSERT INTO `loctem`(`itemid`, `locationid`) VALUES ({id},{location})";
                        MySqlCommand locteminsert = new MySqlCommand(newloctem,db.GetConnection());
                        locteminsert.ExecuteNonQuery() ;

                    }
                    else
                    {
                        MessageBox.Show("The location can't hold that many items");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Idk what happend: " + ex);
            }
        }
    }
}
