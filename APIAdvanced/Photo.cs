using System.Text.Json.Serialization;

public class Photo
{
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("sol")]
    public int Sol { get; set; }

    [JsonPropertyName("camera")]
    public Camera Camera { get; set; }

    [JsonPropertyName("img_src")]
    public string img_src { get; set; }

    [JsonPropertyName("earth_date")]
    public string Earth_date { get; set; }

    [JsonPropertyName("rover")]
    public Rover Rover { get; set; }
}
