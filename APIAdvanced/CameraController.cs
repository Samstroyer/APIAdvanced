using System.Text.Json.Serialization;
using Raylib_cs;

public class CameraController
{
    [JsonPropertyName("photos")]
    public List<Photo> Photos { get; set; }
}
