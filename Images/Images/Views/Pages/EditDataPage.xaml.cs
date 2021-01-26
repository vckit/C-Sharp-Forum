using Images.Context;
using Images.Model;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Images.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditDataPage.xaml
    /// </summary>
    public partial class EditDataPage : Page
    {

        private User selectedItem;
        public EditDataPage(User user)
        {
            InitializeComponent();
            selectedItem = user;
            TitleTextBox.Text = selectedItem.Title;
            // Получаем массив байтов из данных и собираем картинку
            if (selectedItem.Pic != null)
            {
                BitmapImage bitmap = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(selectedItem.Pic))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
                PicImage.Source = bitmap;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

        }

        private void SelectionButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image (*.png; *.jpg; *.jpeg;)| *.png; *.jpg; *.jpeg;";
            if (file.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage(new Uri(file.FileName));
                PicImage.Source = image;
            }
        }

        private void AddDataButton_Click(object sender, RoutedEventArgs e)
        {
            User user = DbContextObject.db.Users.FirstOrDefault(item => item.ID == selectedItem.ID);
            user.Title = TitleTextBox.Text;
            // Разбиваем изображение на массив байтов
            MemoryStream stream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapImage)PicImage.Source));
            encoder.Save(stream);
            user.Pic = stream.ToArray();

            DbContextObject.db.SaveChanges();
            NavigationService.GoBack();

        }
    }
}
