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

namespace wpf_1.Views.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminEditData.xaml
    /// </summary>
    public partial class AdminEditData : Page
    {
        public AdminEditData( )
        {
            InitializeComponent();
        }

        private void btnCancel_Click( object sender, RoutedEventArgs e )
        {
            NavigationService.GoBack();
        }
    }
}
