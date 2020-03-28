using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_Lab1
{
    class TooLargeFileException : Exception
    {
        public TooLargeFileException(long fileSize, string fileSizeUnit, long maxFileSize, string maxfileSizeUnit) 
            : base($"File which You're trying to open, is too large. File has {fileSize} {fileSizeUnit}, but it mustn't be greater than {maxFileSize} {maxfileSizeUnit}")
        {
            
        }
    }
}
