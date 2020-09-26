using C_Sharp_Forum1.Context;
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

namespace C_Sharp_Forum1.Views.Pages.Home
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        // Кнопка Авторизации
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // => - это Лямда Выражение
                var currentUser = ContextConnect.db.User.FirstOrDefault(item => item.Username == txbUsername.Text &&
                item.Password == psbPassword.Password);
                // Проверяем, есть ли в Базе Такой пользователь
                if (currentUser == null)
                    MessageBox.Show("Вы ввели неверные данные авторизации! Пожалуйста, повторите попытку...", "Не верно!", 
                        MessageBoxButton.OK, 
                        MessageBoxImage.Exclamation);
                // Если есть, то должны определить, Администратор это или Пользователь
                else
                {
                    // Для этого идеально подходит Switch Case
                    switch (currentUser.IDRole)
                    {
                        case "A":
                            MessageBox.Show("Привет Администратор " + currentUser.Username);
                            break;
                        case "U":
                            MessageBox.Show("Привет Пользователь " + currentUser.Username);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Отлавливаем исключения, чтобы программа аварийно не завершилась
                MessageBox.Show(ex.Message, ex.Source + "выдал исключение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Кнопка Отмены
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            // Выключить приложение
            Application.Current.Shutdown();
        }
    }
}
