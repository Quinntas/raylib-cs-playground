using System.Numerics;
using Raylib_cs;

namespace raylib_cs_playground.Core;

public class Sprite(Texture2D sprite) : IDisposable
{
    private bool _disposed;
    public Rectangle SourceRect = new(0, 0, sprite.Width, sprite.Height);

    public Texture2D Texture { get; } = sprite;
    public Vector2 Origin { get; } = new(sprite.Width / 2f, sprite.Height / 2f);

    public void Dispose()
    {
        if (!_disposed)
        {
            Raylib.UnloadTexture(Texture);
            _disposed = true;
        }
    }
}