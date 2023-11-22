using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Poll2
{
    internal static class Utility
    {
        public static readonly Random random = new Random();

        public static Color GetRandomColor()
        {
            byte[] rgb = new byte[3];
            random.NextBytes(rgb);
            return Color.FromRgb(rgb[0], rgb[1], rgb[2]);
        }

        public static Color Blue = Color.FromRgb(26, 140, 255);
    }
}
