using System;
using System.Collections.Generic;
using System.IO;
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
using Olymp2021.Context;
using Olymp2021.Model;

namespace Olymp2021.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServiceViewPage.xaml
    /// </summary>
    public partial class ServiceViewPage : Page
    {
        public ServiceViewPage()
        {
            InitializeComponent();
        }

        private void Update(string sort = "", string sort_discount = "", string search = "")
        {
            var service = DbContextObject.db.Service.ToList();
            int all = DbContextObject.db.Service.Count();
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(sort))
            {
                if (sort == "По возрастанию")
                    service = service.OrderBy(item => item.Cost).ToList();
                else
                    service = service.OrderByDescending(item => item.Cost).ToList();
            }

            if (!string.IsNullOrEmpty(sort_discount) && !string.IsNullOrEmpty(sort_discount))
            {
                if (sort_discount == "от 0% до 5%")
                    service = service.Where(item => 0 <= item.Discount && item.Discount <= 5).ToList();
                if (sort_discount == "от 5% до 15%")
                    service = service.Where(item => 5 <= item.Discount && item.Discount <= 15).ToList();
                if (sort_discount == "от 15% до 30%")
                    service = service.Where(item => 15 <= item.Discount && item.Discount <= 30).ToList();
                if (sort_discount == "от 30% до 70%")
                    service = service.Where(item => 30 <= item.Discount && item.Discount <= 70).ToList();
                if (sort_discount == "от 70% до 100%")
                    service = service.Where(item => 70 <= item.Discount && item.Discount <= 100).ToList();
            }
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(search))
                service = service.Where(item => item.Title.ToLower().Contains(txbSearch.Text.ToLower()) || item.Title.ToLower().Contains(txbSearch.Text.ToLower())).ToList();
            CountItemsDB.Text = $"{service.Count} из {all}";
            ServiceListView.ItemsSource = service;
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditServicePage());
        }

        private void btnViewNotes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewNotePage());
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            var selectedService = ServiceListView.SelectedItem as Service;
            if (selectedService != null)
                NavigationService.Navigate(new AddClientPage(selectedService));
        }

        private void EditSelectedItemButton_Click(object sender, RoutedEventArgs e)
        {
            var serviceSelected = ServiceListView.SelectedItem as Service;
            if (serviceSelected != null)
                NavigationService.Navigate(new AddEditServicePage(serviceSelected));
        }

        private void RemoveSelectedItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = ServiceListView.SelectedItem as Service;
                if (selectedItem != null)
                {
                    if(MessageBox.Show("Вы действительно хотите удалить эту запись?", "Подтвердите удаление услуги!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var existNotes = DbContextObject.db.ClientService.Where(item => item.ServiceID == selectedItem.ID).ToList();
                        if (existNotes.Count == 0)
                        {
                            var pictures = DbContextObject.db.ServicePhoto.Where(item => item.ServiceID == selectedItem.ID).ToList();
                            foreach (var item in pictures)
                            {
                                File.Delete(item.PhotoPath.Trim());
                            }
                            if (pictures.Count != 0)
                            {
                                DbContextObject.db.ServicePhoto.RemoveRange(pictures);
                            }
                            DbContextObject.db.Service.Remove(selectedItem);
                            DbContextObject.db.SaveChanges();
                            Update(cmbSort.Text, cmbSortDiscount.Text, txbSearch.Text);
                        }
                        else
                            MessageBox.Show("Запрещено удалять услуги, куда записались клиенты!", "Безопасность системы", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source + " выдал исключение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update((cmbSort.SelectedItem as ComboBoxItem).Content.ToString(), cmbSortDiscount.Text, txbSearch.Text);
        }

        private void cmbSortDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update(cmbSort.Text, (cmbSortDiscount.SelectedItem as ComboBoxItem).Content.ToString(), txbSearch.Text);
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update(cmbSort.Text, cmbSortDiscount.Text, txbSearch.Text);
        }
    }
}
