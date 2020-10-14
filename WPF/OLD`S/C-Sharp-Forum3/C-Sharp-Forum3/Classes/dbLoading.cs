using C_Sharp_Forum3.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace C_Sharp_Forum3.Classes
{
    class dbLoading
    {
        public void LoadCountry(ComboBox comboBox)
        {
            try
            {
                var query = dbContext.db.CountryManufacture.Select(item => new
                {
                    ID = item.Title
                });
                var collection = from item in query select item.ID;
                comboBox.ItemsSource = collection.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
