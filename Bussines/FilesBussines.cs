using File_Manager.Models;
using System.IO;

namespace File_Manager.Bussines
{
    internal class FilesBussines
    {

        Result result = new Result();

        public Result RemoveFiles(Files file)
        {
            string? fil = file.filesname;
            if (File.Exists(fil))
            {
                File.Delete(fil);
            }
            return result;
        }
        public Result CreateFile(Files files)
        {
            files.files = new DirectoryInfo(Location.location).GetFiles();
            files.filesname = files.filesname + ".txt";
            if (File.Exists(files.filesname))
            {
                result.Error = true;
                result.Message = "File exist";
                return result;
            }
            else
            {
                File.Create(files.filesname);
            }
            return result;
        }
        public Result RenameFile(Files files, string newFileName)
        {
            files.files = new DirectoryInfo(Location.location).GetFiles();

            if (File.Exists(newFileName))
            {
                result.Error = true;
                result.Message = "File exist";
                return result;
            }
            else
            {
                if (files.filesname != null)
                {
                    File.Move(files.filesname, newFileName);
                }
            }
            return result;
        }
    }
}
