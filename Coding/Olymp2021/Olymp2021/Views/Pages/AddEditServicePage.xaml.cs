using Microsoft.Win32;
using Olymp2021.Context;
using Olymp2021.Model;
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

namespace Olymp2021.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        private Service selectedService;
        public AddEditServicePage()
        {
            InitializeComponent();
            spId.Visibility = Visibility.Collapsed;
            RemoveAllOthrePic.Visibility = Visibility.Collapsed;
        }

        public AddEditServicePage(Service service)
        {
            InitializeComponent();
            this.selectedService = service;
            txbId.Text = service.ID.ToString();
            txbTitle.Text = service.Title;
            txbCost.Text = service.Cost.ToString();
            txbDuration.Text = service.DurationInSeconds.ToString();
            txbDicount.Text = service.Discount.ToString();
            txbDescription.Text = service.Description;
            var otherPictures = DbContextObject.db.ServicePhoto.Where(item => item.ServiceID == service.ID).ToList();
            for (int i = 0; i < otherPictures.Count; i++)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\" + otherPictures[i].PhotoPath.Trim()));
                image.Width = 150;
                image.Height = 150;
                lvOtherPicture.Items.Add(image);
            }
            ImgMainPic.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\" + service.MainImagePath.Trim()));
        }

        private void txbDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789:".IndexOf(e.Text) < 0;
        }

        private void txbCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void txbDicount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void btnLoadMainPic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if(file.ShowDialog() == true)
            {
                mainPathPicture = file.FileName;
                ImgMainPic.Source = new BitmapImage(new Uri(mainPathPicture));
            }
        }

        private void TextBlock_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] pictures = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < pictures.Length; i++)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri(pictures[i]));
                    image.Width = 150;
                    image.Height = 150;
                    lvOtherPicture.Items.Add(image);
                    othrePictures.Add(pictures[i]);
                }
            }
        }
        List<string> othrePictures = new List<string>();
        private string mainPathPicture = "";
        private void TextBlock_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Move;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(selectedService != null)
                {
                    bool error = false;
                    var service = DbContextObject.db.Service.Where(item => item.ID == selectedService.ID).FirstOrDefault();
                    var title = DbContextObject.db.Service.Where(item => item.Title.ToLower() == txbTitle.Text.ToLower()).ToList();
                    try
                    {
                        if (Convert.ToInt32(txbDuration.Text) > 14400)
                            error = true;
                    }
                    catch
                    {
                        throw new Exception("Длительность услуги должна быть указана числом");
                    }
                    if (error)
                        throw new Exception("Неверный формат данных");
                    else
                    {
                        try
                        {
                            service.Title = txbTitle.Text;
                            service.Description = txbDescription.Text;
                            service.DurationInSeconds = Convert.ToInt32(txbDuration.Text);
                            service.Cost = Convert.ToDecimal(txbCost.Text);
                            service.Discount = Convert.ToDouble(txbDicount.Text);
                            if (!string.IsNullOrEmpty(mainPathPicture) && !string.IsNullOrEmpty(service.MainImagePath))
                                File.Copy(mainPathPicture, service.MainImagePath.Trim(), true);
                            for (int i = 0; i < othrePictures.Count; i++)
                            {
                                ServicePhoto servicePhoto = new ServicePhoto();
                                servicePhoto.ServiceID = selectedService.ID;
                                servicePhoto.PhotoPath = $"Услуги школы\\{System.IO.Path.GetFileName(othrePictures[i]).Trim()}";
                                File.Copy(othrePictures[i], $"Услуги школы\\{System.IO.Path.GetFileName(othrePictures[i]).Trim()}", true);
                                DbContextObject.db.ServicePhoto.Add(servicePhoto);
                                DbContextObject.db.SaveChanges();
                            }
                            DbContextObject.db.SaveChanges();
                            MessageBox.Show("Успешно!");
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
                    var titles = DbContextObject.db.Service.Where(item => item.Title.ToLower() == txbTitle.Text.ToLower()).ToList();
                    if (titles.Count != 0)
                        error = true;
                    try
                    {
                        if (Convert.ToInt32(txbDuration.Text) > 14400)
                            error = true;
                    }
                    catch
                    {
                        throw new Exception("Длительность должна быть указана цифрой");
                    }
                    if (error)
                        throw new Exception("Неверный формат данных");
                    else
                    {
                        Service service = new Service();
                        service.Title = txbTitle.Text;
                        service.Description = txbDescription.Text;
                        service.Cost = Convert.ToDecimal(txbCost.Text);
                        service.Discount = Convert.ToDouble(txbDicount.Text);
                        service.DurationInSeconds = Convert.ToInt32(txbDuration.Text);
                        if(!string.IsNullOrEmpty(mainPathPicture) && !string.IsNullOrWhiteSpace(mainPathPicture))
                        {
                            File.Copy(mainPathPicture, $"Услуги школы\\{System.IO.Path.GetFileName(mainPathPicture).Trim()}");
                            service.MainImagePath = $"Услуги школы\\{System.IO.Path.GetFileName(mainPathPicture).Trim()}";
                        }
                        DbContextObject.db.Service.Add(service);
                        DbContextObject.db.SaveChanges();
                        for (int i = 0; i < othrePictures.Count; i++)
                        {
                            ServicePhoto servicePhoto = new ServicePhoto();
                            //servicePhoto.ServiceID = selectedService.ID;
                            servicePhoto.PhotoPath = $"Услуги школы\\{System.IO.Path.GetFileName(othrePictures[i].Trim())}";
                            File.Copy(othrePictures[i], $"Услуги школы\\{System.IO.Path.GetFileName(othrePictures[i]).Trim()}", true);
                            DbContextObject.db.ServicePhoto.Add(servicePhoto);
                            DbContextObject.db.SaveChanges();
                        }
                        DbContextObject.db.SaveChanges();
                        MessageBox.Show("Успешно!");
                        NavigationService.GoBack();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source + " выдал ошибку", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveAllOthrePic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(selectedService != null)
                {
                    var service = DbContextObject.db.Service.Where(item => item.ID == selectedService.ID).ToList();
                    if (File.Exists(selectedService.MainImagePath.Trim()))
                        File.Delete(selectedService.MainImagePath.Trim());
                    var serivicePictures = DbContextObject.db.ServicePhoto.Where(item => item.ServiceID == selectedService.ID).ToList();
                    for (int i = 0; i < serivicePictures.Count; i++)
                    {
                        if (File.Exists(serivicePictures[i].PhotoPath.Trim()))
                        {
                            File.Delete(serivicePictures[i].PhotoPath.Trim());
                            DbContextObject.db.ServicePhoto.Remove(serivicePictures[i]);
                        }
                    }
                }
                DbContextObject.db.SaveChanges();
                ImgMainPic.Source = null;
                lvOtherPicture.Items.Clear();
                MessageBox.Show("Все доп. изображения удалены!");
            }
            catch (Exception ex ) 
            {
                MessageBox.Show(ex.Message, ex.Source + " выдал исключенеи", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
