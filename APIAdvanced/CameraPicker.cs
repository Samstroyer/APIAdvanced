using System.Numerics;
using Raylib_cs;

/* 
This class file is made for the choosing of camera in PictureMenu
This is a copy of RoverMeny.cs basically - but remade completely as it was a mess to "redo"
*/

public class CameraPicker
{
    List<(Rectangle r, string s)> availableCameraButtons;

    public CameraPicker()
    {
        availableCameraButtons = new();
    }

    public void CreateButtons(int cameraNameCount, int longestNameSize, List<string> cameraNames)
    {
        int dividedHeight = Raylib.GetScreenHeight() / cameraNameCount;
        int currHeight = dividedHeight / 2;
        for (int i = 0; i < cameraNameCount; i++)
        {
            availableCameraButtons.Add(new(new((Raylib.GetScreenWidth() / 2) - (longestNameSize / 2), currHeight, longestNameSize, 80), cameraNames[i]));
            currHeight += dividedHeight;
        }
    }

    public string Display()
    {
        int fontSize = 32;
        while (true)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            Vector2 mouseCords = Raylib.GetMousePosition();
            int index = 0;
            foreach (var b in availableCameraButtons)
            {
                Raylib.DrawRectangleRec(b.r, Color.RED);
                int length = Raylib.MeasureText(b.s, fontSize);
                Raylib.DrawText(b.s, (Raylib.GetScreenWidth() / 2) - (length / 2), (int)b.r.y + 7, fontSize - 2, Color.BLACK);
                if (Raylib.CheckCollisionPointRec(mouseCords, b.r))
                {
                    Raylib.DrawRectangleRec(b.r, Color.GREEN);
                    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                    {
                        Raylib.EndDrawing();
                        return b.s;
                    }
                }
            }

            Raylib.EndDrawing();
        }
    }
}