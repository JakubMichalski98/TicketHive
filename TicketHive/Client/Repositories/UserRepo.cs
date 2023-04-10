using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using TicketHive.Shared;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly HttpClient httpClient;

        public UserRepo(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<UserModel> GetUser(string username)
        {
            var response = await httpClient.GetAsync($"api/Users/{username}");

            if (response.IsSuccessStatusCode)
            {
                var foundUser = await response.Content.ReadFromJsonAsync<UserModel>();
                return foundUser;
            }
            return null;
        }

        public async Task AddBookingToUser(BookingInfo bookingInfo)
        {
            var result = await httpClient.PostAsJsonAsync("api/Users", bookingInfo);
        }
    }
}
