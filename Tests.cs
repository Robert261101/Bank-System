using System;
using System.Collections.Generic;
using System.Text;
using static TemaTAS.Account;

using NUnit.Framework;

namespace TemaTAS.Tests
{
    [TestFixture]
    public class Tests
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
        [TestCase(200, 50)]
        [TestCase(200, 0)]
        public void Deposit(decimal initialDeposit, decimal depositAmount)
        {
            // Arrange
            source.Deposit(initialDeposit);

            // Act 
            source.Deposit(depositAmount);

            // Assert
            Assert.That(source.Balance, Is.EqualTo(initialDeposit + depositAmount));
        }

        [Test]
        [TestCase(200, 70)]
        [TestCase(200, 0)]
        public void Withdraw(decimal initialDeposit, decimal withdrawAmount)
        {
            // Arrange
            source.Deposit(initialDeposit);

            // Act 
            source.Withdraw(withdrawAmount);

            // Assert
            Assert.That(source.Balance, Is.EqualTo(initialDeposit - withdrawAmount));
        }

        [Test]
        [TestCase(200, 0, 78)]
        [TestCase(200, 2, 189)]
        [TestCase(200, 10, 1)]
        public void TransferMinFunds(decimal initialSourceBalance, decimal initialDestinationBalance, decimal transferAmount)
        {
            // Arrange
            source.Deposit(initialSourceBalance);
            destination.Deposit(initialDestinationBalance);
            var InitialDestinationBalance = destination.Balance;

            // Act
            source.TransferMinFunds(destination, transferAmount);

            // Assert
            Assert.That(destination.Balance, Is.EqualTo(transferAmount + InitialDestinationBalance));
            Assert.That(source.Balance, Is.EqualTo(initialSourceBalance - transferAmount));
        }

        [Test]
        [TestCase(200, 50, 191)] // Trying to transfer more than the initial source balance
        [TestCase(200, 0, 0)]    // Trying to transfer with an amount of 0
        public void TransferMinFundsNegative(decimal initialSourceBalance, decimal initialDestinationBalance, decimal transferAmount)
        {
            // Arrange
            source.Deposit(initialSourceBalance);
            destination.Deposit(initialDestinationBalance);
            var initialDestinationBalanceCopy = destination.Balance;

            // Act and Assert
            Assert.Throws<Account.NotEnoughFundsException>(() => source.TransferMinFunds(destination, transferAmount));
            Assert.That(destination.Balance, Is.EqualTo(initialDestinationBalanceCopy));
            Assert.That(source.Balance, Is.EqualTo(initialSourceBalance));
        }
        [Test]
        public void TotalTransactionAmount()
        {
            // Arrange
            source.Deposit(100);
            source.Withdraw(50);

            // Act
            decimal totalAmount = source.GetTotalTransactionAmount();

            // Assert
            Assert.AreEqual(50, totalAmount);
        }

        [Test]
        public void TransferToValidDestination()
        {
            // Arrange
            Account validDestination = new Account();

            // Act
            source.TransferFundsToValidDestination(validDestination, 50);

            // Assert
            Assert.AreEqual(50, validDestination.Balance);
            Assert.AreEqual(150, source.Balance);
        }

        [Test]
        public void CalculateInterest()
        {
            // Arrange
            SavingsAccount savingsAccount = new SavingsAccount(0.05m);
            savingsAccount.Deposit(1000);

            // Act
            savingsAccount.CalculateInterest();

            // Assert
            Assert.AreEqual(1050, savingsAccount.Balance);
        }

        [Test]
        public void TransferFundsWithFee()
        {
            // Arrange
            source.Deposit(200);
            destination.Deposit(100);

            // Act
            source.TransferFundsWithFee(destination, 50, 5);

            // Assert
            Assert.AreEqual(45, source.Balance);
            Assert.AreEqual(150, destination.Balance);
        }

        [Test]
        public void NegativeInterestRate()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new SavingsAccount(-0.01m));
        }

        [Test]
        [TestCase(200, 50, 150)] // Retragere cu lichiditate suficientă
        [TestCase(200, 250, 0)]   // Retragere cu lichiditate insuficientă
        public void WithdrawWithSufficientLiquidity(decimal initialBalance, decimal withdrawAmount, decimal expectedBalance)
        {
            // Arrange
            source.Deposit(initialBalance);

            // Act
            source.Withdraw(withdrawAmount);

            // Assert
            Assert.AreEqual(expectedBalance, source.Balance);
        }

        [Test]
        [TestCase(200, 50, 145)]   // Retragere sub limita minimă cu taxă
        [TestCase(200, 250, 0)]     // Retragere cu lichiditate insuficientă
        public void WithdrawBelowMinLiquidityWithFee(decimal initialBalance, decimal withdrawAmount, decimal expectedBalance)
        {
            // Arrange
            source.Deposit(initialBalance);

            // Act
            source.Withdraw(withdrawAmount);

            // Assert
            Assert.AreEqual(expectedBalance, source.Balance);
        }

        // Adaugă un test pentru verificarea istoricului tranzacțiilor
        [Test]
        public void TransactionHistoryCheck()
        {
            // Arrange
            source.Deposit(100);
            source.Withdraw(50);

            // Act
            var history = source.TransactionHistory.GetTransactionHistory();

            // Assert
            Assert.AreEqual(2, history.Count());
        }


    }
}

