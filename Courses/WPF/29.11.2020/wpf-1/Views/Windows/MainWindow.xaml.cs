using System.Windows;
using wpf_1.Views.Pages;

namespace wpf_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow( )
        {
            InitializeComponent();
            mainFrame.Navigate( new Authorization() );
        }
    }
}
