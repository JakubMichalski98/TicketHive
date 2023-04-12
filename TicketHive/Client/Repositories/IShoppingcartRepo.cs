using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public interface IShoppingcartRepo
    {
        public Task<List<BookingModel>> GetCartFromLocalStorage();

        public Task RemoveFromCart(int eventId);
    }
}
