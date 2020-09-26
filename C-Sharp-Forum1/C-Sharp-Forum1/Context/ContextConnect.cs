using C_Sharp_Forum1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Forum1.Context
{
    // Подключаемся к источнику Данных, который берём из Базы Данных
    public class ContextConnect
    {
        // Инициализируем экземпляр Базы Данных
        public static WPFEntities db = new WPFEntities();
    }
}
