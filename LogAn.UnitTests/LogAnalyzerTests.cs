using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [TestCase]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            var analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName("fileWithBadExtension.foo");
            Assert.False(result);
        }        

        [TestCase("filewithgoodextension.SLF")]
        [TestCase("fileWithGoodExtension.slf")]
        public void IsValidLogFileName_ValidExtensions_ReturnsTrue(string fileName)
        {
            var analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName(fileName);
            Assert.True(result);
        }
    }
}
