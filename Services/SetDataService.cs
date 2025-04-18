﻿using GymProgress.Domain.Models;
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

        public async Task<bool> PostSetData(SetData setData)
        {
            try
            {
                var uri = $"SetDatas/PostFullSetData";

                var jsonContent = JsonSerializer.Serialize(setData);
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

        public async Task<bool> ReplaceSetData(SetData setData)
        {
            try
            {
                var uri = $"SetDatas/ReplaceSetData";

                var jsonContent = JsonSerializer.Serialize(setData);
                Debug.WriteLine(jsonContent);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

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
    }
}
