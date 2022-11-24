using System.Numerics;
using Raylib_cs;

/* 
This class file is made for the different menu screens and buttons to them
*/

public class BaseMenu
{
    protected virtual void Display() { } //Not needed really as RoverMenu's Display has arguments
}

public class PictureMenu
{

}

public class RoverMenu : BaseMenu
{
    List<Rectangle> chooseRoverButtons;

    public RoverMenu()
    {
        chooseRoverButtons = new();
    }

    public void CreateMenu(int roversCount, int longestNameSize)
    {
        int dividedHeight = Raylib.GetScreenHeight() / roversCount;
        int currHeight = dividedHeight / 2;
        for (int i = 0; i < roversCount; i++)
        {
            chooseRoverButtons.Add(new((Raylib.GetScreenWidth() / 2) - (longestNameSize / 2), currHeight, longestNameSize, 80));
            currHeight += dividedHeight;
        }
    }

    public string Display(List<Rover> r, int fontSize)
    {
        Vector2 mouseCords = Raylib.GetMousePosition();

        for (int i = 0; i < chooseRoverButtons.Count; i++)
        {
            string roverName = r[i].Name;
            Color c;
            if (Raylib.CheckCollisionPointRec(mouseCords, chooseRoverButtons[i]))
            {
                c = Color.GREEN;
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                {
                    return roverName;
                }
            }
            else
            {
                c = Color.RED;
            }
            Raylib.DrawRectangleRec(chooseRoverButtons[i], c);
            int offset = Raylib.MeasureText(roverName, fontSize);
            Raylib.DrawText(roverName, Raylib.GetScreenWidth() / 2 - offset / 2, (int)chooseRoverButtons[i].y, fontSize, Color.BLACK);
        }

        return "";
    }
}