using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public class CurrencyRepo : ICurrencyRepo
    {
        private readonly HttpClient httpClient;
        private readonly IUserRepo userRepo;
        public static double exchangeRate;
        private string currencyCode = "";
        private string accessKey = "a5yaLFgVm9E0f87EOma66SE1S5tvNRxS";
        private Root? currency;
        private UserModel user;
       

        public CurrencyRepo(HttpClient httpClient, IUserRepo userRepo)
        {
            this.httpClient = httpClient;
            this.userRepo = userRepo;
        }

        /// <summary>
        /// Sends GET request to exchangerates api and sets exchange rate if it has not been set (is lower than 0.001) before
        /// </summary>
        /// <returns></returns>
        public async Task GetExchangeRates()
        {
            httpClient.DefaultRequestHeaders.Add("apikey", accessKey);

            if (exchangeRate < 0.001)
            {
                var response = await httpClient.GetAsync($"https://api.apilayer.com/exchangerates_data/latest?symbols=EUR,GBP&base=SEK");
                var content = response.Content;

                var stringResponse = await content.ReadAsStringAsync();

                currency = JsonConvert.DeserializeObject<Root>(stringResponse);

                await SetExchangeRate();
            }
        }

        /// <summary>
        /// Sets the exchange rate based on user's currency and sends a post request to the event controller in order to change exchangerate on server
        /// </summary>
        /// <returns></returns>
        public async Task SetExchangeRate()
        {
            user = await userRepo.GetLoggedInUser();

            if (user != null)
            {
                if (user.Currency == "€")
                {
                    exchangeRate = currency.rates.EUR;
                    currencyCode = "€";

                }
                else if (user.Currency == "£")
                {
                    exchangeRate = currency.rates.GBP;
                    currencyCode = "£";
                }
                else if (user.Currency == "SEK")
                {
                    exchangeRate = 1;
                    currencyCode = "SEK";
                }
            }
            else
            {
                exchangeRate = 1;
            }

            
            await httpClient.PostAsJsonAsync("api/Events/exchangerate", exchangeRate);
        }

        /// <summary>
        /// Returns currency code
        /// </summary>
        /// <returns></returns>
        public string GetCurrencyCode()
        {
            return currencyCode;
        }

    }
}
