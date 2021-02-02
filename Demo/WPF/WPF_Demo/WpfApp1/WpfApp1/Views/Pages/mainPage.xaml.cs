using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApp1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для mainPage.xaml
    /// </summary>
    public partial class mainPage : Page
    {
        public mainPage()
        {
            InitializeComponent();
        }

        private void ButtonAddPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new dataViewPage());
        }
    }
}
