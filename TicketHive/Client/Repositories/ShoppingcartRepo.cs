using TicketHive.Shared.Models;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using TicketHive.Client.Pages;

namespace TicketHive.Client.Repositories
{
    public class ShoppingcartRepo : IShoppingcartRepo
    {
        private readonly ILocalStorageService localStorage;

        public ShoppingcartRepo(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task CreateCart(List<BookingModel> bookings)
        {
            string bookingsJson = JsonConvert.SerializeObject(bookings);

            if (!await localStorage.ContainKeyAsync("cart"))
            {
                await localStorage.SetItemAsync("cart", bookingsJson);
            }
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

        public async Task<bool> CheckIfItemExists(BookingModel booking)
        {
            List<BookingModel> bookings = await GetCartFromLocalStorage();

            foreach (var item in bookings)
            {
                if (item.EventModelId == booking.EventModelId)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task AddToCart(BookingModel booking)
        {
            string bookingsJson = await localStorage.GetItemAsync<string>("cart");

            List<BookingModel>? localStorageBookings = JsonConvert.DeserializeObject<List<BookingModel>>(bookingsJson);

            localStorageBookings.Add(booking);
            string updatedBookingsJson = JsonConvert.SerializeObject(localStorageBookings);

            await localStorage.SetItemAsync("cart", updatedBookingsJson);
        }

        public async Task RemoveFromCart(int eventId)
        {

            List<BookingModel>? localStorageBookings = await GetCartFromLocalStorage();

            localStorageBookings.RemoveAll(b => b.EventModelId == eventId);

            string updatedBookingsJson = JsonConvert.SerializeObject(localStorageBookings);

            await localStorage.SetItemAsync("cart", updatedBookingsJson);

        }

        public async Task RemoveCart()
        {
            await localStorage.RemoveItemAsync("cart");
        }
    }
}
