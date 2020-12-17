using System;
using System.Collections.Generic;
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
using wpf_1.Context;
using wpf_1.Model;

namespace wpf_1.Views.Pages.Admin {
    /// <summary>
    /// Логика взаимодействия для AdminEditData.xaml
    /// </summary>
    public partial class AdminEditData : Page {
        private Computer selectedItem;
        public AdminEditData( ) {
            InitializeComponent();
        }
        // Принимаем данные
        public AdminEditData(Computer selectedItem ) {
            InitializeComponent();
            this.selectedItem = selectedItem;
            txb_CPU.Text = selectedItem.CPU;
            txb_GPU.Text = selectedItem.GPU;
            txb_RAM.Text = selectedItem.RAM;
            txb_Motherboard.Text = selectedItem.MotherBoard;
            txb_HDD.Text = selectedItem.HDD;
            txb_Keyboard.Text = selectedItem.Keybourd;
            txb_Mouse.Text = selectedItem.Mouse;
            txb_Headphones.Text = selectedItem.Headphones;
        }

        private void btnCancel_Click( object sender, RoutedEventArgs e ) {
            NavigationService.GoBack( ) ;
        }

        private void btnSave_Click( object sender, RoutedEventArgs e ) {
            try {
                Computer save = dbContext.db.Computers.FirstOrDefault( item => item.ID == selectedItem.ID );
                save.CPU = txb_CPU.Text;
                save.GPU = txb_GPU.Text;
                save.RAM = txb_RAM.Text;
                save.MotherBoard = txb_Motherboard.Text;
                save.HDD = txb_HDD.Text;
                save.Keybourd = txb_Keyboard.Text;
                save.Mouse = txb_Mouse.Text;
                save.Headphones = txb_Headphones.Text;
                dbContext.db.SaveChanges();
                MessageBox.Show( "Данные сохранены!" );
                NavigationService.GoBack();
            }
            catch (Exception ex) {
                MessageBox.Show( ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }
    }
}
