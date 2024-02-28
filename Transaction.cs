using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaTAS
{
    public class Transaction
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Transaction(decimal amount)
        {
            Amount = amount;
            Date = DateTime.Now;
        }
    }
}
