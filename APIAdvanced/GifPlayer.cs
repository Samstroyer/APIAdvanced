using Raylib_cs;

class GifPlayer
{
    private int speed;

    List<Texture2D> textures;

    /// <summary>
    /// Creates a gif from png's in a folder
    /// </summary>
    /// <param name="folder">Path to image sequence</param>
    /// <param name="speed">The playback speed in fps</param>
    public GifPlayer(string folder, int speed)
    {
        this.speed = speed;
        string[] files = Directory.GetFiles(folder);

        textures = new();
        foreach (string file in files)
        {
            Image img = Raylib.LoadImage(file);
            textures.Add(Raylib.LoadTextureFromImage(img));
            Raylib.UnloadImage(img);
        }
    }

    public void Draw(int posX, int posY)
    {
        Raylib.DrawTexture(textures[(int)(Raylib.GetTime() * speed) % textures.Count], posX, posY, Color.WHITE);
    }
}