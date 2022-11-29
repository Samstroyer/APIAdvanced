public class MatchingPhotos
{
    public List<Photo> Photos { get; set; }
    ApplicationNumpad numpad;

    public MatchingPhotos(CameraController c, string filter)
    {
        numpad = new();
        Photos = new();
        foreach (Photo p in c.Photos)
        {
            if (p.Camera.Full_name == filter)
            {
                Photos.Add(p);
            }
            else
            {
                Console.WriteLine("Searching for {0} - {1} does not match", filter, p.Camera.Full_name);
            }
        }
    }
}
