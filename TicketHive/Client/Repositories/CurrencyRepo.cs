﻿using Newtonsoft.Json;
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
        private string accessKey = "LhRJ2zFT23P5DZ8Vg2xfNQ1nXCA6RmoI";
        private Root? currency;
       

        public CurrencyRepo(HttpClient httpClient, IUserRepo userRepo)
        {
            this.httpClient = httpClient;
            this.userRepo = userRepo;
        }
        public async Task GetExchangeRates()
        {
            httpClient.DefaultRequestHeaders.Add("apikey", accessKey);

            await SetCurrencyCode();

           var response = await httpClient.GetAsync($"https://api.apilayer.com/exchangerates_data/latest?symbols=EUR,GBP&base=SEK");
            var content = response.Content;

            var stringResponse = await content.ReadAsStringAsync();

            currency = JsonConvert.DeserializeObject<Root>(stringResponse);

            await SetExchangeRate();
        }
        private async Task SetCurrencyCode()
        {
            UserModel? user = await userRepo.GetLoggedInUser();

            currencyCode = user.Currency;
        }

        public async Task SetExchangeRate()
        {
            await SetCurrencyCode();

            if (currencyCode == "€")
            {
                exchangeRate = currency.rates.EUR;
                
            }
            else if (currencyCode == "£")
            {
                exchangeRate = currency.rates.GBP;
            }
            else if (currencyCode == "SEK")
            {
                exchangeRate = 1;
            }
            var result = await httpClient.PostAsJsonAsync("api/Events/exchangerate", exchangeRate);
        }

        public double GetExchangeRate()
        {
            return exchangeRate;
        }

        public string GetCurrencyCode()
        {
            return currencyCode;
        }

    }
}
