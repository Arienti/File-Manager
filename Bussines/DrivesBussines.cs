using File_Manager.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace File_Manager.Bussines
{
    internal class DrivesBussines
    {
        Drives drives = new Drives();
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
            try
            {
                if (Location.location != string.Empty)
                {
                    directories.directories = new DirectoryInfo(Location.location).GetDirectories();

                    foreach (var d in directories.directories)
                    {
                        directories.Directoryname = d.Name;
                    }
                }
            }
            catch(UnauthorizedAccessException)
            {

            }
            return drive;
        }
    }
}
