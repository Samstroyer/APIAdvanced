using System.Numerics;
using Raylib_cs;
/*
This class file is made for choosen rover in engine
It contains useful information for displaying UI
*/

// Temp from failed attempt
// (string name, List<Camera> availableCameras, int cameraPages, int currentPage) choosenRover = new("", new(), 1, 1); //I have cameraPages here as it will be different per rover


public class ChoosenRover : Rover
{
    List<Rectangle> choosenRoverAvailableCameraButtons;
    int currentPage = 1;
    int cameraPages;

    //Det här kopierades rakt av, fråga INTE vad det gör
    //https://stackoverflow.com/a/47557736
    public ChoosenRover(Rover r)
    {
        foreach (var prop in r.GetType().GetProperties())
        {
            this.GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(r, null), null);
        }
    }

    public void InitValues()
    {
        cameraPages = (int)Math.Ceiling((double)Cameras.Count / 5d);
    }
}
