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

namespace Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities2 db = new NORTHWNDEntities2(); // <<-- North No longer recognised? Issue when I downloaded from github

        public MainWindow()
        {
            InitializeComponent();
        }

        //Q1 - Customer Names
        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
     
            var query = from C in db.Customers
                        select C.ContactName;

            lbxCustomerEx1.ItemsSource = query.ToList();
        }
        //Q2 - Customer Objects
        private void btnCustomerObjects_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        select c;
            dgCustomerObjects.ItemsSource = query.ToList();
        }
        //Q3
        private void btnOrderInfo(object sender, RoutedEventArgs e)
        {
            var query = from o in db.Orders
                        where o.Customer.City.Equals("London")
                        || o.Customer.City.Equals("Paris")
                        || o.Customer.City.Equals("USA")
                        orderby o.Customer.CompanyName
                        select new
                        {
                            CustomerName = o.Customer.CompanyName,
                            City = o.Customer.City,
                            Address = o.ShipAddress
                        };
            dgOrderInfo.ItemsSource = query.ToList().Distinct();
        }
        //Q4 Code to display product information
        private void Query4_Click(object sender, RoutedEventArgs e)
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
            dgProductInfo.ItemsSource = query.ToList();
        }
        //Q5 Code to insert a product
        private void BtnQueryEx5_Click(object sender, RoutedEventArgs e)
        {
            Product p = new Product()
            {
                ProductName = "Kickapoo Jungle Joy Juice",
                UnitPrice = 12.49m,
                CategoryID = 1
            };

            db.Products.Add(p);
            db.SaveChanges();

            ShowProducts(dgCustomersEx5);
        }

        //Q6 Code to update product information

        private void BtnQuery6_Click(object sender, RoutedEventArgs e)
        {

            Product p = new Product()
            {
                ProductName = "Kickapoo Jungle Joy Juice",
                UnitPrice = 12.49m,
                CategoryID = 1
            };

            db.Products.Add(p);
            db.SaveChanges();

            ShowProducts(dgCustomersEx5);

            Product p1 = (db.Products
                .Where(q => q.ProductName.StartsWith("Kick"))
                .Select(q => q)).First();

           p1.UnitPrice = 100m;

           db.SaveChanges();
           ShowProducts(dgEx6);

        }
        //Methods
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




    }
}
