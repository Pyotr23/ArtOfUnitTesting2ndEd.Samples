using System;

namespace ArtOfUnitTesting2ndEd.Samples
{
    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string fileName)
        {
            if (fileName.EndsWith(".SLF"))
                return false;
            return true;
        }
    }
}
