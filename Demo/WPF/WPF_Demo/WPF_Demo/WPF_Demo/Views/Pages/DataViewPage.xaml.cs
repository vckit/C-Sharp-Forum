using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_Demo.Context;

namespace WPF_Demo.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DataViewPage.xaml
    /// </summary>
    public partial class DataViewPage : Page
    {
        public DataViewPage()
        {
            InitializeComponent();
            dbView.ItemsSource = dbContext.db.People.ToList();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDataPage());
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
