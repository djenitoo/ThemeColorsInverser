using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
// ^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$ hex
// ^({|;)
// MY: /(\w|\{|\*)([: "\._,%!#<>%=\-\(\)\*\w\d]+)(;|\})/g

namespace ThemeColorsInverser
{
    public static class StylesheetFileIterator
    {
        public static List<Color> GetHexColors(string[] files)
        {
            if (files == null || files.Length < 1)
            {
                return null;
            }

            var hexColors = new List<Color>();
            Regex regex = new Regex(@"#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})");
            
            foreach (var file in files)
            {
                var lines = File.ReadLines(file);
                foreach (var line  in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || regex.Matches(line).Count == 0)
                    {
                        continue;
                    }

                    foreach (Match match in regex.Matches(line))
                    {
                        hexColors.Add(new Color{Value = match.Value, FileName = file});
                        
                    }
                }
            }

            return hexColors;
        }
    }
}
