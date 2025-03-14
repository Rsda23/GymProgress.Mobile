using GymProgress.Domain.Models;

namespace GymProgress.Mobile.Interfaces
{
    public interface IUsersService
    {
        public Task<User> GetUserByEmail(string email);
    }
}
