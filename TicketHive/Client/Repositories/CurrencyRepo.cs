using Newtonsoft.Json;
using System.Net.Http;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public class CurrencyRepo : ICurrencyRepo
    {
        private readonly HttpClient httpClient;

        private double currencyRate;
        private string currencyCode = "EUR";
        private string accessKey = "LhRJ2zFT23P5DZ8Vg2xfNQ1nXCA6RmoI";

        public CurrencyRepo(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task GetExchangeRate()
        {
            httpClient.DefaultRequestHeaders.Add("apikey", accessKey);

            var response = await httpClient.GetAsync($"https://api.apilayer.com/exchangerates_data/latest?symbols={currencyCode}&base=SEK");
            var content = response.Content;

            var stringResponse = await content.ReadAsStringAsync();

            Root currency = JsonConvert.DeserializeObject<Root>(stringResponse);

            if (currencyCode == "EUR")
            {
                currencyRate = currency.rates.EUR;
            }
            else if (currencyCode == "GBP")
            {
                currencyRate = currency.rates.GBP;
            }
        }
        public async Task<string> GetCurrencyForCountry(string country)
        {
            if (country == "England" || country == "Ireland" || country == "Northern_Ireland" || country == "Scotland")
            {
                return "GBP";
            }
            else if (country == "Sweden")
            {
                return "SEK";
            }
            else
            {
                return "EUR";
            }
        }
    }
}
