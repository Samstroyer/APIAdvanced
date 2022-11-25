using System.Text.Json;

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

    string pickedSol;

    MenuStates currMenu = MenuStates.EnterSol;
    API api = new();

    ChoosenRover choosenRoverContainer;

    ApplicationNumpad numpad;
    CameraController cameraController;

    public PictureMenu(ChoosenRover cr)
    {
        choosenRoverContainer = cr;
        numpad = new();
    }

    public void Start()
    {
        switch (currMenu)
        {
            case MenuStates.EnterSol:
                EnterSol();
                break;
            case MenuStates.ChooseCamera:

                break;
            case MenuStates.DisplayPicture:

                break;
        }
    }

    private void EnterSol()
    {
        pickedSol = numpad.Numpad(choosenRoverContainer);
        currMenu = MenuStates.ChooseCamera;
    }

    private void ChooseCamera()
    {
        cameraController = JsonSerializer.Deserialize<CameraController>(api.PhotoRequest(pickedSol, choosenRoverContainer.Name).Content);
        cameraController.CameraPicker();
    }
}
