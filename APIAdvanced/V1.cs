using System.Text.Json.Serialization;
using System.Text.Json;
using Raylib_cs;

/*
This class file is for the request to see available rovers
This makes it possible to sustain itself if the api updates
*/

public class V1
{
    [JsonPropertyName("rovers")]
    public List<Rover> Rovers { get; set; } = new();

    public int roversCount;
    public int longestName = 0;

    public void InitRovers(string content, int fontSize)
    {
        Rovers = JsonSerializer.Deserialize<V1>(content).Rovers;
        roversCount = Rovers.Count;

        if (roversCount > 1)
        {
            CalculateNameLengths(fontSize);
        }
        else
        {
            longestName = Raylib.MeasureText(Rovers[0].Name, fontSize);
        }
    }

    private void CalculateNameLengths(int fontSize)
    {
        int record = 0;
        foreach (Rover r in Rovers)
        {
            int temp = Raylib.MeasureText(r.Name, fontSize);
            if (temp > record)
            {
                record = temp;
            }
        }
        //I don't want it to be perfectly sized so I will add 10 units (pixels?)
        record += 10;
        longestName = record;
    }
}