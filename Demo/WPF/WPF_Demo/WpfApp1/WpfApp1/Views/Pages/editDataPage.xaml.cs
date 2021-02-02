using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WpfApp1.Context;
using WpfApp1.Model;

namespace WpfApp1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для editDataPage.xaml
    /// </summary>
    public partial class editDataPage : Page
    {
        private Book _selectedItem;
        public editDataPage(Book selectedItem)
        {
            InitializeComponent();
           
            this._selectedItem = selectedItem;
            NameBookTextBox.Text = selectedItem.Name;
            CountPageBookTextBox.Text = selectedItem.CountPage.ToString();
            PriceTextBox.Text = selectedItem.Price.ToString();
            if (selectedItem.Picture != null)
            {
                BitmapImage bitmap = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(selectedItem.Picture))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
                Picture.Source = bitmap;
            }
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
                Book book = DbContextObj.db.Book.FirstOrDefault(item => item.ID == _selectedItem.ID);
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
                DbContextObj.db.SaveChanges();
                MessageBox.Show("Сохранено!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxGenre.ItemsSource = DbContextObj.db.Genre.Select(item => item.Name).ToList();
            PublisherBookComboBox.ItemsSource = DbContextObj.db.Publisher.Select(item => item.Name).ToList();
            AuthorFirstNameComboBox.ItemsSource = DbContextObj.db.Author.Select(item => item.FirstName).ToList();
        }
    }
}
