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
    /// Interaction logic for Records.xaml
    /// </summary>
    public partial class Records : Window
    {
        ShopAppBaseContext context = new ShopAppBaseContext();

        public Records()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var SaleList = from a in context.SalesRecords
                           join b in context.MenuOfProducts on a.IdProduct equals b.Id
                           join c in context.Employees on a.IdEmployee equals c.Id
                           select new
                           {
                               a.Id,
                               b.Type,
                               b.Name,
                               b.PricePLN,
                               a.DateOfSale,
                               a.Quantity,
                               a.Profit,
                               c.Surname
                           };
            SalesTable.ItemsSource = SaleList.ToList();


        }

    }
}
