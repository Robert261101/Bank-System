using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace TemaTAS
{
    public class CurrencyConvertorStub : ICurrencyConvertor
    {
        private decimal euroToRonRate;

        public CurrencyConvertorStub(decimal euroToRonRate)
        {
            this.euroToRonRate = euroToRonRate;
        }

        public decimal ConvertFromEuroToRon(decimal valueInEur)
        {
            return valueInEur * euroToRonRate;
        }

        public decimal ConvertFromRonToEuro(decimal valueInRon)
        {
            if (euroToRonRate == 0)
            {
                throw new InvalidOperationException("Conversion rate cannot be zero.");
            }

            return valueInRon / euroToRonRate;
        }
    }
}
