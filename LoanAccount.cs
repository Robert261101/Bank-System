using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaTAS
{
    public class LoanAccount : Account
    {
        private decimal loanInterestRate;

        public LoanAccount(decimal loanInterestRate)
        {
            if (loanInterestRate < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(loanInterestRate), "Loan interest rate cannot be negative.");
            }
            this.loanInterestRate = loanInterestRate;
        }

        public void CalculateLoanInterest()
        {
            decimal interest = Balance * loanInterestRate;
            Deposit(interest);
        }
    }
}
