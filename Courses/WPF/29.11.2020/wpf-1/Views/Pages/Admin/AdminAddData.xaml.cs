using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using wpf_1.Context;
using wpf_1.Model;

namespace wpf_1.Views.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminAddData.xaml
    /// </summary>
    public partial class AdminAddData : Page
    {
        public AdminAddData( )
        {
            InitializeComponent();
        }

        private void btnCancel_Click( object sender, RoutedEventArgs e )
        {
            NavigationService.GoBack();
        }

        private void btnSave_Click( object sender, RoutedEventArgs e )
        {
            try
            {
                Computer computer = new Computer();
                computer.CPU = txbCPU.Text;
                computer.GPU= txbGPU.Text;
                computer.RAM = txbRAM.Text;
                computer.MotherBoard = txbMotherBoard.Text;
                computer.HDD = txbHDD.Text;
                computer.Keybourd = txbKeyboard.Text;
                computer.Mouse = txbMouse.Text;
                computer.Headphones = txbHeadphones.Text;
                dbContext.db.Computers.Add( computer );
                dbContext.db.SaveChanges();
                MessageBox.Show( "Данные сохранены!" );
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }
    }
}
