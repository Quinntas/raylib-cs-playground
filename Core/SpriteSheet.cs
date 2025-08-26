using Raylib_cs;

namespace raylib_cs_playground.Core;

public class SpriteSheet
{
    public SpriteSheet(string path, int spriteSheetWidth, int spriteSheetHeight, int colCount, int rowCount)
    {
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Sprites", path);
        
        Console.WriteLine(fullPath);
        
        Texture2D spriteSheet = Raylib.LoadTexture(fullPath);
        
        Console.WriteLine(spriteSheet.Id);
    }
}