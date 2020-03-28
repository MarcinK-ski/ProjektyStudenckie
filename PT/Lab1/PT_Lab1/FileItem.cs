using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_Lab1
{
    public class FileItem : StructureItem
    {
        public long FileSize { get; set; }

        public FileItem(string title, string path) : base(title, path)
        {
            
        }

        public long GetFileSizeInMegabytes()
        {
            return FileSize / 1024 / 1024;
        }

        public override string ToString()
        {
            return $"{Title} {GetFileSizeInMegabytes()} MB";
        }
    }
}
