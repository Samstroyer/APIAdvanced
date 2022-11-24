using RestSharp;

public class API
{
    public RestClient client { get; set; } = new RestClient("https://api.nasa.gov/mars-photos/api/v1/");
    public RestRequest request { get; set; }
    public RestResponse response { get; set; }

    private string apiKey { get; set; } = "BxUpg7OIRBnsjGirr3IRuUc2v0aeM4IpliYannDv"; //private mail api key, maybe shouldn't be shared on GitHub :D
    private string simpleKeyEnding { get; set; } = "?api_key=";

    public API()
    {

    }

    public RestResponse StartRequest()
    {
        //request is replaced by new()
        response = client.GetAsync(new("rovers?api_key=" + apiKey)).Result;
        return response;
    }

    public RestResponse CustomRequest(string url)
    {
        request = new(url + simpleKeyEnding + apiKey);
        response = client.GetAsync(request).Result;
        return response;
    }

}
