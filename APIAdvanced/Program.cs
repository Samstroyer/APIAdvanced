using Raylib_cs;
using RestSharp;
using System.IO;
Engine e;

Setup();
Draw();

void Setup()
{
    Raylib.InitWindow(938, 938, "Nasa Explorer! - Light");
    e = new();
}

void Draw()
{
    e.Run();
}
