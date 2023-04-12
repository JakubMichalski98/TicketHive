using TicketHive.Shared.Models;
using Blazored.LocalStorage;
using Newtonsoft.Json;

namespace TicketHive.Client.Repositories
{
    public class ShoppingcartRepo : IShoppingcartRepo
    {
        private readonly ILocalStorageService localStorage;

        public ShoppingcartRepo(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task<List<BookingModel>> GetCartFromLocalStorage()
        {
            string? jsonString = await localStorage.GetItemAsync<string>("cart");

            if (jsonString != null)
            {
                return JsonConvert.DeserializeObject<List<BookingModel>>(jsonString);
            }
            return null;
        }

        public async Task RemoveFromCart(int eventId)
        {

            List<BookingModel>? localStorageBookings = await GetCartFromLocalStorage();

            localStorageBookings.RemoveAll(b => b.EventModelId == eventId);

            string updatedBookingsJson = JsonConvert.SerializeObject(localStorageBookings);

            await localStorage.SetItemAsync("cart", updatedBookingsJson);

        }
    }
}
