using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace TemaTAS
{
    public interface ICurrencyConvertor
    {
        decimal ConvertFromEuroToRon(decimal valueInEur);

        decimal ConvertFromRonToEuro(decimal valueInRon);
    }
}
