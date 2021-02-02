using System.Windows;
using WPF_Demo.Views.Pages;

namespace WPF_Demo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new DataViewPage());
        }
    }
}
