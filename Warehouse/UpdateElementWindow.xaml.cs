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
    /// Interaction logic for UpdateElementWindow.xaml
    /// </summary>
    public partial class UpdateElementWindow : Window
    {
        private DataBaseConnect db;
        public string SelectedName { get; set; }
        public UpdateElementWindow()
        {
            InitializeComponent();
            db = new DataBaseConnect();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the new brand and description from the TextBoxes
                string newBrand = NewBrand.Text;
                string newDescription = NewDescription.Text;

                // Check if the user wants to save the changes
                if (string.IsNullOrEmpty(newBrand) && string.IsNullOrEmpty(newDescription))
                {
                    MessageBox.Show("No changes made. The item will not be updated.");
                    DialogResult = false; // User canceled the update
                }
                else
                {
                    MessageBox.Show("Changes saved successfully.");
                    DialogResult = true; // User accepted the changes
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
    }
}
