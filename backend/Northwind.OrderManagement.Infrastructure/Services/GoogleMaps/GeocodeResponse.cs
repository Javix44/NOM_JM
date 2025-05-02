using System.Text.Json.Serialization;

public class GeocodeResponse
{
    [JsonPropertyName("results")]
    public List<Result> Results { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}

public class Result
{
    [JsonPropertyName("formatted_address")]
    public string FormattedAddress { get; set; }

    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; set; }

    [JsonPropertyName("address_components")]
    public List<AddressComponent> AddressComponents { get; set; }
}

public class AddressComponent
{
    [JsonPropertyName("long_name")]
    public string LongName { get; set; }

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; }

    [JsonPropertyName("types")]
    public List<string> Types { get; set; }
}

public class Geometry
{
    [JsonPropertyName("location")]
    public Location Location { get; set; }
}

public class Location
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}
