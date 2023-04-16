using TicketHive.Shared;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public interface IUserRepo
    {
        public Task<UserModel> GetUser(string username);

        public Task<UserModel> GetLoggedInUser();

        public Task<bool> CheckIfUserHasBooking(BookingModel booking);

        public Task AddBookingToUser(BookingInfoModel bookingInfo);

        public Task<bool> ChangeUserPassword(ChangePasswordModel changePasswordModel);

        public Task<bool> ChangeUserCountry(ChangeUserCountryModel changeUserCountryModel);

        public Task SetUserCurrency();
    }
}
