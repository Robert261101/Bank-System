using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TemaTAS.Tests
{
    [TestFixture]
    public class AccountWithHistoryTests
    {
        [Test]
        public void AddTransactionToHistory()
        {
            // Arrange
            var account = new AccountWithHistory();
            var transaction = new Transaction(50);

            // Act
            account.AddTransactionToHistory(transaction);

            // Assert
            CollectionAssert.Contains(account.GetTransactionHistory(), transaction);
        }
    }
}
