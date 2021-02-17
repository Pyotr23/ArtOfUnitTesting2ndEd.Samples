using NSubstitute;
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

        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            var mockService = Substitute.For<IWebService>();
            var logAnalyzer = new LogAnalyzer(mockService)
            {
                MinNameLength = 8
            };
            var tooShortFileName = "abc.ext";

            logAnalyzer.Analyze(tooShortFileName);

            mockService.Received().LogError("Слишком короткое имя файла abc.ext");
        } 

        [Test]
        public void AnalyzeOrSend_WebServiceThrows_SendEmail()
        {
            var stubService = new FakeWebService
            {
                ToThrow = new Exception("fake exception")
            };

            var mockEmail = new FakeEmailService();
            var analyzer = new LogAnalyzer(stubService, mockEmail);
            var tooShortFileName ="abc.ext";

            analyzer.AnalyzeOrSend(tooShortFileName);

            StringAssert.Contains("someone@somewhere.com", mockEmail.To);
            StringAssert.Contains("fake exception", mockEmail.Body);
            StringAssert.Contains("can’t log", mockEmail.Subject);
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
