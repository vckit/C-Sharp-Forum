using System.Linq;
using System.Windows;
using System.Windows.Controls;
using wpf_1.Context;
using wpf_1.Views.Pages.Admin;
using wpf_1.Views.Pages.User;

namespace wpf_1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization( )
        {
            InitializeComponent();
        }
        // Выключаем программу
        private void btnCancel_Click( object sender, RoutedEventArgs e )
        {
            Application.Current.Shutdown();
        }
        // Происходит авторизация
        private void btnLogIn_Click( object sender, RoutedEventArgs e )
        {
            var currentUser = dbContext.db.SignIns.FirstOrDefault( item => item.Username == txbUsername.Text && item.Password == psbPassword.Password );
            if (currentUser != null)
            {
                switch (currentUser.IDRole)
                {
                    case "A":
                        MessageBox.Show( "Привет Администратор!" );
                        NavigationService.Navigate( new AdminViewData() );
                        break;
                    case "U":
                        MessageBox.Show( "Привет Пользователь!" );
                        NavigationService.Navigate( new UserViewData() );
                        break;
                }
            }
            else
            {
                MessageBox.Show( "Неверный логин или пароль!", "Ошибка твою мать!",
                    MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }
    }
}
