using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace GymProgress.Mobile.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;

        public UsersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                //var uri = $"GetUserByEmail?email={email}";
                var fullUri = $"https://gymprogress-adezdfctcsdjhegr.francecentral-01.azurewebsites.net/GetUserByEmail?email={Uri.EscapeDataString(email)}";
                var response = await _httpClient.GetAsync(fullUri);
                var data = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<User>(data);
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> PostUser(User user)
        {
            try
            {
                var fullUri = $"https://gymprogress-adezdfctcsdjhegr.francecentral-01.azurewebsites.net/PostUser";
                
                var jsonContent = JsonSerializer.Serialize(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(fullUri, content);
                var data = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}
