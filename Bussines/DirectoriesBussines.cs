using File_Manager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Bussines
{
    internal class DirectoriesBussines
    {
        Directories directories = new Directories();
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
            if (!Directory.Exists(directory.Directoryname))
            {
                Directory.CreateDirectory(directory.Directoryname);
            }
            else
            {
                result.Error = true;
                result.Message = "Directory exist";
            }
            return result;
        }
    }
}
