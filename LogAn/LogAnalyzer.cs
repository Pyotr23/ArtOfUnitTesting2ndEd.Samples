using System;
using System.IO;
using System.Linq;

namespace LogAn
{
    public class LogAnalyzer
    {
        public bool WasLastFileNameValid { get; private set; }

        public bool IsValidLogFileName(string fileName)
        {
            WasLastFileNameValid = false;

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("имя файла должно быть задано");

            var extension = Path.GetExtension(fileName);
            var isValid = string.Equals(
                extension,
                ".SLF",
                StringComparison.CurrentCultureIgnoreCase);

            WasLastFileNameValid = isValid;

            return isValid;
        }

        public bool IsValidLogFileNameGettingFromConfig(string fileName)
        {
            IExtensionManager manager = new FileExtensionManager();
            return manager.IsValid(fileName);
        }
    }
}
