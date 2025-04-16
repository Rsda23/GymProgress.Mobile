using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymProgress.Mobile.Services
{
    public class SetDataService : ISetDatasService
    {
        private readonly HttpClient _httpClient;

        public SetDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SetData> GetSetDataByExericceId(string exerciceId)
        {
            try
            {
                var uri = $"SetDatas/GetSetDataByExerciceId?exerciceId={exerciceId}";
                var response = await _httpClient.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var setDatas = JsonSerializer.Deserialize<SetData>(data);
                return setDatas;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
