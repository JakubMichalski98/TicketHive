using TicketHive.Shared;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public interface IUserRepo
    {
        public Task<UserModel> GetUser(string username);

        public Task AddBookingToUser(BookingInfo bookingInfo);

        public Task<bool> ChangeUserPassword(ChangePasswordModel changePasswordModel);    
    }
}
