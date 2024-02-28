using System;
using System.Transactions;

namespace TemaTAS
{
    public class Account
    {
        private decimal balance;
        private decimal minBalance = 10;
        private List<Transaction> transactions;
        private readonly TransactionHistory transactionHistory;

        // Adaugă o proprietate publică pentru istoricul tranzacțiilor
        public TransactionHistory TransactionHistory => transactionHistory;

        // În constructorul implicit și cel cu parametri, inițializează istoricul tranzacțiilor
        public Account()
        {
            balance = 0;
            transactions = new List<Transaction>();
            transactionHistory = new TransactionHistory();
        }

        public Account(decimal value)
        {
            balance = value;
            transactions = new List<Transaction>();
            transactionHistory = new TransactionHistory();
        }

        // Modifică metodele Deposit și Withdraw pentru a adăuga tranzacții în istoric
        public void Deposit(decimal amount)
        {
            if (amount >= 0)
            {
                balance += amount;
                transactions.Add(new Transaction(amount));
                transactionHistory.AddTransaction(new Transaction(amount));
            }
            else
            {
                throw new NotEnoughFundsException();
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount >= 0)
            {
                if (balance - amount >= minBalance)
                {
                    balance -= amount;
                    transactions.Add(new Transaction(-amount));
                    transactionHistory.AddTransaction(new Transaction(-amount));
                }
                else if (balance - amount >= 0 && balance - amount < minBalance)
                {
                    decimal fee = 5;
                    balance -= (amount + fee);
                    transactions.Add(new Transaction(-amount));
                    transactions.Add(new Transaction(-fee));
                    transactionHistory.AddTransaction(new Transaction(-amount));
                    transactionHistory.AddTransaction(new Transaction(-fee));
                }
                else
                {
                    throw new NotEnoughFundsException();
                }
            }
            else
            {
                throw new NotEnoughFundsException();
            }
        }


        public void TransferFunds(Account destination, decimal amount)
        {
            destination.Deposit(amount);
            Withdraw(amount);
        }

        public Account TransferMinFunds(Account destination, decimal amount)
        {
            if ((amount > 0) && ((balance - amount) >= minBalance))
            {
                destination.Deposit(amount);
                Withdraw(amount);
            }
            else throw new NotEnoughFundsException();

            return destination;
        }

        public void TransferFundsFromEuroAmount(Account destination, decimal amount, ICurrencyConvertor euroRate)
        {
            decimal amountInRon = euroRate.ConvertFromEuroToRon(amount);
            if (balance - amountInRon > minBalance && amount >= 0)
            {
                destination.Deposit(amountInRon);
                Withdraw(amountInRon);
            }
            else throw new NotEnoughFundsException();
        }
        public decimal GetTotalTransactionAmount()
        {
            return transactions.Sum(t => t.Amount);
        }

        public void TransferFundsToValidDestination(Account destination, decimal amount)
        {
            if (destination != null)
            {
                destination.Deposit(amount);
                Withdraw(amount);
            }
            else
            {
                throw new ArgumentNullException(nameof(destination), "Destination account cannot be null.");
            }
        }

        public void TransferFundsWithFee(Account destination, decimal amount, decimal fee)
        {
            decimal totalAmount = amount + fee;
            if (balance >= totalAmount)
            {
                destination.Deposit(amount);
                Withdraw(totalAmount);
            }
            else
            {
                throw new NotEnoughFundsException();
            }
        }


        public decimal Balance => balance;

        public decimal MinBalance => minBalance;

        public class NotEnoughFundsException : ApplicationException
        {
        }
    }
}
