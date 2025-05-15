using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace GymProgress.Mobile.Services
{
    public class SeancesService : ISeancesService
    {
        private readonly HttpClient _httpClient;

        public SeancesService(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }

        public async Task<Seance?> GetSeanceByName(string name)
        {
            try
            {
                var uri = $"GetSeanceByName?name={name}";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var seance = JsonSerializer.Deserialize<Seance>(data);

                if (seance != null)
                {
                    return seance;
                }
                else
                {
                    throw new Exception("la seance est nulle");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        public async Task<Seance?> GetSeanceById(string id)
        {
            try
            {
                var uri = $"GetSeanceById?id={id}";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var seance = JsonSerializer.Deserialize<Seance>(data);

                if (seance != null)
                {
                    return seance;
                }
                else
                {
                    throw new Exception("la seance est nulle");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        public async Task<List<Seance>?> GetAllSeance()
        {
            try
            {
                var uri = $"GetAllSeance";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var seance = JsonSerializer.Deserialize<List<Seance>>(data);
                
                if (seance != null)
                {
                    return seance;
                }
                else
                {
                    throw new Exception("la seance est nulle");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        public async Task<List<Seance>?> GetSeanceByUserId(string userId)
        {
            try
            {
                var uri = $"GetSeanceByUserId?userId={userId}";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var seance = JsonSerializer.Deserialize<List<Seance>>(data);

                if (seance != null)
                {
                    return seance;
                }
                else
                {
                    throw new Exception("la seance est nulle");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        public async Task<List<Seance>?> GetSeancePublic()
        {
            try
            {
                var uri = $"GetSeancePublic";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var seance = JsonSerializer.Deserialize<List<Seance>>(data);

                if (seance != null)
                {
                    return seance;
                }
                else
                {
                    throw new Exception("la seance est nulle");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        public async Task<List<Seance>?> GetLastSeance(int count, string userId)
        {
            try
            {
                var uri = $"GetLastSeance?count={count}&userId={userId}";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var seance = JsonSerializer.Deserialize<List<Seance>>(data);

                if (seance != null)
                {
                    return seance;
                }
                else
                {
                    throw new Exception("la seance est nulle");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> PostSeance(Seance seance)
        {
            try
            {
                var uri = $"PostSeance";

                var jsonContent = JsonSerializer.Serialize(seance);
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

        public async Task<bool> AddExerciceToSeanceById(string seanceId, List<string> execicesId)
        {
            try
            {
                var uri = $"PostExerciceToSeanceById?seanceId={seanceId}";

                var jsonContent = JsonSerializer.Serialize(execicesId);
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

        public async Task<bool> Delete(string seanceId)
        {
            try
            {
                var uri = $"DeleteSeanceById?id={seanceId}";

                var response = await _httpClient.DeleteAsync(uri);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> RemoveExerciceToSeance(string seanceId, string exerciceId)
        {
            try
            {
                var uri = $"RemoveExerciceToSeance?seanceId={seanceId}&exerciceId={exerciceId}";

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
