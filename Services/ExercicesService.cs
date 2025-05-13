using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GymProgress.Mobile.Services
{
    public class ExercicesService : IExercicesService
    {
        private readonly HttpClient _httpClient;

        public ExercicesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Exercice>?> GetAllExercice()
        {
            try
            {
                // /exercices/{exerciceId}
                // /exercices/name/{exerciceName}
                var uri = $"Exercices/GetAllExercice";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var exercice = JsonSerializer.Deserialize<List<Exercice>>(data);
                if (exercice != null)
                {
                    return exercice;
                }
                else
                {
                    throw new Exception("L'exercice est null");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<Exercice?> GetExerciceByName(string name)
        {
            try
            {
                var uri = $"Exercices/GetExerciceByName?name={name}";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var exercice = JsonSerializer.Deserialize<Exercice>(data);
                if (exercice != null)
                {
                    return exercice;
                }
                else
                {
                    throw new Exception("L'exercice est null");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<Exercice?> GetExerciceById(string exerciceId)
        {
            try
            {
                var uri = $"Exercices/GetExerciceById?id={exerciceId}";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var exercice = JsonSerializer.Deserialize<Exercice>(data);
                if (exercice != null)
                {
                    return exercice;
                }
                else
                {
                    throw new Exception("L'exercice est null");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<Exercice>?> GetExerciceUserId(string userId)
        {
            try
            {
                var uri = $"Exercices/GetExerciceUserId?userId={userId}";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var exercice = JsonSerializer.Deserialize<List<Exercice>>(data);
                if (exercice != null)
                {
                    return exercice;
                }
                else
                {
                    throw new Exception("L'exercice est null");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<Exercice>?> GetExercicePublic()
        {
            try
            {
                var uri = $"Exercices/GetExercicePublic";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var exercice = JsonSerializer.Deserialize<List<Exercice>>(data);
                if (exercice != null)
                {
                    return exercice;
                }
                else
                {
                    throw new Exception("L'exercice est null");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<Exercice>?> GetExercicesBySeanceId(string seanceId)
        {
            try
            {
                var uri = $"Exercices/GetExercicesBySeanceId?seanceId={seanceId}";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var exercice = JsonSerializer.Deserialize<List<Exercice>>(data);
                if (exercice != null)
                {
                    return exercice;
                }
                else
                {
                    throw new Exception("L'exercice est null");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> PostExercice(Exercice exercice)
        {
            try
            {
                // POST /exercices
                var uri = $"Exercices/PostExercice";

                var jsonOptions = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var jsonContent = JsonSerializer.Serialize(exercice, jsonOptions);
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
                // PUT  /exercices/{exerciceId}
                // BODY { "name": "newName" }
                var uri = $"Exercices/PutName?exerciceId={exerciceId}&name={name}";

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
                // DELETE  /exercices/{exerciceId}
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
