using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_1.Classes;
using WPF_1.Context;
using WPF_1.Model;

namespace WPF_1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditDataInDB.xaml
    /// </summary>
    public partial class EditDataInDB : Page
    {
        private Car selectedItem;
        public EditDataInDB()
        {
            InitializeComponent();

        }

        public EditDataInDB(Car selectedItem)
        {
            InitializeComponent();
            this.selectedItem = selectedItem;
            txbTitle.Text = selectedItem.Title;
            txbModel.Text = selectedItem.Model;
            var selectedSpec = dbContext.db.Spetifications.FirstOrDefault(item => item.IDCar == selectedItem.ID);
            dptDate.SelectedDate = selectedItem.Yaer;
            txbVIn.Text = selectedSpec.VIN;
            txbCountDoors.Text = selectedSpec.CountOfDoors.ToString();
            if (selectedSpec.Pic != null)
            {
                BitmapImage bitmap = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(selectedSpec.Pic))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
                imgPic.Source = bitmap;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Imige (*.png; *.jpeg; *.jpg;) | *.png; *.jpeg; *.jpg; ";
                if (file.ShowDialog() == true)
                {
                    BitmapImage image = new BitmapImage(new Uri(file.FileName));
                    imgPic.Source = image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Spetification spetification = dbContext.db.Spetifications.FirstOrDefault(item => item.ID == selectedItem.ID);
                Car car = dbContext.db.Cars.FirstOrDefault(item => item.ID == selectedItem.ID);
                car.Title = txbTitle.Text;
                car.Model = txbModel.Text;
                car.Yaer = (DateTime)dptDate.SelectedDate;
                spetification.VIN = txbVIn.Text;
                spetification.CountOfDoors = int.Parse(txbCountDoors.Text);
                var currentType = dbContext.db.Types.FirstOrDefault(item => item.Title == cmbType.Text);
                spetification.IDType = currentType.ID;
                var currentCoutry = dbContext.db.Countries.FirstOrDefault(item => item.Title == cmbCoutry.Text);
                spetification.IDCountry = currentCoutry.CountryID;
                spetification.IDCar = car.ID;
                MemoryStream stream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapImage)imgPic.Source));
                encoder.Save(stream);
                spetification.Pic = stream.ToArray();
                dbContext.db.SaveChanges();
                MessageBox.Show("Сохранено!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataFromDB.LoadType(cmbType);
            LoadDataFromDB.LoadCountry(cmbCoutry);
        }
    }
}
