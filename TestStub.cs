using NUnit.Framework;

namespace TemaTAS.Tests
{
    [TestFixture]
    internal class TestStub
    {
        private Account source;
        private Account destination;

        [SetUp]
        public void Setup()
        {
            // Arrange
            source = new Account();
            source.Deposit(200.00M);
            destination = new Account();
            destination.Deposit(150.00M);
        }

        [Test]
        [TestCase(200, 0, 10, 4.87)]
        [TestCase(211, 150, 50, 4)]
        public void TransferFundsFromEuroAmount(decimal initialSourceBalance, decimal initialDestinationBalance, decimal euroAmount, decimal euroToRonRate)
        {
            // Arrange
            source.Deposit(initialSourceBalance);
            destination.Deposit(initialDestinationBalance);
            CurrencyConvertorStub converter = new CurrencyConvertorStub(euroToRonRate);

            // Act
            source.TransferFundsFromEuroAmount(destination, euroAmount, converter);

            // Assert
            decimal ronAmount = euroAmount * euroToRonRate;
            Assert.That(destination.Balance, Is.EqualTo(initialDestinationBalance + ronAmount));
            Assert.That(source.Balance, Is.EqualTo(initialSourceBalance - ronAmount));
        }

        [Test]
        [TestCase(200, 30, 100, 4.87)]
        [TestCase(210, 150, 50, 4)]
        public void TransferFundsFromEuroAmountFail(decimal initialSourceBalance, decimal initialDestinationBalance, decimal euroAmount, decimal euroToRonRate)
        {
            // Arrange
            source.Deposit(initialSourceBalance);
            destination.Deposit(initialDestinationBalance);
            CurrencyConvertorStub converter = new CurrencyConvertorStub(euroToRonRate);

            // Act and Assert
            Assert.Throws<Account.NotEnoughFundsException>(() => source.TransferFundsFromEuroAmount(destination, euroAmount, converter));
            Assert.That(destination.Balance, Is.EqualTo(initialDestinationBalance));
            Assert.That(source.Balance, Is.EqualTo(initialSourceBalance));
        }
    }
}
