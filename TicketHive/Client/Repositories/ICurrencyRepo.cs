namespace TicketHive.Client.Repositories
{
    public interface ICurrencyRepo
    {
        public Task GetExchangeRates();
        public double GetExchangeRate();
        public Task SetExchangeRate();
    }
}
