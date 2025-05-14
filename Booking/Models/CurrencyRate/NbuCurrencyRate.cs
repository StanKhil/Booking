using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace App.Services.CurrencyRate
{
    public class NbuCurrencyRate : ICurrencyRate
    {
        public async Task<List<CurrencyRate>> GetCurrencyRatesAsync()
        {
            using HttpClient client = new();
            String json = await client.GetStringAsync($"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");

            var nbuRates = JsonSerializer.Deserialize<List<NbuRate>>(json)!;

            return nbuRates
                .Select(nbuRates => new CurrencyRate
                {
                    FullName = nbuRates.txt,
                    ShortName = nbuRates.cc,
                    RateBuy = nbuRates.rate,
                    RateSell = nbuRates.rate,
                    Date = DateOnly.Parse(nbuRates.exchangedate)
                }).ToList();
        }

        private class NbuRate
        {
            public int r030 { get; set; }
            public string txt { get; set; } = null!;
            public double rate { get; set; }
            public string cc { get; set; } = null!;
            public string exchangedate { get; set; } = null!;
        }
    }
}
