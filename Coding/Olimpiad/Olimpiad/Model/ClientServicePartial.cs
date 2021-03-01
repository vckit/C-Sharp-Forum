using System;

namespace Olimpiad.Model
{
    partial class ClientService
    {
        // свойство вывода часов и минут в окно просмотра сущетсвующих записей
        public string TimeToStart
        {
            get
            {
                string line = (StartTime - DateTime.Now).ToString();
                string[] time = line.Split(':');
                return $"{line[0]} часов {line[1]} минут";
            }
        }
        // свойство возвращает цвет если запись уже горит
        public string Color
        {
            get
            {
                string[] time = TimeToStart.Split(' ');
                return time[0] == "00" ? "Red" : "Black";
            }
        }
    }
}
