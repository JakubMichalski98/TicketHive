namespace TicketHive.Client.Repositories
{
    public interface ICurrencyRepo
    {
        public Task GetExchangeRates();
        public Task<string> GetCurrencyForCountry(string country);
    }
}
