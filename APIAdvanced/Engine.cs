using System.Text.Json;
using System.Numerics;
using Raylib_cs;

class Engine
{
    enum MenuStates
    {
        Menu,
        Display
    }
    MenuStates menu;
    int fontSize = 48;
    V1 activeRovers = new();
    API controller = new();

    RoverMenu roverPicker = new();
    PictureMenu picturePicker;

    public Engine()
    {
        menu = MenuStates.Menu;
        activeRovers.InitRovers(controller.StartRequest().Content, fontSize);

        roverPicker.CreateMenu(activeRovers.roversCount, activeRovers.longestName);
    }

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

            choosenName = roverPicker.Display(activeRovers.Rovers, fontSize);

            if (choosenName != "")
            {
                menu = MenuStates.Display;
                InitRover(choosenName);
                break;
            }

            Raylib.EndDrawing();
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