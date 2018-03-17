using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_2
{
    static class StationManager
    {
        internal static readonly string WorkingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ProjectOne");
        public static Person CurrentPerson { get; set; }
    }
}
