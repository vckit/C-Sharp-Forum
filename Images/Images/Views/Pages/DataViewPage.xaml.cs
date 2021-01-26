using Images.Context;
using Images.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Images.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DataViewPage.xaml
    /// </summary>
    public partial class DataViewPage : Page
    {
        public DataViewPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListDataView.ItemsSource = DbContextObject.db.Users.ToList();
        }

        private void AddPageOpenButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DataAddPage());
        }

        private void ListDataView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            User selecteditem = (User)ListDataView.SelectedItem;
            if (selecteditem != null)
                NavigationService.Navigate(new EditDataPage(selecteditem));
        }

        private void DeleteSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            User selecteditem = (User)ListDataView.SelectedItem;
            if (selecteditem != null)
            {
                DbContextObject.db.Users.Remove(selecteditem);
                DbContextObject.db.SaveChanges();
                Page_Loaded(null, null);
            }
            else
                MessageBox.Show("Выберите элемент!");
        }
    }
}
