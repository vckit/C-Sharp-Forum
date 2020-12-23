using System;
using System.Collections.Generic;
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
using WPF_1.Context;
using WPF_1.Model;

namespace WPF_1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DBGetInfoViewPage.xaml
    /// </summary>
    public partial class DBGetInfoViewPage : Page
    {
        private Car selectedItem;
        public DBGetInfoViewPage()
        {
            InitializeComponent();
        }
        public DBGetInfoViewPage(Car selectedItem)
        {
            InitializeComponent();
            this.selectedItem = selectedItem;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dbDataGetInfoView.ItemsSource = dbContext.db.Spetifications.Where(item => item.IDCar == selectedItem.ID).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
