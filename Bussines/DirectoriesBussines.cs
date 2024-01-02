using File_Manager.Models;
using System.IO;

namespace File_Manager.Bussines
{
    internal class DirectoriesBussines
    {
        Result result = new Result();

        public Result RemoveDirectory(Directories directory)
        {
            string dir = directory.Directoryname;
            if (Directory.Exists(dir))
            {
                Directory.Delete(dir);
            }
            return result;
        }
        public Result CreateDirectory(Directories directory)
        {
            Result result = new Result();
            directory.directories = new DirectoryInfo(Location.location).GetDirectories();

            if (Directory.Exists(directory.Directoryname))
            {
                result.Error = true;
                result.Message = "Directory is existing";
                return result;
            }
            else
            {
                Directory.CreateDirectory(directory.Directoryname);
                return result;
            }
        }
        public Result RenameDirectories(Directories directory, string newName)
        {
            Result result = new Result();
            directory.directories = new DirectoryInfo(Location.location).GetDirectories();

            if (Directory.Exists(newName))
            {
                result.Error = true;
                result.Message = "Directory is existing";
                return result;
            }
            else
            {
                Directory.Move(directory.Directoryname, newName);
                return result;
            }
        }
    }
}
