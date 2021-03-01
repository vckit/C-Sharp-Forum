using System;
using System.IO;
using System.Windows.Media;

namespace Olymp2021.Model
{
    partial class Service
    {
        public byte[] Photo
        {
            get
            {
                return MainImagePath != null && File.Exists(MainImagePath.Trim()) ? File.ReadAllBytes(MainImagePath.Trim()) : null;
            }
        }

        public int Minute
        {
            get
            {
                return DurationInSeconds / 60;
            }
        }

        public double Price
        {
            get
            {
                return Convert.ToDouble(Cost) - Convert.ToDouble(Cost) * Convert.ToDouble(Discount) / 100;
            }
        }

        public string BackgroundColor
        {
            get
            {
                return Discount != 0 ? Color.FromRgb(231, 250, 191).ToString() : Color.FromRgb(255, 255, 255).ToString();
            }
        }
    }
}
