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
            if (log.Loginin(Username.Text, db))
            {
                BasicUserInterFace login = new BasicUserInterFace();
                login.Show();
                this.Close();
            }

        }
    }
}
