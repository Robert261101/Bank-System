using System;
using System.Collections.Generic;

namespace TemaTAS
{
    public class TransactionHistory
    {
        private readonly List<Transaction> transactions;

        public TransactionHistory()
        {
            transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public IEnumerable<Transaction> GetTransactionHistory()
        {
            return transactions;
        }
    }
}
