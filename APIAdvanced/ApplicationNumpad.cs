using System.Numerics;
using Raylib_cs;

/*
This class file is made for creating a numpad onscreen that you can enter numbers in
This is used in PictureMenu
*/

public class ApplicationNumpad
{
    List<(Byte num, Rectangle rec)> numberButton = new();
    Rectangle delete;
    Rectangle enter;

    int fontSize = 32;

    Vector2 offsets;

    public string currentSearch = "";
    public string savedSearch;

    public ApplicationNumpad()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

        offsets = new(405, 650);

        int x = 0;
        int y = 0;
        for (Byte i = 1; i <= 10; i++)
        {
            if (i == 10)
            {
                Rectangle r = new(60 + offsets.X, 180 + offsets.Y, 50, 50);
                delete = new(0 + offsets.X, 180 + offsets.Y, 50, 50);
                enter = new(120 + offsets.X, 180 + offsets.Y, 50, 50);
                numberButton.Add((0, r));
                break;
            }
            else
            {
                Rectangle r = new(x + offsets.X, y + offsets.Y, 50, 50);
                numberButton.Add((i, r));
            }

            x += 60;
            if (x >= 180)
            {
                y += 60;
                x = 0;
            }
        }
    }

    public string Numpad(ChoosenRover r)
    {
        int currentSearchInt = 0;

        while (true)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            Raylib.DrawText($"Max sol: {r.Max_sol}\nStart date: {r.Landing_date}\nEnd date: {r.Max_date}", 10, 20, fontSize, Color.BLACK);

            int temp;
            if (int.TryParse(currentSearch, out temp))
            {
                currentSearchInt = temp;
            }
            else
            {
                currentSearchInt = 0;
            }


            var mousePos = Raylib.GetMousePosition();
            Color c;
            foreach (var item in numberButton)
            {
                c = Color.GREEN;
                Raylib.DrawRectangleRec(item.rec, Color.GRAY);

                string potential = currentSearch + item.num.ToString();
                if (int.Parse(potential) > r.Max_sol)
                {
                    c = Color.RED;
                    Raylib.DrawRectangleRec(item.rec, Color.DARKGRAY);
                }
                else if (Raylib.CheckCollisionPointRec(mousePos, item.rec))
                {
                    c = Color.DARKGREEN;
                    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                    {
                        currentSearch += item.num.ToString();
                    }
                }
                Raylib.DrawText(item.num.ToString(), (int)item.rec.x + 5, (int)item.rec.y + 8, fontSize, c);
            }

            //Enter button
            c = Color.GREEN;
            Raylib.DrawRectangleRec(delete, Color.GRAY);
            if (Raylib.CheckCollisionPointRec(mousePos, delete))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                {
                    Raylib.EndDrawing();
                    savedSearch = currentSearch;
                    return savedSearch;
                }
                c = Color.DARKGREEN;
            }
            Raylib.DrawText("E", (int)delete.x + 5, (int)delete.y + 8, fontSize, c);

            //Delete button
            c = Color.GREEN;
            Raylib.DrawRectangleRec(enter, Color.GRAY);
            if (Raylib.CheckCollisionPointRec(mousePos, enter))
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) && currentSearch.Length > 0)
                {
                    currentSearch = currentSearch.Remove(currentSearch.Length - 1);
                }
                c = Color.DARKGREEN;
            }
            Raylib.DrawText("<", (int)enter.x + 5, (int)enter.y + 8, fontSize, c);

            int offset = Raylib.MeasureText(currentSearch, fontSize);
            Raylib.DrawText(currentSearch, Raylib.GetScreenWidth() / 2 - offset / 2, 400, fontSize, Color.BLACK);

            Raylib.EndDrawing();
        }
    }
}