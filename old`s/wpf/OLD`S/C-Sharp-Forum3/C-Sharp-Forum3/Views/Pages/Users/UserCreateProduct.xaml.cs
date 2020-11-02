using C_Sharp_Forum3.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Diagnostics;
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

namespace C_Sharp_Forum3.Views.Pages.Users
{
    /// <summary>
    /// Interaction logic for UserCreateProduct.xaml
    /// </summary>
    public partial class UserCreateProduct : Page
    {
        public UserCreateProduct()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dbLoading dbLoad = new dbLoading();
            dbLoad.LoadCountry(cmbCountry);
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
