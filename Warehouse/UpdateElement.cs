using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Warehouse
{
    internal class UpdateElement
    {
        private DataBaseConnect db;

        public UpdateElement(DataBaseConnect db)
        {
            this.db = db;
        }

        public void UpdateItem(string name, string desc, string brand, int location)
        {
            try
            {
            

            }
            catch (Exception ex)
            {

                MessageBox.Show("Something went wrong" + ex.Message);
            }


        }

    }
}
