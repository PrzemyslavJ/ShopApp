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
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        public EditEmployeeWindow()
        {
            InitializeComponent();
        }
        ShopAppBaseContext context = new ShopAppBaseContext();
        Employee emps = new Employee();

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                emps.Id = Convert.ToInt32(IdT.Text);
                emps.Surname = SurnameT.Text;
                emps.ContractType = ContractT.Text;
                emps.StartOfWork = StartT.Text;
                emps.Password = PasswordT.Text;
                context.Employees.Add(emps);
                context.SaveChanges();
                MessageBox.Show("Zapis się powiódł !", "Komunikat");
                IdT.Text = "";
                SurnameT.Text = "";
                ContractT.Text = "";
                StartT.Text = "";
                PasswordT.Text = "";
            }
            catch
            {
                MessageBox.Show("Wprowadzono błędne dane, wprowadź poprawne !", "Błąd");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SingleEmp = context.Employees.Where(x => x.Id == Convert.ToInt32(IdT.Text)).FirstOrDefault();
                context.Employees.Remove(SingleEmp);
                context.SaveChanges();
                MessageBox.Show("Usunięto wybranego pracownika !", "Komunikat");
                IdT.Text = "";
            }
            catch
            {
                MessageBox.Show("Wprowadzono niepawidłowy numer ! Wprowadź poprawny", "Błąd");
            }
        }
    }
}
