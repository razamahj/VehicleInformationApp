using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace VehicleInfoApp.Services
{
    public class VehicleService
    {
        private readonly HttpClient _httpClient;

        public VehicleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<VehicleInfo> GetVehicleInfo(string registrationNumber)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", "fZi8YcjrZN1cGkQeZP7Uaa4rTxua8HovaswPuIno");

            var response = await _httpClient.GetAsync($"https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration={registrationNumber}");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var vehicleInfo = await JsonSerializer.DeserializeAsync<VehicleInfo>(responseStream);

            return vehicleInfo;
        }
    }

    public class VehicleInfo
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public DateTime? MotExpiryDate { get; set; }
        public int? MileageAtLastMot { get; set; }
    }
}
