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

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }
        ShopAppBaseContext Shop = new ShopAppBaseContext();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Table1.ItemsSource = Shop.MenuOfProducts.ToList();

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordWindow Edit = new PasswordWindow("Administrator");
            Edit.ShowDialog();
        }
    }

}