using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Context;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Model;

namespace WpfApp1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для dataViewPage.xaml
    /// </summary>
    public partial class dataViewPage : Page
    {
        public dataViewPage()
        {
            InitializeComponent();
        }

        private void ButtonAddData_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new addPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataListView.ItemsSource = DbContextObj.db.Book.ToList();
        }

        private void DataListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var _selectedItemList = (Book)DataListView.SelectedItem;
            if (_selectedItemList != null)
                NavigationService.Navigate(new editDataPage(_selectedItemList));
            else
                MessageBox.Show("Выберите элемент!");
        }

        private void ButtonRemoveData_Click(object sender, RoutedEventArgs e)
        {
            var _selectedItem = (Book)DataListView.SelectedItem;
            if (_selectedItem != null)
            {
                DbContextObj.db.Book.Remove(_selectedItem);
                DbContextObj.db.SaveChanges();
                MessageBox.Show("Удаление прошло успешно!");
            }
            else
                MessageBox.Show("Выберите элемент!");
        }
    }
}
