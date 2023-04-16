namespace TicketHive.Client.Repositories
{
    public interface ICurrencyRepo
    {
        public Task GetExchangeRates();
        public Task SetExchangeRate();

        public string GetCurrencyCode();
    }
}
