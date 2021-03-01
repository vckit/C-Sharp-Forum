using Olimpiad.Context;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Olimpiad.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewNotePage.xaml
    /// </summary>
    public partial class ViewNotePage : Page
    {
        public ViewNotePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e) => Update();

        private void Update()
        {
            ListNotesData.ItemsSource = null;
            var notes = DbContextObject.db.ClientService.ToList();
            notes = notes.Where(item => item.StartTime >= DateTime.Now && item.StartTime <= DateTime.Now.AddDays(1)).OrderBy(item => item.StartTime).ToList();
            ListNotesData.ItemsSource = notes;
        }
    }
}
