using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Models
{
    internal class Drives
    {
        public DriveInfo[] drives = DriveInfo.GetDrives();
        public string drivername { get; set; } = "";
    }
}
