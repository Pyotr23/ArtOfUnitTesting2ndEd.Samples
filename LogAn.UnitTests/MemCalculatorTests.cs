using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class MemCalculatorTests
    {
        [Test]
        public void Sum_ByDefault_ReturnsZero()
        {
            var calculator = CreateCalculator();

            var lastSum = calculator.Sum();

            Assert.AreEqual(0, lastSum);
        }

        [Test]
        public void Sum_WhenCalled_ChangesSum()
        {

            var calculator = CreateCalculator();

            calculator.Add(1);
            var sum = calculator.Sum();

            Assert.AreEqual(1, sum);
        }

        private MemCalculator CreateCalculator()
        {
            return new ();
        }
    }
}
