using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Demo.Context
{
    public static class dbLoadCombo
    {
        public static void LoadStatusFamily(ComboBox comboBox)
        {
            var query = dbContext.db.FamilyStatus.Select(item => new
            {
                title = item.Title
            });
            var collectionStatusFamily = from selectedItem in query select selectedItem.title;
            comboBox.ItemsSource = collectionStatusFamily.ToList();
        }
    }
}
