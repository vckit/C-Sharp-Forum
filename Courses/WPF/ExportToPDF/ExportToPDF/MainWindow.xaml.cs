using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ExportToPDF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model model = new Model();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Заполняем тестовые данные. Иммитируют базу данных
            model.Rows = new List<Model>
            {
                new Model {Name = "Abdulkhakim", Surname = "Magomedov", Age = 18, Instagram = "vc_kit"},
                new Model{Name = "Arina", Surname = "Dobro", Age = 17, Instagram = "vcarina"},
                new Model{Name = "Kristina", Surname = "Mirnaya", Age = 18, Instagram = "mirnaya_kris"}
            };
            DataView.ItemsSource = model.Rows.ToList();
        }
        // Экспорт данных в PDF
        private void ButtomExport_Click(object sender, RoutedEventArgs e)
        {
            // Основной функционал для экспорта данных
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(DataView, "invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
