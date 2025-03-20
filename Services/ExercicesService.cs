using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System.Diagnostics;
using System.Text.Json;

namespace GymProgress.Mobile.Services
{
    public class ExercicesService : IExercicesService
    {
        private readonly HttpClient _httpClient;

        public ExercicesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Exercice>> GetAllExercice()
        {
            try
            {
                var uri = $"Exercices/GetAllExercice";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var exercice = JsonSerializer.Deserialize<List<Exercice>>(data);
                return exercice;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<Exercice> GetExerciceByName(string name)
        {
            try
            {
                var uri = $"Exercices/GetExerciceByName?name={name}";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var exercice = JsonSerializer.Deserialize<Exercice>(data);
                return exercice;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
