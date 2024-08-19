using System.Text.Json;
using System.Numerics;
using Raylib_cs;
using Aspose.Imaging.Exif.Enums;
using Aspose.Imaging.Xmp.Schemas.XmpDm;
using System.Runtime.CompilerServices;

class Engine
{
    enum MenuStates
    {
        Menu,
        Display
    }

    // Start in menu
    MenuStates menu = MenuStates.Menu;
    int fontSize = 48;
    V1 activeRovers = new();
    API controller = new();

    RoverMenu roverPicker = new();
    PictureMenu picturePicker;


    GifPlayer gifPlayer;
    bool loadingRovers = true;

    public Engine()
    {
        gifPlayer = new GifPlayer("Resources/loading", 4);

        var a = Task.Run(() =>
        {
            Thread.Sleep(10000000);
            activeRovers.InitRovers(controller.StartRequest().Content, fontSize);
            roverPicker.CreateMenu(activeRovers.roversCount, activeRovers.longestName);
            loadingRovers = false;
        });
    }

    string[] loadChars = { " |", "/", "--", "\\", " |", "/", "--", "\\" };

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            switch (menu)
            {
                case MenuStates.Menu:
                    Menu();
                    break;

                case MenuStates.Display:
                    Render();
                    break;

                default:
                    Console.WriteLine("Error, exiting program!");
                    Raylib.CloseWindow();
                    break;
            }
        }
    }

    private void Menu()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);

        if (loadingRovers)
        {
            Raylib.DrawText("Fetching rovers...", 380, 200, 24, Color.BLACK);
            Raylib.DrawText(loadChars[(int)(Raylib.GetTime() * 5) % loadChars.Length], Raylib.GetScreenWidth() / 2, 250, 24, Color.BLACK);
            gifPlayer.Draw(385, 400);
            Raylib.EndDrawing();
            return;
        }
        else if (activeRovers.Rovers.Count <= 0)
        {
            Raylib.DrawText("Failed to fetch rovers! Closing app", 400, 200, 24, Color.BLACK);
            Raylib.EndDrawing();
            Thread.Sleep(1500);
            Raylib.CloseWindow();
            return;
        }

        string choosenName = roverPicker.Display(activeRovers.Rovers, fontSize);

        Raylib.EndDrawing();

        if (choosenName != "")
        {
            menu = MenuStates.Display;
            InitRover(choosenName);
        }
    }

    private void Render()
    {
        picturePicker.Start();
    }

    private void InitRover(string roverName)
    {
        foreach (Rover r in activeRovers.Rovers)
        {
            if (r.Name == roverName)
            {
                picturePicker = new(new(r));
            }
        }
    }
}