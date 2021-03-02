using Olymp2021.Model;
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
using Olymp2021.Context;

namespace Olymp2021.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        Service selecedService;
        public AddClientPage(Service service)
        {
            InitializeComponent();
            this.selecedService = service;
            txbTitle.Text = service.Title;
            txbDuration.Text = service.DurationInSeconds.ToString();
            cmbClient.ItemsSource = DbContextObject.db.Client.ToList();
        }

        private void txbStartTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789:".IndexOf(e.Text) < 0;
        }

        private void txbStartTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txbStartTime.Text.Contains(":"))
                {
                    string[] line = txbStartTime.Text.Split(':');
                    if(!string.IsNullOrEmpty(line[1]) && !string.IsNullOrWhiteSpace(line[1]))
                    {
                        DateTime date = new DateTime();
                        date = Convert.ToDateTime("01.01.2021" + $" {line[0]}:{line[1]}");
                        date = date.AddMinutes(Convert.ToInt32(txbDuration.Text));
                        txbEndTime.Text = date.ToShortTimeString();
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, ex.Source + " выдал исключение!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientService clientService = new ClientService();
                clientService.ClientID = (cmbClient.SelectedItem as Client).ID;
                clientService.ServiceID = selecedService.ID;
                string date = dtpDate.SelectedDate.Value.ToShortDateString() + " " + txbStartTime.Text;
                clientService.StartTime = Convert.ToDateTime(date);
                clientService.Comment = txbComment.Text;
                DbContextObject.db.ClientService.Add(clientService);
                DbContextObject.db.SaveChanges();
                MessageBox.Show("Готово!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source + " выдал ошибку!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
