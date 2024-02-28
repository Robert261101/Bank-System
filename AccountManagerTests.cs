using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TemaTAS.Tests
{
    [TestFixture]
    public class AccountManagerTests
    {
        [Test]
        public void AddAndRemoveAccount()
        {
            // Arrange
            var manager = new AccountManager();
            var account = new Account();

            // Act
            manager.AddAccount(account);
            var accounts = manager.GetAccounts();
            manager.RemoveAccount(account);

            // Assert
            CollectionAssert.Contains(accounts, account);
            CollectionAssert.DoesNotContain(accounts, account);
        }

        [Test]
        public void TransferBetweenAccounts()
        {
            // Arrange
            var manager = new AccountManager();
            var source = new Account();
            var destination = new Account();
            manager.AddAccount(source);
            manager.AddAccount(destination);

            // Act
            source.Deposit(100);
            manager.TransferBetweenAccounts(source, destination, 50);

            // Assert
            Assert.AreEqual(50, source.Balance);
            Assert.AreEqual(50, destination.Balance);
        }
    }
}
