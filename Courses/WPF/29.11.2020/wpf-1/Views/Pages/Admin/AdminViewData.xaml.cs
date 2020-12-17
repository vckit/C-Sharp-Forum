using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using wpf_1.Context;
using wpf_1.Model;

namespace wpf_1.Views.Pages.Admin {
    /// <summary>
    /// Логика взаимодействия для AdminViewData.xaml
    /// </summary>
    public partial class AdminViewData : Page {
        public AdminViewData( ) {
            InitializeComponent();
        }

        private void btnAdd_Click( object sender, RoutedEventArgs e ) {
            NavigationService.Navigate( new AdminAddData() );
        }
        // Кнопка РЕДАКТИРОВАТЬ
        private void btnEdit_Click( object sender, RoutedEventArgs e ) {
            try {

                Computer editComputer = (Computer)dataView.SelectedItem;
                if (editComputer != null)
                    NavigationService.Navigate( new AdminEditData( editComputer ) );
                else
                    throw new Exception( "Вы не выбрали ни одного элемента!" );
            }
            catch (Exception ex) {
                MessageBox.Show( ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error );
            }

        }
        // Кнопка УДАЛИТЬ
        private void btnRemove_Click( object sender, RoutedEventArgs e ) {
            try {
                Computer removeComputer = (Computer)dataView.SelectedItem;
                if (MessageBox.Show( "Вы действительно хотите далить выбранный элемент? Данные будут удалены навсегда!",
                    "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question ) == MessageBoxResult.Yes) {
                    if (removeComputer != null) {
                        dbContext.db.Computers.Remove( removeComputer );
                        dbContext.db.SaveChanges();
                        Page_Loaded( null, null );
                    }
                    else
                        throw new Exception( "Вы ничего не выбрали, что мне удалить?" );
                }
            }
            catch (Exception ex) {
                MessageBox.Show( ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }

        private void Page_Loaded( object sender, RoutedEventArgs e ) {
            dataView.ItemsSource = dbContext.db.Computers.ToList();
        }

        private void txbSearch_TextChanged( object sender, TextChangedEventArgs e ) {
            dataView.ItemsSource = dbContext.db.Computers.Where( item => item.CPU.Contains( txbSearch.Text ) ||
            item.ID.ToString().Contains( txbSearch.Text ) || item.RAM.Contains( txbSearch.Text ) ).ToList();
        }
    }
}
