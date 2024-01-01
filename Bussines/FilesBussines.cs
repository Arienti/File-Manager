using File_Manager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Bussines
{
    internal class FilesBussines
    {

        Result result = new Result();

        public Result RemoveFiles(Files file)
        {
            string? fil = file.filesname;
            if (Directory.Exists(fil))
            {
                File.Delete(fil);
            }
            return result;
        }
    }
}
