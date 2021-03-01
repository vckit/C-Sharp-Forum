using Olymp2021.Views.Pages;
using System;
using System.Windows;

namespace Olymp2021
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new LoginPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) => mainFrame.GoBack();

        private void mainFrame_ContentRendered(object sender, EventArgs e) =>
            btnBack.Visibility = mainFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
    }
}
