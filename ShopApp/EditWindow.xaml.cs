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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
        }

        ShopAppBaseContext context = new ShopAppBaseContext();
        MenuOfProduct menu = new MenuOfProduct();

        private void IdT_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeT_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NameT_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PriceT_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                menu.Id = Convert.ToInt32(IdT.Text);
                menu.Type = Convert.ToString(TypeT.Text);
                menu.Name = Convert.ToString(NameT.Text);
                menu.PricePLN = Convert.ToDouble(PriceT.Text);
                context.MenuOfProducts.Add(menu);
                context.SaveChanges();
                MessageBox.Show("Poprawny zapis !", "Komunikat");
                IdT.Text = "";
                TypeT.Text = "";
                NameT.Text = "";
                PriceT.Text = "";

            }
            catch
            {
                MessageBox.Show("Niepoprawny dane wprowadzone, wprowadź jeszcze raz !", "Błąd");
            }

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SingleRecord = context.MenuOfProducts.Where(x => x.Id == Convert.ToInt32(IdT.Text)).FirstOrDefault();
                context.MenuOfProducts.Remove(SingleRecord);
                context.SaveChanges();
                MessageBox.Show("Poprawne usunięcie !", "Komunikat");
                IdT.Text = "";
                TypeT.Text = "";
                NameT.Text = "";
                PriceT.Text = "";
            }
            catch
            {
                MessageBox.Show("Niepoprawny numer wprowadzony, wprowadź jeszcze raz !", "Błąd");
            }
        }
    }
}