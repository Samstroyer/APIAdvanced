using Raylib_cs;

/* 
This class file is made for the choosing of picture
This class is planned to be extremely complex and helpful for the user over anything
*/

public class PictureMenu
{
    private enum MenuStates
    {
        EnterSol,
        ChooseCamera,
        EnterPictureID,
        DisplayPicture
    }

    MenuStates currMenu = MenuStates.EnterSol;

    ChoosenRover choosenRoverContainer;

    public PictureMenu(ChoosenRover cr)
    {
        choosenRoverContainer = cr;
    }

    public void Start()
    {
        switch (currMenu)
        {
            case MenuStates.EnterSol:

                break;
            case MenuStates.ChooseCamera:

                break;
            case MenuStates.EnterPictureID:

                break;
            case MenuStates.DisplayPicture:

                break;
        }
    }

    private void EnterSol()
    {
        while (true)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);



            Raylib.EndDrawing();
        }
    }
}
