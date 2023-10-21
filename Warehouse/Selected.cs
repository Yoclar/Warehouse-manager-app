using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using System.Windows;
using System.Reflection.PortableExecutable;
using ZstdSharp.Unsafe;

namespace Warehouse
{
    internal class Selected
    {
        
        string[] ret = new string[2];//this will separate the name and the quantity of an item
        public Selected()
        {
            
        }
        public string[] List(string where, DataBaseConnect db)
        {
            //SQL select method to find th id of the chosen category
            string locationquery = $"SELECT id FROM `locations` WHERE name LIKE \"{where}\"";
                //giving it to the database
            MySqlDataReader reader = db.ExecuteQuery(locationquery);
            //these will contain the informations whic will go into ret
            string items = string.Empty,itemcount = string.Empty;
             
            if (reader.Read())//Runs the select method and gives the program the found objects
            //If the databse doesn't have the location name than it reads all the items 
            {

            try
            {
            
            string itemsquery= "SELECT * FROM `items`  INNER JOIN loctem ON items.id = itemid WHERE loctem.locationid = " + reader["id"].ToString();
                    //Sadly you always have to close it befor giving it a new select method or anything else
                    reader.Close();
             reader = db.ExecuteQuery(itemsquery);
            while(reader.Read())
            {
                string columnValue = "Name: "+ reader["name"].ToString()!; //gets the name of the current row
                items += columnValue + "\n "; //ads a line breake and ads the next name
                        itemcount += "Count: " + reader["quantity"].ToString()! + "\n"; //same but in 1 row and with the quantity
            }
            reader.Close();
                    //filling the ret variable
                    ret[0] =items;
                    ret[1] = itemcount;
            return ret;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                reader.Close() ;
                return ret;
            }
            }
            else
            {
                //Sadly you always have to close it befor giving it a new select method or anything else
                //(even if the name is different the databse can't handle two select method at once)
                reader.Close ();
                string allitemquery = "SELECT * FROM `items`";
                MySqlDataReader allitemreader = db.ExecuteQuery(allitemquery);
                try
                {
                

                
                //until the table has 0 informations left 
                while (allitemreader.Read())
                {
                    //turning the table's column into string so we can write out it's content
                    string columnValue = "Name: " + allitemreader["name"].ToString()!;
                    
                    items += columnValue + "\n";
                    itemcount += "Count: " + allitemreader["quantity"].ToString()! + "\n";
                    }
                    allitemreader.Close();
                    ret[0] = items;
                    ret[1] = itemcount;
                    return ret;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem: "+ex);
                    allitemreader.Close();
                    return ret;
                }
            }
        } 
    }
}
