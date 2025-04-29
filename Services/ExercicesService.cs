using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System.Diagnostics;
using System.Text;
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

        public async Task<Exercice> GetExerciceById(string exerciceId)
        {
            try
            {
                var uri = $"Exercices/GetExerciceById?id={exerciceId}";
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

        public async Task<bool> PostExercice(string name)
        {
            try
            {
                var uri = $"Exercices/PostExercice";

                var jsonContent = JsonSerializer.Serialize(name);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(uri, content);
                var data = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> UpdateExercice(string exerciceId, string name)
        {
            try
            {
                var uri = $"Exercices/PutName?exerciceId={exerciceId}&name={name}";

                //var jsonContent = JsonSerializer.Serialize(name);
                //Debug.WriteLine(jsonContent);
                //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var content = new StringContent("");

                var response = await _httpClient.PutAsync(uri, content);
                var data = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> Delete(string exerciceId)
        {
            try
            {
                var uri = $"Exercices/DeleteExerciceById?id={exerciceId}";

                var response = await _httpClient.DeleteAsync(uri);

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
