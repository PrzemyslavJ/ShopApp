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
    /// Interaction logic for Analysis.xaml
    /// </summary>
    public partial class Analysis : Window
    {
        public Analysis()
        {
            InitializeComponent();
        }
        ShopAppBaseContext context = new ShopAppBaseContext();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FirstDate.DisplayDateEnd = DateTime.Today;
            SecondDate.DisplayDateEnd = DateTime.Today;
            SelectAllAnalysis.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void SelectAllAnalysis_Click(object sender, RoutedEventArgs e)
        {
            Header.Content = "Analiza całościowa";
            var AnalysisPList = from a in context.SalesRecords
                                join b in context.MenuOfProducts on a.IdProduct equals b.Id
                                group a by new { b.Id, b.Name, b.Type } into c
                                select new
                                {
                                    Id = c.Key.Id,
                                    Name = c.Key.Name,
                                    Type = c.Key.Type,
                                    CountQuantity = c.Sum(d => d.Quantity),
                                    CountProfit = c.Sum(f => f.Profit)
                                };

            var AnalysisTList = from a in context.SalesRecords
                                join b in context.MenuOfProducts on a.IdProduct equals b.Id
                                group a by b.Type into c
                                select new
                                {
                                    Type = c.Key,
                                    CountQuantity = c.Sum(d => d.Quantity),
                                    CountProfit = c.Sum(f => f.Profit)
                                };

            var AnalysisEList = from a in context.SalesRecords
                                join b in context.Employees on a.IdEmployee equals b.Id
                                group a by new { b.Id, b.Surname } into c
                                select new
                                {
                                    Id = c.Key.Id,
                                    Surname = c.Key.Surname,
                                    CountQuantity = c.Sum(d => d.Quantity),
                                    CountProfit = c.Sum(f => f.Profit)
                                };

            AnalysisOfProducts.ItemsSource = AnalysisPList.ToList();
            AnalysisOfTypes.ItemsSource = AnalysisTList.ToList();
            AnalysisOfEmployees.ItemsSource = AnalysisEList.ToList();
        }

        private void SelectTimeAnalysis_Click(object sender, RoutedEventArgs e)
        {
            DateTime? SecondDateSet = SecondDate.SelectedDate;
            DateTime SecondCorrectDate = SecondDateSet ?? DateTime.Now;
            SecondCorrectDate = SecondCorrectDate.AddDays(1);
            
            if (FirstDate.SelectedDate != null && SecondDate.SelectedDate != null)
            {
                Header.Content = "Analiza okresowa od: " + FirstDate.SelectedDate + " do: " + SecondDate.SelectedDate;
                var AnalysisPListDate = from a in context.SalesRecords
                                        join b in context.MenuOfProducts on a.IdProduct equals b.Id
                                        where a.DateOfSale >= FirstDate.SelectedDate && a.DateOfSale <= SecondCorrectDate
                                        group a by new { b.Id, b.Name, b.Type } into c
                                        select new
                                        {
                                            Id = c.Key.Id,
                                            Name = c.Key.Name,
                                            Type = c.Key.Type,
                                            CountQuantity = c.Sum(d => d.Quantity),
                                            CountProfit = c.Sum(f => f.Profit)
                                        };
                var AnalysisTListDate = from a in context.SalesRecords
                                        join b in context.MenuOfProducts on a.IdProduct equals b.Id
                                        where a.DateOfSale >= FirstDate.SelectedDate && a.DateOfSale <= SecondCorrectDate
                                        group a by b.Type into c
                                        select new
                                        {
                                            Type = c.Key,
                                            CountQuantity = c.Sum(d => d.Quantity),
                                            CountProfit = c.Sum(f => f.Profit)
                                        };
                var AnalysisEListDate = from a in context.SalesRecords
                                        join b in context.Employees on a.IdEmployee equals b.Id
                                        where a.DateOfSale >= FirstDate.SelectedDate && a.DateOfSale <= SecondCorrectDate
                                        group a by new { b.Id, b.Surname } into c
                                        select new
                                        {
                                            Id = c.Key.Id,
                                            Surname = c.Key.Surname,
                                            CountQuantity = c.Sum(d => d.Quantity),
                                            CountProfit = c.Sum(f => f.Profit)
                                        };
                AnalysisOfProducts.ItemsSource = AnalysisPListDate.ToList();
                AnalysisOfTypes.ItemsSource = AnalysisTListDate.ToList();
                AnalysisOfEmployees.ItemsSource = AnalysisEListDate.ToList();
            }
            else
                MessageBox.Show("Zdefiniuj przedział czasowy !", "Komunikat");
        }
    }
}