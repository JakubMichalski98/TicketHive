using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public interface IUserRepo
    {
        public Task<UserModel> GetUser(string username);
    }
}
