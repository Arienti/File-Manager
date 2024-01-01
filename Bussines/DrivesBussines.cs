using File_Manager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Bussines
{
    internal class DrivesBussines
    {
        Drives drives = new Drives();
        DirectoriesBussines Directories = new DirectoriesBussines();
        public List<DriveInfo> drivesList = new List<DriveInfo>();
        public DrivesBussines()
        {
            drives.drives = DriveInfo.GetDrives();
        }
        public  DrivesBussines GetDrives(Drives drive)
        {
            DrivesBussines drivesBussines = new DrivesBussines();

            for(int i = 0; i < drive.drives.Length; i++)
            {
                DriveInfo d = drive.drives[i];
                if (d.IsReady)
                {
                    drivesList.Add(d);
                }
            }
            return drivesBussines;
        }
        public DrivesBussines GetDirectories(Directories directories)
        {
            DrivesBussines drive = new DrivesBussines();
            if (Location.location != string.Empty)
            {
                directories.directories = new DirectoryInfo(Location.location).GetDirectories();

                foreach (var d in directories.directories)
                {
                    directories.Directoryname = d.Name;
                }
            }
            return drive;
        }
        public DrivesBussines GetFiles(Files files)
        {
            DrivesBussines drive = new DrivesBussines();
            if (Location.location != string.Empty)
            {
                files.files = new DirectoryInfo(Location.location).GetFiles();

                foreach (var f in files.files)
                {
                    files.filesname = f.Name;
                }
            }
            return drive;
        }
    }
}
