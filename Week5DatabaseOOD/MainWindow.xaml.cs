﻿using System;
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
        private void btnQueryEx4_Click(object sender,RoutedEventArgs e)
        {
            var query = from p in db.Products
                        where p.Category.CategoryName.Equals("Beverages")
                        orderby p.ProductID descending
                        select new
                        {
                            p.ProductID,
                            p.ProductName,
                            p.Category.CategoryName,
                            p.UnitPrice
                        };
            dgCustomersEX4.ItemsSource = query.ToList();
        }
        private void btnQueryEx5_Click(object sender,RoutedEventArgs e)
        {
            Product p = new Product()
            {
                ProductName = "Kickapoo Jungle Joy Juice",
                UnitPrice = 12.49m,
                CategoryID = 1
            };
            db.Products.Add(p);
            db.SaveChanges();
            ShowProducts(dgCustomersEX5);
        }
        private void ShowProducts(DataGrid currentGrid)
        {
            var query = from p in db.Products
                        where p.Category.CategoryName.Equals("Beverages")
                        orderby p.ProductID descending
                        select new
                        {
                            p.ProductID,
                            p.ProductName,
                            p.Category.CategoryName,
                            p.UnitPrice
                        };
            currentGrid.ItemsSource = query.ToList();
        }
        private void btnQueryEx6_Click(object sender,RoutedEventArgs e)
        {
            Product p1 = (db.Products
                .Where(p => p.ProductName.StartsWith("kick"))
                .Select(p => p)).First();
            
            p1.UnitPrice = 100m;
            
            db.SaveChanges();
            ShowProducts(dgCustomersEX6);
        }
        private void btnQueryEx7_Click(object sender, RoutedEventArgs e)
        {
            var products = from p in db.Products
                           where p.ProductName.StartsWith("kick")
                           select p;
            foreach (var item in products)
            {
                item.UnitPrice = 100m;
            }
            db.SaveChanges();
            ShowProducts(dgCustomersEX7);
        }
        private void btnQueryEx8_Click(object sender, RoutedEventArgs e)
        {
            var products = from p in db.Products
                           where p.ProductName.StartsWith("kick")
                           select p;
            db.Products.RemoveRange(products);
            db.SaveChanges();
            ShowProducts(dgCustomersEX8);
        }
        private void btnQueryEx9_Click(object sender,RoutedEventArgs e)
        {
            var query = db.Customers_By_City("London");
            dgCustomersEX9.ItemsSource = query.ToList();
        }
    }
}
