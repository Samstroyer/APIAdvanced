using System.Text.Json;
using System.Numerics;
using Raylib_cs;

class Engine
{
    enum MenuState
    {
        Menu,
        Display
    }
    MenuState menu;
    int fontSize = 48;
    V1 activeRovers = new();
    API controller = new();
    ChoosenRover choosenRover;

    RoverMenu rm = new();
    PictureMenu pm = new();

    public Engine()
    {
        menu = MenuState.Menu;
        activeRovers.InitRovers(controller.StartRequest().Content, fontSize);

        rm.CreateMenu(activeRovers.roversCount, activeRovers.longestName);
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
                    Console.WriteLine("MenuState has no recognized state...");
                    Console.WriteLine("Error! Exiting Program!");
                    Raylib.CloseWindow();
                    break;
            }
        }
    }

    private void Menu()
    {
        string choosenName = "";
        while (true)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            choosenName = rm.Display(activeRovers.Rovers, fontSize);

            if (choosenName != "")
            {
                menu = MenuState.Display;
                InitRover(choosenName);
                break;
            }

            Raylib.EndDrawing();
        }
    }

    private void Render()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);

        pm.Display();

        Raylib.EndDrawing();
    }

    private void InitRover(string roverName)
    {
        foreach (Rover r in activeRovers.Rovers)
        {
            if (r.Name == roverName)
            {
                choosenRover = new(r);
            }
        }
    }
}