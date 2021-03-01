using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Olymp2021.Views.Pages
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

        private void btnLoginAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (txbCodeAdmin.Text == "0000")
            {
                Properties.Settings.Default.isAdmin = "Visible";
                NavigationService.Navigate(new ServiceViewPage());
            }
            else
                MessageBox.Show("Ошибка при авторизации!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnFreeLogin_Click(object sender, RoutedEventArgs e)
        {
            // вход обычного функционала
            Properties.Settings.Default.isAdmin = "Collapsed";
            NavigationService.Navigate(new ServiceViewPage());
        }

        private void txbCodeAdmin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
