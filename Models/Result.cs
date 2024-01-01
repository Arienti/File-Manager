using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Models
{
    internal class Result
    {
        public bool Error { get; set; } = false; // By default is not error
        public string Message { get; set; }
    }
}
