using Microsoft.AspNetCore.Components.Authorization;
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
        private readonly AuthenticationStateProvider authStateProvider;

        public UserRepo(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
        {
            this.httpClient = httpClient;
            this.authStateProvider = authStateProvider;
        }

        /// <summary>
        /// Returns user with provided username from the EventDb database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns user in EventDb database that corresponds to the logged in user from UsersDb database
        /// </summary>
        /// <returns></returns>
        public async Task<UserModel> GetLoggedInUser()
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                return await GetUser(user.Identity.Name);
            }
            return null;
        }

        /// <summary>
        /// Gets logged in user and checks whether user already has a booking with event contained in provided booking
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfUserHasBooking(BookingModel booking)
        {
            var user = await GetLoggedInUser();

            foreach(var item in user.Bookings)
            {
                if (item.EventModelId == booking.EventModelId)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Sends a POST request to the controller with provided BookingInfoModel
        /// </summary>
        /// <param name="bookingInfo"></param>
        /// <returns></returns>
        public async Task AddBookingToUser(BookingInfoModel bookingInfo)
        {
            var result = await httpClient.PostAsJsonAsync("api/Users", bookingInfo);
        }

        /// <summary>
        /// Sends a PUT request to the controller with provided BookingInfoModel
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        public async Task<bool> ChangeUserPassword(ChangePasswordModel changePasswordModel)
        {
            var response = await httpClient.PutAsJsonAsync($"api/Users", changePasswordModel);

            if (response.IsSuccessStatusCode) 
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sends a PUT request to the controller with provided ChangeUserCountryModel
        /// </summary>
        /// <param name="changeUserCountryModel"></param>
        /// <returns></returns>
        public async Task<bool> ChangeUserCountry(ChangeUserCountryModel changeUserCountryModel)
        {
            var response = await httpClient.PutAsJsonAsync($"api/Users/country", changeUserCountryModel);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sends PUT request to the controller in order to set user currency
        /// </summary>
        /// <returns></returns>
        public async Task SetUserCurrency()
        {
            var user = await GetLoggedInUser();

            var response = await httpClient.PutAsJsonAsync($"api/Users/currency", user.Username);
        }
    }
}
