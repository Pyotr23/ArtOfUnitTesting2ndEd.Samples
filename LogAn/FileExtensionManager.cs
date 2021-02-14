using System.IO;
using System.Linq;

namespace LogAn
{
    public class FileExtensionManager : IExtensionManager
    {
        public bool IsValid(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(extension))
                return false;

            var extensionWithoutDot = extension[1..];
            var extensions = File.ReadAllLines("FileExtensions.txt");

            return extensions.Contains(extensionWithoutDot);
        }
    }
}
