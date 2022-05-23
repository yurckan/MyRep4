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

namespace WPF1
{
    public partial class GlavMenu : Window
    {
        public GlavMenu(List<User> user)
        {
            InitializeComponent();
            if(user != null)
                if(user.Select(x=>x.RoleID).First() == 1)
                {
                    AddBtn.Visibility = Visibility.Visible;
                }
            AddCmb();
        }

        private void AddCmb()
        {
            SortCmb.Items.Add("Цена");
            SortCmb.Items.Add("По возростанию");
            SortCmb.Items.Add("По убыванию");
            SortCmb.SelectedIndex = 0;

            var manufactyrer = App.Context.Manufacturer.ToList();
            manufactyrer.Insert(0, new Manufacturer
            {
                Title = "Все производители"
            });
            ManufactyrerCmb.ItemsSource = manufactyrer;
            ManufactyrerCmb.SelectedIndex = 0;
        }

        private void ContextLView()
        {
            var contextProduct = App.Context.Product.ToList();

            contextProduct = contextProduct.Where(c => c.Information.ToLower().Contains(SearchTB.Text.ToLower())).ToList();

            if (ManufactyrerCmb.SelectedIndex > 0)
                contextProduct = contextProduct.Where(c=>c.ManufacturerID == ManufactyrerCmb.SelectedIndex).ToList();

            if (SortCmb.SelectedIndex == 1)
                contextProduct = contextProduct.OrderBy(c => c.Cost).ToList();
            if (SortCmb.SelectedIndex == 2)
                contextProduct = contextProduct.OrderByDescending(c => c.Cost).ToList();

            LView.ItemsSource = contextProduct;
        }

        private void ManufactyrerCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContextLView();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContextLView();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct(null);
            addProduct.Show();
            Close();
        }
    }
}
