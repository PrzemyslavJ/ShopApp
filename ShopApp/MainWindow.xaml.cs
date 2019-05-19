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

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        double sum1;
        char sign = ' ';
        double Amount = 0;
        int IdP;
        string NameProduct;
        string TypeProduct;
        double Price = 0;
        int Quantity = 0;
        double Profit = 0;
        int IdEmp = 0;
        double Rest = 0;
        double ForPay = 0;

        ShopAppBaseContext Shop = new ShopAppBaseContext();
        List<ShoppingList> FirstList = new List<ShoppingList>();

        private void StandardCommunicateError() { MessageBox.Show("Niepoprawne dane, wprowadź jeszcze raz !", "Błąd"); }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var Flist = from a in Shop.MenuOfProducts select a.Type;
            var UniqueList = new HashSet<string>(Flist);
            type.ItemsSource = UniqueList.ToList();

            FirstTable.ItemsSource = FirstList;

        }

        private void type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            TypeProduct = Convert.ToString(type.SelectedValue);
            var Nlist = Shop.MenuOfProducts.Where(a => a.Type == TypeProduct);
            NameS.ItemsSource = Nlist.ToList();

        }

        private void NameS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NameProduct = Convert.ToString(NameS.SelectedValue);
            var Plist = from a in Shop.MenuOfProducts
                        where a.Name == NameProduct
                        select new { a.PricePLN, a.Id };

            foreach (var x in Plist)
            {
                Price = x.PricePLN;
                IdP = x.Id;
            }

            price.Text = Convert.ToString(Price);
        }

        private void quantity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Quantity = quantity.SelectedIndex + 1;
        }

        private void SaleButtonFirst_Click(object sender, RoutedEventArgs e)
        {
            Profit = Quantity * Price;

            if (TypeProduct != null && NameProduct != null)
            {
                FirstList.Add(new ShoppingList(IdP, TypeProduct, NameProduct, Price, Quantity, Profit));
                FirstTable.Items.Refresh();
            }
            else
            {
                StandardCommunicateError();
            }

            ForPay = FirstList.Sum(x => x.Profit);
            For_Pay.Text = ForPay.ToString();
            SetRest();

        }

        private void SaleFinalButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy potwierdzasz sprzedaż ? ", "Potwierdzenie sprzedaży", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if (IdEmp != 0)
                {
                    if (FirstList.Count() > 0)
                    {
                        if (Rest > 0)
                        {
                            try
                            {
                                int locCount1 = Shop.SalesRecords.Count();
                                foreach (var s in FirstList)
                                {
                                    SalesRecord record = new SalesRecord();
                                    if (Shop.SalesRecords.Count() == 0)
                                        record.Id = 1;
                                    else
                                    {
                                        record.Id = Shop.SalesRecords.Count() + 1;
                                    }
                                    record.IdProduct = s.Id;
                                    record.DateOfSale = DateTime.Now;
                                    record.Quantity = s.Quantity;
                                    record.Profit = s.Profit;
                                    record.IdEmployee = IdEmp;
                                    Shop.SalesRecords.Add(record);
                                    Shop.SaveChanges();
                                }
                                int locCount2 = Shop.SalesRecords.Count();
                                if (locCount2 > locCount1)
                                {
                                    MessageBox.Show("Dokonano pomyślnej rejestracji zakupów ! Reszta:  " + Rest+ " zł", "Komunikat");
                                    FirstList.Clear();
                                    FirstTable.Items.Refresh();
                                    ClearFieldCash();
                                }
                            }
                            catch
                            {
                                StandardCommunicateError();
                            }   
                        } 
                    else
                        MessageBox.Show("Brakuje " + Rest + " zł do zapłaty !", "Błąd");
                }
                else
                    MessageBox.Show("List produktów jest pusta !", "Błąd");
                }
                else
                    MessageBox.Show("Pracownik jest niezalogowany, Zaloguj się ! ", "Błąd");
            }
        
        }
    
        private void StandardAppend(int i)
        {
            if (amount.Text == "0")
            {
                
                amount.Text = i.ToString();
            }
            else
            {
                amount.AppendText(i.ToString());
            }
        }


      private void B1_Click(object sender, RoutedEventArgs e)
      {
            StandardAppend(1);
      }

      private void B2_Click(object sender, RoutedEventArgs e)
      {
            StandardAppend(2);
      }

      private void B3_Click(object sender, RoutedEventArgs e)
      {
            StandardAppend(3);
        }

      private void B4_Click(object sender, RoutedEventArgs e)
      {
            StandardAppend(4);
        }

      private void B5_Click(object sender, RoutedEventArgs e)
      {
            StandardAppend(5);
        }

      private void B6_Click(object sender, RoutedEventArgs e)
      {
            StandardAppend(6);
        }

      private void B7_Click(object sender, RoutedEventArgs e)
      {
            StandardAppend(7);
        }

      private void B8_Click(object sender, RoutedEventArgs e)
      {
            StandardAppend(8);
        }

      private void B9_Click(object sender, RoutedEventArgs e)
      {
            StandardAppend(9); 
      }

      private void B0_Click(object sender, RoutedEventArgs e)
      {
          amount.AppendText("0");
      }

      private void Bp_Click(object sender, RoutedEventArgs e)
      {
          amount.AppendText(",");
      }

      private void SetRest()
        {
            Rest = Amount - ForPay;
            if (Rest >= 0 && ForPay > 0)
                rest.Background = Brushes.Green;
            else if (Rest < 0 && ForPay > 0)
                rest.Background = Brushes.Red;
            rest.Text = Rest.ToString();
        }

      private void ClearFieldCash()
        {
            Amount = 0;
            Rest = 0;
            ForPay = 0;
            amount.Text = Amount.ToString();
            rest.Text = Rest.ToString();
            For_Pay.Text = ForPay.ToString();
            rest.Background = Brushes.White;
        }

      private void amount_TextChanged(object sender, TextChangedEventArgs e)
      {
          try
          {
              Amount = Convert.ToDouble(amount.Text);
          }
          catch
          {
                Amount = 0;
                amount.Text = Amount.ToString();
          }

            SetRest();
          
      }

      private void BackSpace_Click(object sender, RoutedEventArgs e)
      {
          if (amount.Text.Length > 0)
          {
              amount.Text = amount.Text.Substring(0, amount.Text.Length - 1);
          }
      }

      private void Button_Click_2(object sender, RoutedEventArgs e)
      {
          sum1 = Convert.ToDouble(amount.Text);
          sign = '+';
          amount.Text = "";
      }


      private void menuProduktow_click(object sender, RoutedEventArgs e)
      {
          Menu menu = new Menu();
          menu.ShowDialog();
      }

      private void deleteall_Click(object sender, RoutedEventArgs e)
      {
          amount.Text = "";
          sum1 = 0;
      }

      private void roznica_Click(object sender, RoutedEventArgs e)
      {
          sum1 = Convert.ToDouble(amount.Text);
          sign = '-';
          amount.Text = "";
      }

      private void mnozenie_Click(object sender, RoutedEventArgs e)
      {
          sum1 = Convert.ToDouble(amount.Text);
          sign = '*';
          amount.Text = "";
      }

      private void dzielenie_Click(object sender, RoutedEventArgs e)
      {
          sum1 = Convert.ToDouble(amount.Text);
          sign = '/';
          amount.Text = "";
      }

      private void Zestawienie_Click(object sender, RoutedEventArgs e)
      {
         Records records = new Records();
         records.ShowDialog();
      }


      private void Analysis_Click(object sender, RoutedEventArgs e)
      {
         Analysis analysis = new Analysis();
         analysis.ShowDialog();
      }

      private void wynik_Click(object sender, RoutedEventArgs e)
      {
          switch (sign)
          {
              case '+':
                  sum1 = sum1 + Convert.ToDouble(amount.Text);
                  amount.Text = Convert.ToString(sum1);
                  break;
              case '-':
                  sum1 = sum1 - Convert.ToDouble(amount.Text);
                  amount.Text = Convert.ToString(sum1);
                  break;
              case '*':
                  sum1 = sum1 * Convert.ToDouble(amount.Text);
                  amount.Text = Convert.ToString(sum1);
                  break;
              case '/':
                  if (Convert.ToDouble(amount.Text) == 0)
                  {
                      MessageBox.Show("Nie dziel przez 0 !");
                  }
                  else
                  {
                      sum1 = sum1 / (Convert.ToDouble(amount.Text));
                      amount.Text = Convert.ToString(sum1);
                  }
                  break;

          }
            
      }
       
      private void EmployeeButton(object sender, RoutedEventArgs e)
      {
            PasswordWindow Logg = new PasswordWindow("Employee");
            Logg.ShowDialog();
            IdEmp = Logg.IdE;
            LoggEmpSet.Content = "Pracownik: " + Logg.NEmp;


      }

      

      private void RecordDelete_Click(object sender, RoutedEventArgs e)
      {
            if (FirstList.Count() > 0)
            {
                if (FirstList.Count() == 1)
                {
                    FirstList.Clear();
                    FirstTable.Items.Refresh();
                    ClearFieldCash();
                }
                else
                {
                    FirstList.RemoveAt(FirstList.Count() - 1);
                    FirstTable.Items.Refresh();
                }
            }
      }

     

      private void Button_Click_3(object sender, RoutedEventArgs e)
      {
          FirstList.Clear();
          FirstTable.Items.Refresh();
          ClearFieldCash();
      }

        private void EmployeeList_Click(object sender, RoutedEventArgs e)
        {
            PasswordWindow EmpWin = new PasswordWindow("AdministratorEmployee");
            EmpWin.ShowDialog();
        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplikacja została stworzona przez Przemysława Jaworskiego. " +
                "Głównie ma charakter edukacyjny i ćwiczeniowy, jednak mogłaby się spisać jako aplikacja wykorzystawana w sklepach." +
                " Hasło Administratora to: 123321. Wszelkie uwagi i komentarze proszę kierować na adres email: jaworskiprzemyslaw95@gmail.com", "Informacje");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultclose = MessageBox.Show("Czy napewno chcesz zamknąć aplikację ?", "Komunikat", MessageBoxButton.YesNo);

            if (resultclose == MessageBoxResult.Yes)
                Close();
        }

    }
  
    
    }

