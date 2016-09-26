using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ThemeColorsInverser
{
    class Program
    {
        static void Main(string[] args)
        {
            var htmlFileBegin = "<html><body>";
            var htmlFileEnd = "</body></html>";
            var fileTitle = "<h2>{0}</h2>";
            var htmlBox = "<div style=\"display:inline-block;width:50px;height:50px;margin:5px;background-color:{0};\"></div>";

            var filesNames = DirectoryIterator.GetDirectoryFileNames("C:\\Users\\jenny\\Desktop\\Light");

            var foundHexColorsGroup = StylesheetFileIterator.GetHexColors(filesNames).GroupBy(c => c.FileName);

            using (FileStream fs = new FileStream("C:\\Users\\jenny\\Desktop\\Light\\colors.htm", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine(htmlFileBegin);
                    foreach (var group in foundHexColorsGroup)
                    {
                        w.WriteLine(fileTitle, group.Key);

                        foreach (var color in group)
                        {
                            w.WriteLine(htmlBox,color.Value);
                            //var oppositeColorArgb = System.Drawing.Color.FromArgb(ColorTranslator.FromHtml(color.Value).ToArgb() ^ 0xffffff);
                            //var oppositeColorHex = "#" + oppositeColorArgb.R.ToString("X2") + oppositeColorArgb.B.ToString("X2") + oppositeColorArgb.B.ToString("X2");
                            var oppositeColorArgb = ColorToHSV(ColorTranslator.FromHtml(color.Value));
                            var oppositeColorHex = "#" + oppositeColorArgb.R.ToString("X2") + oppositeColorArgb.B.ToString("X2") + oppositeColorArgb.B.ToString("X2");
                            w.WriteLine(htmlBox, oppositeColorHex);
                            w.WriteLine("<div></div>");

                        }
                        
                    }
                    w.WriteLine(htmlFileEnd);
                }
            }
        }

        public static System.Drawing.Color ColorToHSV(System.Drawing.Color color)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            var hue = (color.GetHue() + 180) % 360; // (Hue + 180) % 360
            var saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            var value = max / 255d;

            return HSVToRGB(hue, saturation, value);
        }

        public static System.Drawing.Color HSVToRGB(float hue, double saturation, double value)
        {
            var range = Convert.ToInt32(Math.Floor(hue / 60.0)) % 6;
            var f = hue / 60.0 - Math.Floor(hue / 60.0);

            var v = value * 255.0;
            var p = v * (1 - saturation);
            var q = v * (1 - f * saturation);
            var t = v * (1 - (1 - f) * saturation);

            switch (range)
            {
                case 0:
                    return System.Drawing.Color.FromArgb(255,(int)v, (int)t, (int)p);
                case 1:
                    return System.Drawing.Color.FromArgb(255,(int)q, (int)v, (int)p);
                case 2:
                    return System.Drawing.Color.FromArgb(255,(int)p, (int)v, (int)t);
                case 3:
                    return System.Drawing.Color.FromArgb(255,(int)p, (int)q, (int)v);
                case 4:
                    return System.Drawing.Color.FromArgb(255,(int)t, (int)p, (int)v);
            }
            return System.Drawing.Color.FromArgb(255,(int)v, (int)p, (int)q);
        }
    }
}
