using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Olimpiad.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginFree_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.isAdmin = "Collapsed";
            NavigationService.Navigate(new ServiceViewPage());
        }

        private void LoginFromAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (txbAdminCode.Text == "0000")
            {
                Properties.Settings.Default.isAdmin = "Visible";
                NavigationService.Navigate(new ServiceViewPage());
            }
            else
                MessageBox.Show("Вы ввели неверно код администратора, пожалуйста попробуйте заново или войдите как гость.", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
