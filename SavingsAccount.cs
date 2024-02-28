using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaTAS
{
    public class SavingsAccount : Account
    {
        private decimal interestRate;

        public SavingsAccount(decimal interestRate)
        {
            if (interestRate < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(interestRate), "Interest rate cannot be negative.");
            }
            this.interestRate = interestRate;
        }

        public void CalculateInterest()
        {
            decimal interest = Balance * interestRate;
            Deposit(interest);
        }
        

    }
}
