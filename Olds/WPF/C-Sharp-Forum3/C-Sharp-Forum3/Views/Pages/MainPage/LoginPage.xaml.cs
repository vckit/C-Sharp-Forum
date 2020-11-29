using C_Sharp_Forum3.Context;
using C_Sharp_Forum3.Views.Pages.Users;
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

namespace C_Sharp_Forum3.Views.Pages.MainPage
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public void Authorization()
        {
            try
            {
                var currentUser = dbContext.db.SignIn.FirstOrDefault(item => item.Username == txbUsername.Text &&
                item.Password == psbPassword.Password);
                if (currentUser == null)
                    MessageBox.Show("Такого пользователя не в Базе Даннх!", "НЕ ВЕРНО!", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                else
                {
                    switch (currentUser.IDRole)
                    {
                        case "A":
                            NavigationService.Navigate(new UserViewData());
                            break;
                        case "U":
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source + "выдал исключение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                MessageBox.Show("Привет, я Finally, я блок кода, который сработает в любом случае, если даже try не выдаст исключение!",
                    "Пояснительная записка", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            Authorization();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите остановить программу?","Уведомление системы", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Authorization();
        }
    }
}
