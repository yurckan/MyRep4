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
    public partial class AddProduct : Window
    {
        private Product contextProduct = new Product();
        public AddProduct(Product currentProduct)
        {
            InitializeComponent();
            AddCmb();
            if (currentProduct != null)
            {
                SaveBtn.Content = "Сохранить";
                currentProduct = currentProduct;
                Title = "Редактирование продукта";
            }

            DataContext = contextProduct;
        }

        private void AddCmb()//Заполнение выподающих списков
        {
            var edIzmer = App.Context.EdnIzmer.ToList();
            edIzmer.Insert(0, new EdnIzmer
            {
                Title = "Единицы измерения"
            });
            EdIzmerCmb.ItemsSource = edIzmer;

            var manufacturer = App.Context.Manufacturer.ToList();
            manufacturer.Insert(0, new Manufacturer
            {
                Title = "Все производители"
            });
            ManufacturerCmb.ItemsSource = manufacturer;

            var category = App.Context.CategoryProduct.ToList();
            category.Insert(0, new CategoryProduct
            {
                Title = "Все категории"
            });
            CategoryCmb.ItemsSource = category;

            var suplier = App.Context.Suplier.ToList();
            suplier.Insert(0, new Suplier
            {
                Title = "Все постащики"
            });
            Spleint.ItemsSource = suplier;

            EdIzmerCmb.SelectedIndex = 0;
            ManufacturerCmb.SelectedIndex = 0;
            Spleint.SelectedIndex = 0;
            CategoryCmb.SelectedIndex = 0;
        }

        private bool Proverca()
        {
            string message = "";

            if (ArticuleTB.Text == "")
                message += "Необходимо указать артикул\n";
            if (TitleTB.Text == "")
                message += "Необходимо указать название продукта\n";

            if (EdIzmerCmb.SelectedIndex < 1)
                message += "Необходимо выбрать единицу изверения\n";

            if (CostTB.Text == "")
                message += "Неоходимо указать стоимость товара\n";

            if (ManufacturerCmb.SelectedIndex < 1)
                message += "Неоходимо указать производителя\n";
            if (Spleint.SelectedIndex < 1)
                message += "Неоходимо указать поставщика\n";
            if (CategoryCmb.SelectedIndex < 1)
                message += "Неоходимо указать категорию товара\n";

            if (CountTB.Text == "")
                message += "Необходимо указать количество на складе\n";
            if (InformationTB.Text == "")
                message += "Необходимо заполнить описание товара\n";



            if (message == "")
                return true;
            else
            {
                MessageBox.Show(message, "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Proverca())
            {
                if (contextProduct.ID == 0)
                    App.Context.Product.Add(contextProduct);

                try
                {
                    App.Context.SaveChanges();
                    MessageBox.Show("Информация сохранена", "Иформация", MessageBoxButton.OK, MessageBoxImage.Information);

                    GlavMenu glavMenu = new GlavMenu(null);
                    glavMenu.Show();
                    Close();
                }
                catch
                {
                    MessageBox.Show("Некорректное заполнение данных");
                }
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить добавление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                GlavMenu glavMenu = new GlavMenu(null);
                glavMenu.Show();
                Close();
            }
        }
    }
}
