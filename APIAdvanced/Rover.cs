using System;
using System.Text.Json.Serialization;

/*
This class file is made for the rover object in the json sometimes queried from the api
*/

public class Rover
{
    [JsonPropertyName("id")]
    public int Id { get; private set; }

    [JsonPropertyName("name")]
    public string Name { get; private set; }

    [JsonPropertyName("landing_date")]
    public string Landing_date { get; private set; }

    [JsonPropertyName("launch_date")]
    public string Launch_date { get; private set; }

    [JsonPropertyName("status")]
    public string Status { get; private set; }

    [JsonPropertyName("max_sol")]
    public int Max_sol { get; private set; }

    [JsonPropertyName("max_date")]
    public string Max_date { get; private set; }

    [JsonPropertyName("total_photos")]
    public int Total_photos { get; private set; }

    [JsonPropertyName("cameras")]
    public List<Camera> Cameras { get; private set; }
}
