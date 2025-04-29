using GymProgress.Domain.Models;

namespace GymProgress.Mobile.Interfaces
{
    public interface IUsersService
    {
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserById(string userId);
        public Task<bool> Delete(string userId);
    }
}
