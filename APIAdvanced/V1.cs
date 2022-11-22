using System.Text.Json.Serialization;

/*
This class file is for the request to see available rovers
This makes it possible to sustain itself if the api updates
*/

public class V1
{
    [JsonPropertyName("rovers")]
    public List<Rover> Rovers { get; set; }
}