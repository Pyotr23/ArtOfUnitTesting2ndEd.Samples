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

        [TestCase("badname.foo", false)]
        [TestCase("goodname.slf", true)]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid(string fileName, bool expected)
        {
            var analyzer = CreateAnalyzer();

            analyzer.IsValidLogFileName(fileName);

            Assert.AreEqual(expected, analyzer.WasLastFileNameValid);
        }

        [Test]
        public void IsValidFileNameGettingFromConfig_NameSupportedExtension_ReturnsTrue()
        {
            var fakeManager = new FakeExtensionManager()
            {
                WillBeValid = true
            };

            var analyzer = new LogAnalyzer(fakeManager);

            var result = analyzer.IsValidLogFileNameGettingFromConfig("good.slf");

            Assert.True(result);
        }

        [Test]
        public void OverrideTest()
        {
            var stub = new FakeExtensionManager
            {
                WillBeValid = true
            };

            var analyzer = new TestableLogAnalyzer(stub);

            var result = analyzer.IsValidLogFileName("file.ext");

            Assert.IsTrue(result);
        }

        private LogAnalyzer CreateAnalyzer()
        {
            return new LogAnalyzer();
        }
    }

    public class TestableLogAnalyzer : LogAnalyzerUsingFactoryMethod
    {
        private IExtensionManager _manager;

        public TestableLogAnalyzer(IExtensionManager manager)
        {
            _manager = manager;
        }

        public override IExtensionManager GetManager()
        {
            return new FakeExtensionManager();
        }
    }

    public class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid { get; set; }

        public bool IsValid(string fileName)
        {
            return true;
        }
    }
}
