
using System.Net;
using RestSharp;
using Raylib_cs;

public class API
{
    public static RestClient client { get; set; } = new RestClient("https://api.nasa.gov/mars-photos/api/v1/");
    public RestRequest request { get; set; }
    public RestResponse response { get; set; }

    private string fileName = "temp.jpeg";

    private static string apiKey { get; set; } = "BxUpg7OIRBnsjGirr3IRuUc2v0aeM4IpliYannDv"; //private mail api key, maybe shouldn't be shared on GitHub :D
    private static string simpleKeyEnding { get; set; } = "?api_key=";

    public RestResponse StartRequest()
    {
        //request is replaced by new()
        response = client.GetAsync(new("rovers?api_key=" + apiKey)).Result;
        return response;
    }

    public RestResponse AvailablePhotosForSolRequest(string sol, string roverName)
    {
        //Example
        //https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?sol=0&api_key=DEMO_KEY

        request = new($"rovers/{roverName}/photos?sol={sol}&api_key={apiKey}");
        response = client.GetAsync(request).Result;
        return response;
    }

    public Image FetchPhoto(string imgSrc)
    {
        //Example
        //http://mars.jpl.nasa.gov/msl-raw-images/msss/01000/mcam/1000MR0044631190503679E04_DXXX.jpg

        /*
        OK, so NASA, take some really good notes now.
        I am up late thinking it is a nice night to code, WHY DO YOU LABEL YOUR JFIF FILES PNG?!?!?!
        I have done every step ever to troubleshoot why I can download and open your images but not render them in raylib... 
        Well, it is because you give us the JFIF....

        That is why this code exists...
        */
        WebClient wc = new();
        wc.DownloadFile(imgSrc, fileName);

        

        // Image test = Raylib.LoadImage("a.png");
        // Image redone = Raylib.LoadImage("redone.png");
        //File.Delete(fileName);

        return Raylib.LoadImage(fileName);
    }
}
