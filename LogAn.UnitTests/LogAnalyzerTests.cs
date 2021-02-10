using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            var analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName("fileWithBadExtension.foo");
            Assert.False(result);
        }
    }
}
