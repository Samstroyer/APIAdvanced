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
        // Raylib.BeginDrawing();
        // Raylib.ClearBackground(Color.WHITE);

        // //There is a maximum of 17 cameras on one rover (Perserverance has 17, Curiosity has 7 and the other 2 has 5 cameras...) 
        // //I have to make a page button because of that...... Not nice
        // //I will make it so that it displays 5 per page, why not
        // for (int i = 0; i < 5; i++)
        // {

        // }

        // Raylib.EndDrawing();
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