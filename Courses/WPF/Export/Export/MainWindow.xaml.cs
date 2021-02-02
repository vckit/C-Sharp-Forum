using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Export
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
            model.Rows = new List<Model>
            {
                 new Model { FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1988, 12, 19) },
                 new Model { FirstName = "Lara", LastName = "Croft", DateOfBirth = new DateTime(1975, 5, 3) },
                 new Model { FirstName = "Sam", LastName = "Fisher", DateOfBirth = new DateTime(1967, 2, 9) }
            };
            DataView.ItemsSource = model.Rows.ToList();
        }

        private void CopyAsCsvHandler(DataGrid dg)
        {
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataView.SelectAllCells();
            DataView.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, DataView);
            DataView.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            File.AppendAllText("D:\\test.csv", result, UnicodeEncoding.UTF8);
        }
    }
}
