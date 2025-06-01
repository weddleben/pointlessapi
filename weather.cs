using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class Weather
{
    private static readonly HttpClient _http = new HttpClient();
    private static readonly Random _random = new Random();

    public static async Task<string> RunAsync()
    {
        for (int attempts = 0; attempts < 5; attempts++)
        {
            var (lat, lon) = GetRandomUSCoordinates();
            var (forecastURL, city, state) = await GetForecastUrl(lat, lon);

            if (forecastURL == null) continue;

            var forecast = await GetHourlyForecast(forecastURL);
            if (forecast == null) continue;

            return $"It is {forecast}°F in {city}, {state}";
        }

        return "Unable to retrieve forecast after several attempts.";
    }

    private static (double Latitude, double Longitude) GetRandomUSCoordinates()
    {
        double minLat = 24.5;
        double maxLat = 49.0;
        double minLon = -125.0;
        double maxLon = -66.9;

        double lat = _random.NextDouble() * (maxLat - minLat) + minLat;
        double lon = _random.NextDouble() * (maxLon - minLon) + minLon;

        return (Math.Round(lat, 6), Math.Round(lon, 6));
    }


    private static async Task<(string forecastUrl, string city, string state)> GetForecastUrl(double lat, double lon)
    {
        string url = $"https://api.weather.gov/points/{lat},{lon}";
        _http.DefaultRequestHeaders.UserAgent.ParseAdd("DarthVader");

        try
        {
            var response = await _http.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
                return (null, null, null);

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(response.Headers);
            var json = JsonDocument.Parse(responseString);

            string forecastURL = json.RootElement.GetProperty("properties")
                                        .GetProperty("forecastHourly")
                                        .GetString();

            string city = json.RootElement.GetProperty("properties")
                                    .GetProperty("relativeLocation")
                                    .GetProperty("properties")
                                    .GetProperty("city")
                                    .GetString();

            string state = json.RootElement.GetProperty("properties")
                                     .GetProperty("relativeLocation")
                                     .GetProperty("properties")
                                     .GetProperty("state")
                                     .GetString();

            return (forecastURL, city, state);
        }
        catch
        {
            return (null, null, null);
        }
    }

    private static async Task<string> GetHourlyForecast(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var validatedUri))
        {
            Console.WriteLine($"Invalid URL: {url}");
            return null;
        }

        try
        {
            var response = await _http.GetAsync(validatedUri);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(responseString);

            int temperature = json.RootElement
                                  .GetProperty("properties")
                                  .GetProperty("periods")[0]
                                  .GetProperty("temperature")
                                  .GetInt16();

            return temperature.ToString();
        }
        catch
        {
            return null;
        }
    }
}
