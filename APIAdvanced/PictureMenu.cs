using System.Text.Json;
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
        ChooseCameraAndPicture,
        DisplayPicture
    }

    string pickedSol;

    MenuStates currMenu = MenuStates.EnterSol;
    API api = new();

    ChoosenRover choosenRoverContainer;

    ApplicationNumpad numpad;
    CameraController cameraController;
    CameraPicker cameraPicker;
    MatchingPhotos matchingPhotos;

    int pictureIndex;

    public PictureMenu(ChoosenRover cr)
    {
        choosenRoverContainer = cr;
        numpad = new();
        cameraPicker = new();
    }

    public void Start()
    {
        switch (currMenu)
        {
            case MenuStates.EnterSol:
                EnterSol();
                break;
            case MenuStates.ChooseCameraAndPicture:
                //ChooseCamera();
                AlternativeChooseCamera();
                break;
            case MenuStates.DisplayPicture:
                //Display();
                AlternativeDisplay();
                break;
        }
    }

    private void EnterSol()
    {
        pickedSol = numpad.Numpad(choosenRoverContainer);
        currMenu = MenuStates.ChooseCameraAndPicture;
    }

    private void ChooseCamera()
    {
        cameraController = JsonSerializer.Deserialize<CameraController>(api.AvailablePhotosForSolRequest(pickedSol, choosenRoverContainer.Name).Content);
        List<string> availableCameraNames = new();

        int longestNameSize = 0;
        foreach (Photo p in cameraController.Photos)
        {
            if (!availableCameraNames.Contains(p.Camera.Full_name))
            {
                availableCameraNames.Add(p.Camera.Full_name);
                int size = Raylib.MeasureText(p.Camera.Full_name, 48);
                if (size > longestNameSize)
                {
                    longestNameSize = size;
                }
            }
        }

        if (availableCameraNames.Count > 0)
        {
            cameraPicker.CreateButtons(availableCameraNames.Count, longestNameSize, availableCameraNames);
            string choosenCamera = cameraPicker.Display();
            matchingPhotos = new(cameraController, choosenCamera);

            currMenu = MenuStates.DisplayPicture;

            numpad = new();
            pictureIndex = numpad.Numpad(matchingPhotos.Photos.Count) - 1; // -1 because it is index
        }
    }

    private void AlternativeChooseCamera()
    {
        cameraController = JsonSerializer.Deserialize<CameraController>(api.AvailablePhotosForSolRequest(pickedSol, choosenRoverContainer.Name).Content);
        List<string> availableCameraNames = new();
        int longestNameSize = 0;
        foreach (Photo p in cameraController.Photos)
        {
            if (!availableCameraNames.Contains(p.Camera.Full_name))
            {
                availableCameraNames.Add(p.Camera.Full_name);
                int size = Raylib.MeasureText(p.Camera.Full_name, 48);
                if (size > longestNameSize)
                {
                    longestNameSize = size;
                }
            }
        }

        if (availableCameraNames.Count > 0)
        {
            cameraPicker.CreateButtons(availableCameraNames.Count, longestNameSize, availableCameraNames);
            string choosenCamera = cameraPicker.Display();
            matchingPhotos = new(cameraController, choosenCamera);

            currMenu = MenuStates.DisplayPicture;
        }
    }

    private void AlternativeDisplay()
    {
        List<Image> imgList = api.AlternativeFetchPhoto(matchingPhotos.Photos);
        List<Texture2D> textureList = new();
        for (int i = 0; i < imgList.Count; i++)
        {
            Image testImage = imgList[i];
            Texture2D newTexture = Raylib.LoadTextureFromImage(testImage);
            textureList.Add(newTexture);
        }

        int index = 0;

        while (true)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            Raylib.DrawTexture(textureList[index], 0, 0, Color.WHITE);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_E))
            {
                Console.WriteLine("yay");
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                break;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_J))
            {
                if (index == 0)
                {
                    index = textureList.Count - 1;
                }
                else
                {
                    index--;
                }
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_L))
            {
                if (index == textureList.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            Raylib.EndDrawing();
        }
    }

    private void Display()
    {
        Image img = api.FetchPhoto(matchingPhotos.Photos[pictureIndex].ImgSrc);
        Raylib.ImageResize(ref img, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        Texture2D t = Raylib.LoadTextureFromImage(img);

        while (true)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            Raylib.DrawTexture(t, 0, 0, Color.WHITE);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_E))
            {
                Console.WriteLine("yay");
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                break;
            }

            Raylib.EndDrawing();
        }
    }
}
