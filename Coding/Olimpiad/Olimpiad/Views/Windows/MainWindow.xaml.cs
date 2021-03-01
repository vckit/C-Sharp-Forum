using Olimpiad.Views.Pages;
using System;
using System.Windows;

namespace Olimpiad
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new LoginPage());
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e) =>
            ButtonBack.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;

        private void ButtonBack_Click(object sender, RoutedEventArgs e) => MainFrame.GoBack();
    }
}
