using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Warehouse
{
    internal class ButtonOperations
    {
        private DataBaseConnect db;
    

        public ButtonOperations(DataBaseConnect db)
        {
            this.db = db;
         
        }




        public void InsertOperation(bool isEmployed)
        {
            if (isEmployed)
            {
              
            }
            else
            {

            }
        }

        public void UpdateOperation(bool isEmployed)
        {
            if (isEmployed) 
            {

            }
            else
            {

            }
        }
        public void DeleteOperation(bool isEmployed)
        {
            if (isEmployed)
            {

            }
            else
            {

            }
        }
    }
    
}
