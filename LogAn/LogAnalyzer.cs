using System;
using System.IO;

namespace LogAn
{
    public class LogAnalyzer
    {
        private readonly IExtensionManager _manager;
        private readonly IWebService _service;
        private readonly IEmailService _email;

        public bool WasLastFileNameValid { get; private set; }

        public LogAnalyzer(IWebService service)
        {
            _service = service;
        }

        public LogAnalyzer(IWebService service, IEmailService email)
            : this(service)
        {
            _email = email;
        }

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

        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
                _service.LogError("Слишком короткое имя файла " + fileName);
        }

        public void AnalyzeOrSend(string fileName)
        {
            if (fileName.Length >= 8)
                return;

            try
            {
                _service.Throw("Слишком короткое имя файла " + fileName);
            }
            catch (Exception e)
            {
                _email.SendEmail("someone@somewhere.com", "can’t log", e.Message);
            }            
        }
    }
}
