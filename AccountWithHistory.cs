using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaTAS
{
    public class AccountWithHistory : Account
    {
        private TransactionHistory transactionHistory;

        public AccountWithHistory()
        {
            transactionHistory = new TransactionHistory();
        }

        public void AddTransactionToHistory(Transaction transaction)
        {
            transactionHistory.AddTransaction(transaction);
        }

        public IEnumerable<Transaction> GetTransactionHistory()
        {
            return transactionHistory.GetTransactionHistory();
        }
    }
}
