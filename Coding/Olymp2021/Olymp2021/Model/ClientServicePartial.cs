using System;

namespace Olymp2021.Model
{
    partial class ClientService
    {
        public string TimeToStart
        {
            get
            {
                string line = (StartTime - DateTime.Now).ToString();
                string[] time = line.Split(':');
                return $"{time[0]} часов {time[1]} минут";
            }
        }
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
