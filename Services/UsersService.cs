﻿using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System.Diagnostics;
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
                var uri = $"GetUserByEmail?email={email}";
                var response = await _httpClient.GetAsync(uri);
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
    }
}
