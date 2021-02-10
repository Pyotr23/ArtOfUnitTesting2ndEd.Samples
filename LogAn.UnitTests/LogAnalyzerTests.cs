using NUnit.Framework;
using System;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [TestCase]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            var analyzer = CreateAnalyzer();

            bool result = analyzer.IsValidLogFileName("fileWithBadExtension.foo");

            Assert.False(result);
        }        

        [TestCase("filewithgoodextension.SLF")]
        [TestCase("fileWithGoodExtension.slf")]
        public void IsValidLogFileName_ValidExtensions_ReturnsTrue(string fileName)
        {
            var analyzer = CreateAnalyzer();

            bool result = analyzer.IsValidLogFileName(fileName);

            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_EmptyFileName_Throws()
        {
            var analyzer = CreateAnalyzer();

            var ex = Assert.Catch<Exception>(() => analyzer.IsValidLogFileName(""));

            StringAssert.Contains("имя файла должно быть задано", ex.Message);
        }

        private LogAnalyzer CreateAnalyzer()
        {
            return new LogAnalyzer();
        }
    }
}
