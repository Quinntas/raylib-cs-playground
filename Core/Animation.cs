namespace raylib_cs_playground.Core;

public class Animation(SpriteSheet spriteSheet, float frameTime)
{
    private readonly SpriteSheet _spriteSheet = spriteSheet;
    private int _currentFrame;
    private float _timer;
    public float FrameTime { get; } = frameTime;

    public void Play()
    {
        _currentFrame = 0;
        _timer = 0;
    }

    public void Update(float deltaTime)
    {
        _timer += deltaTime;
        if (_timer <= FrameTime) return;
        _timer = 0;
        _currentFrame = (_currentFrame + 1) % _spriteSheet.Sprites.Count;
    }

    public Sprite GetCurrentSprite()
    {
        return _spriteSheet.Sprites[_currentFrame];
    }
}