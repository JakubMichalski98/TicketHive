﻿using Microsoft.AspNetCore.Components.Authorization;
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
        private readonly ICurrencyRepo currencyRepo;

        public UserRepo(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ICurrencyRepo currencyRepo)
        {
            this.httpClient = httpClient;
            this.authStateProvider = authStateProvider;
            this.currencyRepo = currencyRepo;
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

        public async Task<bool> CheckIfBookingExists(BookingModel booking)
        {
            var user = await GetLoggedInUser();

            foreach(var item in user.Bookings)
            {
                if (item.EventModelId == booking.EventModelId)
                    return true;
            }
            return false;
        }

        public async Task AddBookingToUser(BookingInfoModel bookingInfo)
        {
            var result = await httpClient.PostAsJsonAsync("api/Users", bookingInfo);
        }

        public async Task<bool> ChangeUserPassword(ChangePasswordModel changePasswordModel)
        {
            var response = await httpClient.PutAsJsonAsync($"api/Users", changePasswordModel);

            if (response.IsSuccessStatusCode) 
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ChangeUserCountry(ChangeUserCountryModel changeUserCountryModel)
        {
            var response = await httpClient.PutAsJsonAsync($"api/Users/country", changeUserCountryModel);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task SetUserCurrency()
        {
            var user = await GetLoggedInUser();

            var response = await httpClient.PutAsJsonAsync($"api/Users/currency", user.Username);
        }
    }
}
