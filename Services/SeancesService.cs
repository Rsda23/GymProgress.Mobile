using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System.Diagnostics;
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

        public async Task<Seance> GetSeanceByName(string name)
        {
            try
            {
                var uri = $"GetSeanceByName?name={name}";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var seance = JsonSerializer.Deserialize<Seance>(data);
                return seance;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
            
            //var catalog = JsonConvert.DeserializeObject<Catalog>(responseString);
            //return catalog;
            //return new Seance("demo", new List<Exercice>());
        }

    }
}
