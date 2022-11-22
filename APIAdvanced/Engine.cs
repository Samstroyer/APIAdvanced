using System.Text.Json;
using RestSharp;
using Raylib_cs;
using System;

class Engine
{
    string apiKey = "BxUpg7OIRBnsjGirr3IRuUc2v0aeM4IpliYannDv"; //private mail api key, maybe shouldn't be shared on GitHub :D

    enum MenuState
    {
        Menu,
        Display
    }
    MenuState menu;

    RestClient client;
    RestRequest request;
    RestResponse response;

    int roversCount;

    public Engine()
    {
        menu = MenuState.Menu;

        client = new RestClient("https://api.nasa.gov/mars-photos/api/v1/");
        response = client.GetAsync(new("rovers?api_key=" + apiKey)).Result;
        V1 activeRovers = JsonSerializer.Deserialize<V1>(response.Content);
        
        // roversCount = activeRovers.Rovers.Count();

        V1 yes = new();

        JsonSerializer.Serialize<V1>(yes);

    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            switch (menu)
            {
                case MenuState.Menu:
                    Menu();
                    break;

                case MenuState.Display:
                    Render();
                    break;

                default:
                    Console.WriteLine("Error!\nExiting Program!");
                    Console.WriteLine("MenuState has no recognized state...");
                    Raylib.CloseWindow();
                    break;
            }
        }
    }

    private void Menu()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);



        Raylib.EndDrawing();
    }

    private void Render()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);
        Raylib.EndDrawing();
    }
}