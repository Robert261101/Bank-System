using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaTAS;

namespace TemaTAS.Tests
{
    [TestFixture]
    public class TransactionHistoryTests
    {
        [Test]
        public void AddTransactionToHistory()
        {
            // Arrange
            var history = new TransactionHistory();
            var transaction = new Transaction(50);

            // Act
            history.AddTransaction(transaction);

            // Assert
            CollectionAssert.Contains(history.GetTransactionHistory(), transaction);
        }
    }
}