using Microsoft.Win32;
using Olimpiad.Context;
using Olimpiad.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Olimpiad.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        private Service _service;
        public AddEditServicePage()
        {
            InitializeComponent();
            PanelId.Visibility = Visibility.Collapsed;
            PanelRemoveAllPic.Visibility = Visibility.Collapsed;
        }

        public AddEditServicePage(Service service)
        {
            InitializeComponent();
            this._service = service;
            txbId.Text = service.ID.ToString();
            txbTitle.Text = service.Title;
            txbCost.Text = service.Cost.ToString();
            txbDiscount.Text = service.Discount.ToString();
            txbDescription.Text = service.Description;
            txbDuration.Text = service.DurationInSeconds.ToString();
            var pictures = DbContextObject.db.ServicePhoto.Where(item => item.ServiceID == service.ID).ToList();
            for (int i = 0; i < pictures.Count; i++)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\" + pictures[i].PhotoPath.Trim()));
                image.Width = 190;
                image.Height = 150;
                lvOtherPicService.Items.Add(image);
            }
            imgMainPic.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\" + service.MainImagePath.Trim()));
        }
        // список для сохранения путей доп. изображений
        List<string> picturesPath = new List<string>();
        private string _generalPicturesPath = "";

        private void TextBlock_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // массив для хранения путей выбранных доп. изображений
                string[] pictures = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < pictures.Length; i++)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri(pictures[i]));
                    image.Width = 190;
                    image.Height = 150;
                    lvOtherPicService.Items.Add(image);
                    picturesPath.Add(pictures[i]);
                }
            }
        }

        private void TextBlock_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Move;
        }

        private void btnLoadImgMain_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == true)
            {
                _generalPicturesPath = file.FileName;
                imgMainPic.Source = new BitmapImage(new Uri(_generalPicturesPath));
            }
        }

        private void RemoveAllOtherPic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_service != null)
                {
                    var service = DbContextObject.db.Service.Where(item => item.ID == _service.ID).FirstOrDefault();
                    if (File.Exists(_service.MainImagePath.Trim()))
                        File.Delete(_service.MainImagePath.Trim());
                    service.MainImagePath = null;
                    var servicePicture = DbContextObject.db.ServicePhoto.Where(item => item.ServiceID == _service.ID).ToList();
                    for (int i = 0; i < servicePicture.Count; i++)
                    {
                        if (File.Exists(servicePicture[i].PhotoPath.Trim()))
                        {
                            File.Delete(servicePicture[i].PhotoPath.Trim());
                            DbContextObject.db.ServicePhoto.Remove(servicePicture[i]);
                        }
                    }
                }
                DbContextObject.db.SaveChanges();
                imgMainPic.Source = null;
                lvOtherPicService.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_service != null)
                {
                    bool error = false;
                    var service = DbContextObject.db.Service.Where(item => item.ID == _service.ID).FirstOrDefault();
                    var title = DbContextObject.db.Service.Where(item => item.Title.ToLower() == txbTitle.Text.ToLower()).ToList();
                    try
                    {
                        if (Convert.ToInt32(txbDuration.Text) > 14400)
                            error = true;
                    }
                    catch
                    {
                        throw new Exception("Длительность должна быть указана числом!");
                    }
                    if (error)
                        throw new Exception("Неверно введены данные");
                    else
                    {
                        try
                        {
                            service.Title = txbTitle.Text;
                            service.Cost = Convert.ToDecimal(txbCost.Text);
                            service.DurationInSeconds = Convert.ToInt32(txbDuration.Text);
                            service.Description = txbDescription.Text;
                            service.Discount = Convert.ToDouble(txbDiscount.Text);
                            if (!string.IsNullOrEmpty(_generalPicturesPath) && !string.IsNullOrEmpty(service.MainImagePath))
                                File.Copy(_generalPicturesPath, service.MainImagePath.Trim(), true);
                            for (int i = 0; i < picturesPath.Count; i++)
                            {
                                ServicePhoto servicePhoto = new ServicePhoto();
                                servicePhoto.ServiceID = _service.ID;
                                servicePhoto.PhotoPath = $" Услуги школы\\ {System.IO.Path.GetFileName(picturesPath[i]).Trim()}";
                                File.Copy(picturesPath[i], $"Услуги школы\\ {System.IO.Path.GetFileName(picturesPath[i]).Trim()}", true);
                                DbContextObject.db.ServicePhoto.Add(servicePhoto);
                                DbContextObject.db.SaveChanges();
                            }
                            DbContextObject.db.SaveChanges();
                            NavigationService.GoBack();
                        }
                        catch
                        {
                            throw new Exception("Неверно введены данные");
                        }
                    }
                }
                else
                {
                    bool error = false;
                    var titles = DbContextObject.db.Service.Where(item => item.Title.ToLower() == txbTitle.Text).ToList();
                    if (titles.Count != 0)
                    {
                        error = true;
                    }
                    try
                    {
                        if (Convert.ToInt32(txbDuration.Text) > 14400)
                            error = true;
                    }
                    catch
                    {
                        throw new Exception("Длительность должна быть указана целыми цифрами!");
                    }
                    if (error)
                        throw new Exception("Неверный формат данных");
                    else
                    {
                        Service service = new Service();
                        service.Title = txbTitle.Text;
                        service.Description = txbDescription.Text;
                        service.DurationInSeconds = Convert.ToInt32(txbDuration.Text);
                        service.Discount = Convert.ToDouble(txbDiscount.Text);
                        if (!string.IsNullOrEmpty(_generalPicturesPath) && !string.IsNullOrWhiteSpace(_generalPicturesPath))
                        {
                            File.Copy(_generalPicturesPath, $"Услуги школы\\{System.IO.Path.GetFileName(_generalPicturesPath)}", true);
                            service.MainImagePath = $"Услуги школы\\{System.IO.Path.GetFileName(_generalPicturesPath)}";
                        }
                        DbContextObject.db.Service.Add(service);
                        DbContextObject.db.SaveChanges();
                        for (int i = 0; i < picturesPath.Count; i++)
                        {
                            ServicePhoto servicePhoto = new ServicePhoto();
                            servicePhoto.ServiceID = service.ID;
                            servicePhoto.PhotoPath = $"Услуги школы\\{System.IO.Path.GetFileName(picturesPath[i])}";
                            File.Copy(picturesPath[i], $"Услуги школы\\{System.IO.Path.GetFileName(picturesPath[i])}", true);
                            DbContextObject.db.ServicePhoto.Add(servicePhoto);
                            DbContextObject.db.SaveChanges();
                        }
                        DbContextObject.db.SaveChanges();
                        NavigationService.GoBack();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
