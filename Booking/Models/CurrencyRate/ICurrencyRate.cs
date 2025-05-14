using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.CurrencyRate
{
    public interface ICurrencyRate
    {
        Task<List<CurrencyRate>> GetCurrencyRatesAsync();
    }
}
