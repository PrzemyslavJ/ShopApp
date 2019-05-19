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
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public PasswordWindow(string Option)
        {
            InitializeComponent();
            this.Option = Option;
        }
        string Option = "";
        public int IdE { get; set; }
        public string NEmp { get; set; } = "";
        string AdministratorPassword = "123321";


        ShopAppBaseContext context = new ShopAppBaseContext();

        private void StandardComPass() { MessageBox.Show("Niepoprawne hasło, wprowadź jeszcze raz !", "Błąd"); }

        
        private void PasswordConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (Option == "Employee")
            {
                bool Ct = false;

                foreach (var x in context.Employees)
                {
                    if (x.Password == PasswordText.Text)
                    {
                        IdE = x.Id;
                        NEmp = x.Surname;
                        Ct = true;
                        MessageBox.Show("Ustawiono pracownika: " + NEmp, "Komunikat");
                        break;
                    }
                }

                if (Ct == false)
                {
                    StandardComPass();
                }

            }
            else if (Option == "Administrator")
            {
                if (AdministratorPassword == PasswordText.Text)
                {
                    EditWindow editW = new EditWindow();
                    editW.ShowDialog();
                }
                else
                    StandardComPass();
            }
            else if (Option == "AdministratorEmployee")
            {
                if (AdministratorPassword == PasswordText.Text)
                {
                    EmployeeWindow EmpWin = new EmployeeWindow();
                    EmpWin.ShowDialog();
                }
                else
                    StandardComPass();
            }

        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}