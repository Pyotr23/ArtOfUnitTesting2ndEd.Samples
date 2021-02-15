using System;

namespace LogAn
{
    public class FakeWebService : IWebService
    {
        public Exception ToThrow { get; set; }

        public string LastError { get; private set; }

        public void LogError(string message)
        {
            LastError = message;
        }

        public void Throw(string message)
        {
            if (ToThrow != null)
                throw ToThrow;
        }
    }
}
