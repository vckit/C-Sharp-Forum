using System.Windows;
using WPF_1.Views.Pages;

namespace WPF_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new DataViewPage());
        }
    }
}
