using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_1.Classes;
using WPF_1.Context;
using WPF_1.Model;

namespace WPF_1.Views.Pages
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
            dbDataView.ItemsSource = dbContext.db.Cars.ToList();
        }
        //Кнопка ПОДРОБНЕЕ
        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            Car selectedItem = (Car)dbDataView.SelectedItem;
            if (selectedItem != null)
            {
                NavigationService.Navigate(new DBGetInfoViewPage(selectedItem));
            }
        }
        //Кнопка создать ДАННЫЕ 
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDataInDB());
        }
        //Кнопка РЕДАКТИРОВАТЬ данные
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Car selectedTtem = (Car)dbDataView.SelectedItem;
            if (selectedTtem != null)
                NavigationService.Navigate(new EditDataInDB(selectedTtem));

        }
        // Кнопка УДАЛИТЬ данные
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Car selectedItem = (Car)dbDataView.SelectedItem;
                if (selectedItem != null)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить данную запись?", "Удалить?", MessageBoxButton.OKCancel, MessageBoxImage.Question)
                        == MessageBoxResult.OK)
                    {
                        Car removeCar = dbContext.db.Cars.FirstOrDefault(item => item.ID == selectedItem.ID);
                        dbContext.db.Cars.Remove(removeCar);
                        dbContext.db.SaveChanges();
                        Page_Loaded(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Поиск
        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dbDataView.ItemsSource = dbContext.db.Cars.Where(keyword => keyword.Title.Contains(txbSearch.Text) ||
            keyword.Model.Contains(txbSearch.Text)).ToList();
        }

    }
}
