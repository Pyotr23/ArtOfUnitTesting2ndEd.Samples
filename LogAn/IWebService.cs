namespace LogAn
{
    public interface IWebService
    {
        void LogError(string message);
        void Throw(string message);
    }
}
