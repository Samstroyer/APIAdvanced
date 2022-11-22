using System.Text.Json.Serialization;

/*
This class file is made for the camera instance of queried rovers
It can be useful as it displays the names of the cameras the specific rover has
Camera information is useful for choosing camera
*/

public class Camera
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("rover_id")]
    public int Rover_id { get; set; }

    [JsonPropertyName("full_name")]
    public string Full_name { get; set; }
}
