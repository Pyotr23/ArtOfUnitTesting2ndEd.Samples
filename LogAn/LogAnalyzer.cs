using System;
using System.IO;

namespace LogAn
{
    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("имя файла должно быть задано");

            var extension = Path.GetExtension(fileName);
            return string.Equals(
                extension, 
                ".SLF", 
                StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
