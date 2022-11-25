using RestSharp;

public class API
{
    public static RestClient client { get; set; } = new RestClient("https://api.nasa.gov/mars-photos/api/v1/");
    public RestRequest request { get; set; }
    public RestResponse response { get; set; }

    private static string apiKey { get; set; } = "BxUpg7OIRBnsjGirr3IRuUc2v0aeM4IpliYannDv"; //private mail api key, maybe shouldn't be shared on GitHub :D
    private static string simpleKeyEnding { get; set; } = "?api_key=";

    public RestResponse StartRequest()
    {
        //request is replaced by new()
        response = client.GetAsync(new("rovers?api_key=" + apiKey)).Result;
        return response;
    }

    public RestResponse PhotoRequest(string sol, string roverName)
    {
        //Example
        //https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?sol=0&api_key=DEMO_KEY

        request = new($"rovers/{roverName}/photos?sol={sol}&api_key={apiKey}");
        response = client.GetAsync(request).Result;
        return response;
    }

}
