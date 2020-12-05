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
using wpf_1.Context;

namespace wpf_1.Views.Pages {
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page {
        public Authorization() {
            InitializeComponent();
        }
        // Выключаем программу
        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
        // Происходит авторизация
        private void btnLogIn_Click(object sender, RoutedEventArgs e) {
            var currentUser = dbContext.db.SignIns.FirstOrDefault(item => item.Username == txbUsername.Text && item.Password == psbPassword.Password);
            if(currentUser != null) {
                switch (currentUser.IDRole) {
                    case "A":
                        MessageBox.Show("Привет Администратор!");
                        break;
                    case "U":
                        MessageBox.Show("Привет Пользователь!");
                        break;
                }
            }
            else {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка твою мать!", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
