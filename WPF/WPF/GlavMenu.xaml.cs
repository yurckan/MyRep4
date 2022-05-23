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

namespace WPF
{
    public partial class GlavMenu : Window
    {
        public GlavMenu(List<User> user)
        {
            InitializeComponent();
            AddCmb();
            if(user == null)
                ContextMenuList.Visibility = Visibility.Hidden;
            else if (user.Select(x=>x.RoleID).First() != 1)
                ContextMenuList.Visibility = Visibility.Hidden;

            ContextLViewProduct();
        }

        private void AddCmb()
        {
            //Заполнение Cmb - цена
            CostCmb.Items.Add("Сортировка цены");
            CostCmb.Items.Add("По возростанию");
            CostCmb.Items.Add("По убыванию");
            CostCmb.SelectedIndex = 0;

            //Заполнение Cmb - производители
            var manufacturer = App.Context.Manufacturer.ToList();
            manufacturer.Insert(0, new Manufacturer()
            { 
                Title = "Все производители" 
            });
            ManufacturerCmb.ItemsSource = manufacturer;
            ManufacturerCmb.SelectedIndex = 0;
        }

        private void ContextLViewProduct()
        {
            var contextProduct = App.Context.Product.ToList();//Заполнение коллекции
            //Поиск по описанию продукта
                contextProduct = contextProduct.Where(c => c.Title.ToLower().Contains(SearchTB.Text.ToLower())).ToList();

            //Фильтрация по наименованию организации
            if (ManufacturerCmb.SelectedIndex > 0)
                contextProduct = contextProduct.Where(c => c.ManufacturerID == ManufacturerCmb.SelectedIndex).ToList();

            //Сортировка по возростанию и убыванию
            if (CostCmb.SelectedIndex == 1)
                contextProduct = contextProduct.OrderBy(c => c.Cost).ToList();
            if (CostCmb.SelectedIndex == 2)
                contextProduct = contextProduct.OrderByDescending(c => c.Cost).ToList();
            

            LViewProduct.ItemsSource = contextProduct;//Вывод данных в список
        }

        private void ManufacturerCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContextLViewProduct();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContextLViewProduct();
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
