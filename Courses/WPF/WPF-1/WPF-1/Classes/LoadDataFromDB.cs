using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF_1.Context;

namespace WPF_1.Classes
{
    public static class LoadDataFromDB
    {
        //Загруить данные из таблицы ТИП
        public static void LoadType(ComboBox comboBox)
        {
            var query = dbContext.db.Types.Select(item => new
            {
                titleItem = item.Title
            });
            var collectionType = from selectedItem in query select selectedItem.titleItem;
            comboBox.ItemsSource = collectionType.ToList();
        }

        public static void LoadCountry(ComboBox comboBox)
        {
            var query = dbContext.db.Countries.Select(item => new
            {
                titleCountry = item.Title
            });
            var collectionCountries = from selectedItem in query select selectedItem.titleCountry;
            comboBox.ItemsSource = collectionCountries.ToList();
        }
    }
}
