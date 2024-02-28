using NUnit.Framework;
using Moq;

namespace TemaTAS.Tests
{
    [TestFixture]
    public class AccountTests
    {
        private Account source;
        private Account destination;
        private Mock<ICurrencyConvertor> mockConverter;

        [SetUp]
        public void Setup()
        {
            source = new Account();
            destination = new Account();
            mockConverter = new Mock<ICurrencyConvertor>();
        }

        [Test]
        [TestCase(200, 0, 10, 4.87)]
        public void TransferFundsFromEuroAmount(decimal initialSourceBalance, decimal initialDestinationBalance, decimal euroAmount, decimal euroToRonRate)
        {
            // Arrange
            source.Deposit(initialSourceBalance);
            destination.Deposit(initialDestinationBalance);

            mockConverter.Setup(x => x.ConvertFromEuroToRon(It.IsAny<decimal>())).Returns((decimal amount) => amount * euroToRonRate);

            // Act
            source.TransferFundsFromEuroAmount(destination, euroAmount, mockConverter.Object);

            // Assert
            decimal ronAmount = euroAmount * euroToRonRate;
            Assert.That(destination.Balance, Is.EqualTo(ronAmount + initialDestinationBalance));
            Assert.That(source.Balance, Is.EqualTo(initialSourceBalance - ronAmount));

            // Verify
            mockConverter.Verify(x => x.ConvertFromEuroToRon(It.IsAny<decimal>()), Times.Once());
        }

        [Test]
        [TestCase(200, 0, 100, 4.87)]
        public void TransferFundsFromEuroAmountInsufficientBalance(decimal initialSourceBalance, decimal initialDestinationBalance, decimal euroAmount, decimal euroToRonRate)
        {
            // Arrange
            source.Deposit(initialSourceBalance);
            destination.Deposit(initialDestinationBalance);

            mockConverter.Setup(x => x.ConvertFromEuroToRon(It.IsAny<decimal>())).Returns((decimal amount) => amount * euroToRonRate);

            // Act and Assert
            Assert.Throws<Account.NotEnoughFundsException>(() => source.TransferFundsFromEuroAmount(destination, euroAmount, mockConverter.Object));

            // Verify
            mockConverter.Verify(x => x.ConvertFromEuroToRon(It.IsAny<decimal>()), Times.Once());
        }
    }
}
