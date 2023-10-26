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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            Login log = new Login();
            bool userExist = log.Loginin(username, db);
            if(userExist)
            {
                bool isEmployee = log.IsEmployee(username,db);
                BasicUserInterFace login = new BasicUserInterFace();
                login.Show();
                if (!isEmployee)
                {
                    login.InsertButton.Visibility = Visibility.Collapsed;
                    login.UpdateButton.Visibility = Visibility.Collapsed;
                    
                }
                this.Close();
            }
            else
            {
                LoginFailed.Content = "Login failed";
            }
         
            

        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Get the username entered in the UserRegister TextBox
            string username = UserRegister.Text;
            // Check if the IsEmployeeCheckbox is checked and set isEmployed accordingly
            bool RegisEmployee = IsEmployeeCheckbox.IsChecked == true;
            // Create an instance of the Register class with a database connection
            Register userRegister = new Register(new DataBaseConnect());
            // Call the RegisterUser method to attempt user registration
            bool registrationSuccess = userRegister.RegisterUser(username, RegisEmployee);

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
