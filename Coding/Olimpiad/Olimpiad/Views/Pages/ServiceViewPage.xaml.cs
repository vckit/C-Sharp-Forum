using Olimpiad.Context;
using Olimpiad.Model;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Olimpiad.Views.Pages
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

        private void Update(string sort = "", string discount_filter = "", string search = "")
        {
            var services = DbContextObject.db.Service.ToList();
            int all = DbContextObject.db.Service.Count();
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(sort))
            {
                if (sort == "По воразстанию")
                    services = services.OrderBy(item => item.Price).ToList();
                else
                    services = services.OrderByDescending(item => item.Price).ToList();
            }
            if (!string.IsNullOrEmpty(discount_filter) && !string.IsNullOrEmpty(discount_filter))
            {
                if (discount_filter == "от 0% до 5%")
                    services = services.Where(item => 0 <= item.Discount && item.Discount < 5).ToList();
                if (discount_filter == "от 5% до 15%")
                    services = services.Where(item => 5 <= item.Discount && item.Discount < 15).ToList();
                if (discount_filter == "от 15% до 30%")
                    services = services.Where(item => 15 <= 30 && item.Discount < 30).ToList();
                if (discount_filter == "от 30% до 70%")
                    services = services.Where(item => 30 <= item.Discount && item.Discount < 70).ToList();
                if (discount_filter == "от 70% до 100%")
                    services = services.Where(item => 70 <= item.Discount && item.Discount < 100).ToList();
            }
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(search))
                services = services.Where(item => item.Title.ToLower().Contains(txbSearch.Text.ToLower()) || item.Description.ToLower().Contains(search.ToLower())).ToList();
            txbCountVisibleItem.Text = $"{services.Count} из {all}";
            listServiceData.ItemsSource = services;
        }

        private void btnSelectedServiceEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = listServiceData.SelectedItem as Service;
            if (selectedItem != null)
                NavigationService.Navigate(new AddEditServicePage(selectedItem));
        }
        // Удаление
        private void btnSelectedServiceRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = listServiceData.SelectedItem as Service;
                if (selectedItem != null)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "Подтвердите удаление.", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var notes = DbContextObject.db.ClientService.Where(item => item.ServiceID == selectedItem.ID).ToList();
                        if (notes.Count == 0)
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
                            throw new Exception("Услуга не может быть удалена, так как на него записались клиенты!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnViewNotes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // сортировка по возрастанию и убыванию
            Update((cmbSort.SelectedItem as ComboBoxItem).Content.ToString(), cmbSortDiscount.Text, txbSearch.Text);
        }

        private void cmbSortDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // сортировка по скидкам
            Update(cmbSort.Text, (cmbSortDiscount.SelectedItem as ComboBoxItem).Content.ToString(), txbSearch.Text);
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            // поиск
            Update(cmbSort.Text, cmbSortDiscount.Text, txbSearch.Text);
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditServicePage());
        }
    }
}
