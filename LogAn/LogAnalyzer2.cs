using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class LogAnalyzer2
    {
        private ILogger _logger;
        private IWebService _webService;


        public LogAnalyzer2(ILogger logger, IWebService webService)
        {
            _logger = logger;
            _webService = webService;
        }

        public int MinNameLength { get; set; }

        public void Analyze(string filename)
        {
            if (filename.Length < MinNameLength)
            {
                try
                {
                    _logger.LogError(string.Format("Filename too short: {0}", filename));
                }
                catch (Exception e)
                {
                    _webService.Write("Error From Logger: " + e);

                }
            }
        }
    }

    public interface ILogger
    {
        void LogError(string message);
    }

    public interface IWebService
    {
        void Write(string message);
        void Write(ErrorInfo message);
    }

    public class ErrorInfo
    {
        private readonly int _severity;
        private readonly string _message;

        public ErrorInfo(int severity, string message)
        {
            _severity = severity;
            _message = message;
        }

        public int Severity
        {
            get { return _severity; }
        }

        public string Message
        {
            get { return _message; }
        }

        protected bool Equals(ErrorInfo other)
        {
            return _severity == other._severity && string.Equals(_message, other._message);
        }

        //this is needed to make this test pass:
        // Analyze_LoggerThrows_CallsWebServiceWithNSubObjectCompare
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ErrorInfo)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_severity * 397) ^ _message.GetHashCode();
            }
        }
    }

}
