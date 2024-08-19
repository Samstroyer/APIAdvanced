using System.Net;
using RestSharp;
using Raylib_cs;

public class API
{
    public static RestClient client { get; } = new RestClient("https://api.nasa.gov/mars-photos/api/v1/");
    public RestRequest? request { get; set; }
    public RestResponse? response { get; set; }

    private string trueFileName = "temp.jpg";
    private string usedFileName = "temp.png";

    private static string apiKey { get; } = "BxUpg7OIRBnsjGirr3IRuUc2v0aeM4IpliYannDv"; //private mail api key, maybe shouldn't be shared on GitHub :D

    /// <summary>
    /// Gets the rovers of the API
    /// </summary>
    /// <returns>Rovers</returns>
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

        It says jpg in url yes, but I think no

        That is why this code exists... (aspose code)
        */
        WebClient wc = new();
        wc.DownloadFile(imgSrc, trueFileName);


        using (Aspose.Imaging.Image asposeImage = Aspose.Imaging.Image.Load(trueFileName))
        {
            // Create PNG options

            asposeImage.Save(usedFileName);
        }

        Image raylibImage = Raylib.LoadImage(usedFileName);
        Raylib.ImageResize(ref raylibImage, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

        File.Delete(trueFileName);
        File.Delete(usedFileName);

        return raylibImage;
    }

    public List<Image> AlternativeFetchPhoto(List<Photo> photos)
    {
        List<Image> imgList = new();
        WebClient wc = new();

        int counter = 0;

        foreach (Photo p in photos)
        {
            wc.DownloadFile(p.ImgSrc, trueFileName);

            using (Aspose.Imaging.Image asposeImage = Aspose.Imaging.Image.Load(trueFileName))
            {
                asposeImage.Save(usedFileName);
            }

            Image raylibImage = Raylib.LoadImage(usedFileName);
            Raylib.ImageResize(ref raylibImage, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            imgList.Add(raylibImage);

            File.Delete(usedFileName);
            File.Delete(trueFileName);

            Console.WriteLine("Downloaded {0}, out of {1}", counter, photos.Count);
            counter++;
        }

        return imgList;
    }
}