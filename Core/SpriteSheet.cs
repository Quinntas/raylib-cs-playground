using Raylib_cs;

namespace raylib_cs_playground.Core;

public class SpriteSheet : IDisposable
{
    private bool _disposed;

    public SpriteSheet(string path, int spriteWidth, int spriteHeight)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Sprites", path);
        var spriteSheet = Raylib.LoadTexture(fullPath);

        if (spriteSheet.Id == 0)
            throw new Exception($"Failed to load sprite sheet: {fullPath}");

        var spritesCount = spriteSheet.Width / spriteWidth;
        Sprites = new List<Sprite>(spritesCount);

        var fullImage = Raylib.LoadImageFromTexture(spriteSheet);

        try
        {
            for (var i = 0; i < spritesCount; i++)
            {
                var subImage = Raylib.ImageFromImage(
                    fullImage,
                    new Rectangle(i * spriteWidth, 0, spriteWidth, spriteHeight)
                );

                try
                {
                    var sprite = new Sprite(Raylib.LoadTextureFromImage(subImage));
                    Sprites.Add(sprite);
                }
                finally
                {
                    Raylib.UnloadImage(subImage);
                }
            }
        }
        finally
        {
            Raylib.UnloadImage(fullImage);
            Raylib.UnloadTexture(spriteSheet);
        }
    }

    public List<Sprite> Sprites { get; }

    public void Dispose()
    {
        if (!_disposed)
        {
            foreach (var sprite in Sprites) Raylib.UnloadTexture(sprite.Texture);
            Sprites.Clear();
            _disposed = true;
        }
    }
}