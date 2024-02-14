using Newtonsoft.Json;
using VenueFinder.Application.Interfaces;
using VenueFinder.Domain;
using VenueFinder.Domain.Entities;

namespace VenueFinder.Application
{
    public class CoinmapService : ICoinmapService
    {
        private readonly HttpClient _httpClient;

        public CoinmapService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Venue> GetVenueAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://coinmap.org/api/v1/venues/{id}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CoinmapResponse>(content);

                return result?.Venues[0] ?? new Venue();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error fetching venues: {e.Message}");
                return new Venue();
            }
        }

        public async Task<List<Venue>> GetAllVenuesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://coinmap.org/api/v1/venues/");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CoinmapResponse>(content);

                return result?.Venues ?? new List<Venue>();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error fetching venues: {e.Message}");
                return new List<Venue>();
            }
        }

        public async Task<List<Venue>> GetVenuesByCategoryAsync(string category)
        {
            try
            {
                var response = await _httpClient
                    .GetAsync($"https://coinmap.org/api/v1/venues/?category={category}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CoinmapResponse>(content);

                return result?.Venues ?? new List<Venue>();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error fetching venues by category: {e.Message}");
                return new List<Venue>();
            }
        }
    }
}
