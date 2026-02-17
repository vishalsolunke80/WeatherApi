using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApiDemo.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public WeatherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<object> GetWeather()
        {
            string apiKey = "b72fbc86a690195278112b0ec4f46cf9";
            string city = "Mumbai";

            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var json = JsonDocument.Parse(result);

            var data = new
            {
                City = json.RootElement.GetProperty("name").GetString(),
                Temperature = json.RootElement.GetProperty("main").GetProperty("temp").GetDecimal(),
                Weather = json.RootElement.GetProperty("weather")[0].GetProperty("description").GetString(),
                Humidity = json.RootElement.GetProperty("main").GetProperty("humidity").GetInt32()
            };

            return data;
        }
    }
}
