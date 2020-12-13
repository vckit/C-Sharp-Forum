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
using wpf_1.Context;

namespace wpf_1.Views.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminViewData.xaml
    /// </summary>
    public partial class AdminViewData : Page
    {
        public AdminViewData( )
        {
            InitializeComponent();
        }

        private void btnAdd_Click( object sender, RoutedEventArgs e )
        {
            NavigationService.Navigate( new AdminAddData() );
        }

        private void btnEdit_Click( object sender, RoutedEventArgs e )
        {
            NavigationService.Navigate( new AdminEditData() );
        }

        private void btnRemove_Click( object sender, RoutedEventArgs e )
        {

        }

        private void Page_Loaded( object sender, RoutedEventArgs e )
        {
            dataView.ItemsSource = dbContext.db.Computers.ToList();
        }
    }
}
