namespace TicketHive.Client.Repositories
{
    public interface ICurrencyRepo
    {
        public Task GetExchangeRate();
        public Task<string> GetCurrencyForCountry(string country);
    }
}
