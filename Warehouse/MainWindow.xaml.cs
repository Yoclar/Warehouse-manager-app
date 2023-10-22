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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Warehouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public DataBaseConnect db = new DataBaseConnect();
        public MainWindow()
                
        {
            InitializeComponent();
            db.OpenConnection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            BasicUserInterFace login = new BasicUserInterFace();
            if (log.Loginin(Username.Text, db))
            {
                login.Show();
                this.Close();
            }

        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Get the username entered in the UserRegister TextBox
            string username = UserRegister.Text;
            // Check if the IsEmployeeCheckbox is checked and set isEmployed accordingly
            bool isEmployed = IsEmployeeCheckbox.IsChecked ?? false;
            // Create an instance of the Register class with a database connection
            Register userRegister = new Register(new DataBaseConnect());
            // Call the RegisterUser method to attempt user registration
            bool registrationSuccess = userRegister.RegisterUser(username, isEmployed);

            if (registrationSuccess)
            {
                MessageBox.Show("Registration successful!");
                this.Close();
                
                BasicUserInterFace newLogin = new BasicUserInterFace();
                newLogin.Show();
               
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again.");
            }





        }

    
    }
}
