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
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
        }

        ShopAppBaseContext context = new ShopAppBaseContext();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TableEmployee.ItemsSource = context.Employees.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditEmployeeWindow EditWindow = new EditEmployeeWindow();
            EditWindow.ShowDialog();
        }
    }
}
