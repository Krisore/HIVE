﻿using System.Net.Http.Json;
using HIVE.Client.Services.Interface;
using HIVE.Shared.Model;

namespace HIVE.Client.Services
{
    public class AdviserService : IAdviserService
    {
        private readonly HttpClient _client;

        public AdviserService(HttpClient client)
        {
            _client = client;
        }
        public Adviser Adviser {get; set; }
        public List<Adviser> Advisers {get; set; } = new List<Adviser>();
        public async Task<Adviser> GetAdviserAsync(int id)
        {
            var response = await _client.GetFromJsonAsync<Adviser>($"api/Adviser/{id}");
            if (response != null)
            {
                Adviser = response;
            }
            return Adviser;
        }

        public async Task<IEnumerable<Adviser>> GetAdvisersBySearchAsync(string search)
        {
            var result = await _client.GetFromJsonAsync<List<Adviser>>($"api/Adviser/{search}");
            return result;
        }

        public async Task<List<Adviser>> GetAdvisersAsync()
        {
            var response = await _client.GetFromJsonAsync<List<Adviser>>("api/Adviser");
            if (response is not null)
            {
                Advisers = response;
            }
            return Advisers;
        }

        public async Task<HttpResponseMessage> AddAdviserAsync(Adviser adviser)
        {
            var response = await _client.PostAsJsonAsync($"api/Adviser", adviser);
            return response;
        }


        public async Task<HttpResponseMessage> DeleteAdviserAsync(int adviserId)
        {
            var result = await _client.DeleteAsync($"api/Adviser/{adviserId}");
            return result;
        }

        public async Task<HttpResponseMessage> UpdateAdviserAsync(Adviser adviser)
        {
            var request = await _client.PutAsJsonAsync($"api/Adviser/{adviser.Id}", adviser);
            return request;
        }

    }
}
