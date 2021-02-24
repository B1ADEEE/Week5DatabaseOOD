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

namespace Week5DatabaseOOD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities db = new NORTHWNDEntities();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnQuery_Click(object sender,RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        select c.CompanyName;
            lbxCustimersEX1.ItemsSource = query.ToList();
        }
        private void btnQueryEx2_Click(object sender,RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        select c;
            dgCustomersEX2.ItemsSource = query.ToList();
        }
        private void btnQueryEx3_Click(object sender,RoutedEventArgs e)
        {
            var query = from o in db.Orders
                        where o.Customer.City.Equals("London")
                        || o.Customer.City.Equals("Paris")
                        || o.Customer.Country.Equals("USA")
                        orderby o.Customer.ContactName
                        select new
                        {
                            CustomerName = o.Customer.CompanyName,
                            City = o.Customer.City,
                            Address = o.ShipAddress
                        };
            dgCustomersEX3.ItemsSource = query.ToList().Distinct();
        }
    }
}
