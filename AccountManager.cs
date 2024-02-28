using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaTAS
{
    public class AccountManager
    {
        private List<Account> accounts;

        public AccountManager()
        {
            accounts = new List<Account>();
        }

        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }

        public void RemoveAccount(Account account)
        {
            accounts.Remove(account);
        }

        public IEnumerable<Account> GetAccounts()
        {
            return accounts;
        }

        public void TransferBetweenAccounts(Account source, Account destination, decimal amount)
        {
            source.Withdraw(amount);
            destination.Deposit(amount);
        }
    }
}
