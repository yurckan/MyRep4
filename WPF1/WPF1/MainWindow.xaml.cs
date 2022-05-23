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

namespace WPF1
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

            //Проверка пустых полей
            if (LoginTB.Text == "")
                message += "Поле ЛОГИН не должен быть пустым.\n";
            if (PasswordTB.Password == "")
                message += "Поле ПАРОЛЬ не должен быть пустым.\n";

            if (message == "")
                return true;
            else
            {
                MessageBox.Show(message, "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }

        private void AutoBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Proverca())
            {
                //Проверка на совпадение логина и пароля в БД
                List<User> userCoun = App.Context.User.Where(c => c.Login == LoginTB.Text && c.Password == PasswordTB.Password).ToList();
                if(userCoun.Count() == 1)
                {
                    GlavMenu glavMenu = new GlavMenu(userCoun);
                    glavMenu.Show();
                    Close();
                }
                else
                    MessageBox.Show("Неверный логин или пароль. \n Вы можете войти без авторизации.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void NotLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            //Вход под гостем
            GlavMenu glavMenu = new GlavMenu(null);
            glavMenu.Show();
            Close();
        }
    }
}
