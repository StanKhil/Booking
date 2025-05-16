using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace Booking.Services.CurrencyRate
{
    public class NbuCurrencyRate : ICurrencyRate
    {
        public async Task<double> GetCurrencyRateAsync()
        {
            using HttpClient client = new();
            String json = await client.GetStringAsync($"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");

            var nbuRates = JsonSerializer.Deserialize<List<NbuRate>>(json)!;

            foreach (var rate in nbuRates)
            {
                if (rate.cc == "EUR")
                {
                    return rate.rate;
                }
            }
            return 0;
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
