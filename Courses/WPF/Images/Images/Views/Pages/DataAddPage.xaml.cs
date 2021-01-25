using Images.Context;
using Images.Model;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Images.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DataAddPage.xaml
    /// </summary>
    public partial class DataAddPage : Page
    {
        public DataAddPage()
        {
            InitializeComponent();

        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddDataButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Title = TitleTextBox.Text;
            // Разбиваем изображение на массив байтов
            MemoryStream stream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapImage)PicImage.Source));
            encoder.Save(stream);
            user.Pic = stream.ToArray();

            DbContextObject.db.Users.Add(user);
            DbContextObject.db.SaveChanges();
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
    }
}
