using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using System.Data;
using System.Windows;
using System.Reflection.PortableExecutable;
using ZstdSharp.Unsafe;
using System.Windows.Controls;

namespace Warehouse
{
    internal class Selected
    {
        private Button InsertButton;
        private Button UpdateButton;
       

        string[] ret = new string[3];//this will separate the name and the quantity of an item
        public Selected()
        {

        }
        public string[] List(string where, DataBaseConnect db ,int which)
        {
            //SQL select method to find th id of the chosen category
            string locationquery = $"SELECT id FROM `locations` WHERE name LIKE \"{where}\"";
            //giving it to the database
            MySqlDataReader reader = db.ExecuteQuery(locationquery);
            //these will contain the informations whic will go into ret
            string items = string.Empty, itemcount = string.Empty,itembrand=string.Empty;

            if (reader.Read())//Runs the select method and gives the program the found objects
            //If the databse doesn't have the location name than it reads all the items 
            {

                try
                {

                    string itemsquery = $"SELECT * FROM `items`  INNER JOIN loctem ON items.id = itemid WHERE loctem.locationid ={reader["id"].ToString()} LIMIT {which},1";
                    //Sadly you always have to close it befor giving it a new select method or anything else
                    reader.Close();
                    reader = db.ExecuteQuery(itemsquery);
                    reader.Read();
                        items = reader["name"].ToString()!; //gets the name of the current row
                        itemcount = reader["quantity"].ToString()! ; //same but in 1 row and with the quantity
                        itembrand = reader["brand"].ToString()! ;
                    
                    reader.Close();
                    //filling the ret variable
                    ret[0] = items;
                    ret[1] = itemcount;
                    ret[2] = itembrand;
                    return ret;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    reader.Close();
                    return ret;
                }
            }
            else
            {
                //Sadly you always have to close it befor giving it a new select method or anything else
                //(even if the name is different the databse can't handle two select method at once)
                reader.Close();
                string allitemquery = "SELECT * FROM `items` LIMIT "+which+",1";
                MySqlDataReader allitemreader = db.ExecuteQuery(allitemquery);
                allitemreader.Read();
                try
                {
                        //turning the table's column into string so we can write out it's content
                       items =allitemreader["name"].ToString()!;
                        itemcount = allitemreader["quantity"].ToString()!;
                        itembrand = allitemreader["brand"].ToString()!;
                    
                    allitemreader.Close();
                    ret[0] = items;
                    ret[1] = itemcount;
                    ret[2] = itembrand;
                    return ret;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem: " + ex);
                    allitemreader.Close();
                    return ret;
                }
            }
        }
        public int itemcount(string where, DataBaseConnect db)
        {
            //SQL select method to find th id of the chosen category
            string locationquery = $"SELECT id FROM `locations` WHERE name LIKE \"{where}\"";
            //giving it to the database
            MySqlDataReader reader = db.ExecuteQuery(locationquery);
            //these will contain the informations whic will go into ret
            

            if (reader.Read())//Runs the select method and gives the program the found objects
            //If the databse doesn't have the location name than it reads all the items 
            {

                try
                {

                    string itemsquery = "SELECT Count(*) as cnt FROM `items`  INNER JOIN loctem ON items.id = itemid WHERE loctem.locationid = " + reader["id"].ToString();
                    //Sadly you always have to close it befor giving it a new select method or anything else
                    reader.Close();
                    reader = db.ExecuteQuery(itemsquery);
                    reader.Read();
                    int ret= int.Parse(reader["cnt"].ToString()!);   
                    reader.Close();
                    return ret;
                    //filling the ret variable

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    reader.Close();
                    return 0;
                }
            }
            else
            {
                //Sadly you always have to close it befor giving it a new select method or anything else
                //(even if the name is different the databse can't handle two select method at once)
                reader.Close();
                string allitemquery = "SELECT Count(*) as cnt FROM `items`";
                MySqlDataReader allitemreader = db.ExecuteQuery(allitemquery);
                try
                {
                    allitemreader.Read();
                    int ret = int.Parse(allitemreader["cnt"].ToString()!);
                    allitemreader.Close();
                    return ret;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem: " + ex);
                    allitemreader.Close();
                    return 0;
                }
            }
        }

        public void SetButtonVisibility(bool isEmployed)
        {
            if (isEmployed)
            {
                InsertButton.Visibility = Visibility.Visible;
                UpdateButton.Visibility = Visibility.Visible;
                
            }
            else
            {
                InsertButton.Visibility = Visibility.Collapsed;   
                UpdateButton.Visibility = Visibility.Collapsed;

            }
        }
    }
}
