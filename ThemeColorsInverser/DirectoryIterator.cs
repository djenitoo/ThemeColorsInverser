using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeColorsInverser
{
    public static class DirectoryIterator
    {
        public static string[] DirectoryFiles;

        public static string[] GetDirectoryFileNames(string directoryUrl)
        {
            DirectoryFiles = Directory.GetFiles(directoryUrl, "*.css", SearchOption.AllDirectories);

            //// Display all the files.
            //foreach (string file in DirectoryFiles)
            //{
            //    Console.WriteLine(file);
            //}

            return DirectoryFiles;
        }
    }
}
