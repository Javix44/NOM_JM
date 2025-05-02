using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Northwind.OrderManagement.Application.Features.Orders.DTOs;

namespace Northwind.OrderManagement.Domain.Interfaces
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleMapsService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["GoogleMaps:ApiKey"];
            
        }

        public async Task<ValidatedAddressDto> ValidateAddressAsync(string rawAddress)
        {
            var encoded = Uri.EscapeDataString(rawAddress);
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={encoded}&key={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<GeocodeResponse>(json);

            if (data == null)
                throw new Exception("Google Maps API response could not be deserialized.");

            if (data.Status != "OK")
                throw new Exception($"Google Maps API returned error: {data.Status}. Raw address: {rawAddress}");

            if (data.Results?.FirstOrDefault() is not { } result)
                throw new Exception($"Address not found. Raw address: {rawAddress}. Response: {json}");

            var location = result.Geometry.Location;

            string GetComponent(string type) =>
                result.AddressComponents.FirstOrDefault(c => c.Types.Contains(type))?.LongName;

            return new ValidatedAddressDto
            {
                FormattedAddress = result.FormattedAddress,
                Street = GetComponent("route"),
                City = GetComponent("locality") ?? GetComponent("sublocality") ?? GetComponent("administrative_area_level_2"),
                State = GetComponent("administrative_area_level_1"),
                PostalCode = GetComponent("postal_code"),
                Country = GetComponent("country"),
                Latitude = location.Lat,
                Longitude = location.Lng
            };
        }
    }
}
 