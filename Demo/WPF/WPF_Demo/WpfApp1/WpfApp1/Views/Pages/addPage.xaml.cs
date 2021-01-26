using Microsoft.Win32;
using System;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WpfApp1.Context;
using WpfApp1.Model;
using System.IO;

namespace WpfApp1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для addPage.xaml
    /// </summary>
    public partial class addPage : Page
    {
        public addPage()
        {
            InitializeComponent();
            // Как выгрузить данные из таблицы в ComboBox
            ComboBoxGenre.ItemsSource = DbContextObj.db.Genre.Select(item => item.Name).ToList();
            PublisherBookComboBox.ItemsSource = DbContextObj.db.Publisher.Select(item => item.Name).ToList();
            AuthorFirstNameComboBox.ItemsSource = DbContextObj.db.Author.Select(item => item.FirstName).ToList();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new mainPage());
        }

        private void btnLoadImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image (*.jpg; *.png; *.jpeg;) | *.jpg; *.png; *.jpeg;";
            if (file.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(file.FileName));
                Picture.Source = bitmap;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book book = new Book();
                book.Name = NameBookTextBox.Text;
                var currentGenre = DbContextObj.db.Genre.FirstOrDefault(item => item.Name == ComboBoxGenre.Text);
                book.IDGenre = currentGenre.ID;
                book.CountPage = int.Parse(CountPageBookTextBox.Text);
                var currentAutrhor = DbContextObj.db.Author.FirstOrDefault(item => item.FirstName == AuthorFirstNameComboBox.Text);
                book.IDAuthor = currentAutrhor.ID;
                book.Price = Convert.ToDecimal(PriceTextBox.Text);
                var currentPublisher = DbContextObj.db.Publisher.FirstOrDefault(item => item.Name == PublisherBookComboBox.Text);
                book.IDPublisher = currentPublisher.ID;
                // Добавить ФОТОГРАФИЮ в Базу Данных
                {
                    MemoryStream stream = new MemoryStream();
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapImage)Picture.Source));
                    encoder.Save(stream);
                    book.Picture = stream.ToArray();
                }
                DbContextObj.db.Book.Add(book);
                DbContextObj.db.SaveChanges();
                MessageBox.Show("Сохранено!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
        }
    }
}
