namespace TicketHive.Client.Repositories
{
    public interface ICurrencyRepo
    {
        public Task GetExchangeRates();
        public double GetExchangeRate();
    }
}
