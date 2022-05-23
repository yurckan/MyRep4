using System;
using System.Collections.Generic;
using System.IO;
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

namespace WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private bool Proverca()
        {
            string message = "";

            if (LoginTB.Text == "")
                message += "Поле ЛОГИН не должен быть пустым!\n";
            if (PasswordTB.Password == "")
                message += "Поле ПАРОЛЬ не должен быть пустым!\n";


            if (message == "")
                return true;
            else
            {
                MessageBox.Show(message, "Внимание",MessageBoxButton.OK,MessageBoxImage.Information);
                return false;
            }
        }


        private void AutoBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Proverca())
            {
                List<User> userCount = App.Context.User.Where(c => c.Login == LoginTB.Text &&
                c.Password == PasswordTB.Password).ToList();
                if(userCount.Count() == 1)
                {
                    GlavMenu glavMenu = new GlavMenu(userCount);
                    glavMenu.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!\nВы можете войти без авторизации", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlavMenu glavMenu = new GlavMenu(null);
            glavMenu.Show();
            Close();
        }
    }
}
