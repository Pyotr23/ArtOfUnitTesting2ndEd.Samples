using System;
using System.IO;

namespace LogAn
{
    public class LogAnalyzer
    {
        private readonly IExtensionManager _manager;

        public bool WasLastFileNameValid { get; private set; }

        public LogAnalyzer(IExtensionManager manager)
        {
            _manager = manager;
        }

        public LogAnalyzer()
        { }

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
            return _manager.IsValid(fileName);
        }
    }
}
