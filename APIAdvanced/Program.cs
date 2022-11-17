using Raylib_cs;
using RestSharp;
using System.IO;

Setup();
Draw();

void Setup()
{
    Raylib.InitWindow(938, 938, "Nasa Explorer! - Light");
}


void Draw()
{
    string apiKey = "BxUpg7OIRBnsjGirr3IRuUc2v0aeM4IpliYannDv"; //private mail api key, maybe shouldn't be shared on GitHub :D
    string baseURL = "https://api.nasa.gov/";

    while (!Raylib.WindowShouldClose())
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);



        Raylib.EndDrawing();
    }
}
