namespace raylib_cs_playground.Core;

public class AnimationController
{
    private readonly Dictionary<string, Animation> _animations = new();
    public string? CurrentAnimationName { get; private set; }

    public void AddAnimation(string name, SpriteSheet spriteSheet, float frameTime = 0.1f)
    {
        _animations[name] = new Animation(spriteSheet, frameTime);
    }

    public void Play(string name)
    {
        CurrentAnimationName = name;
        _animations[name].Play();
    }

    public Sprite GetSprite()
    {
        if (CurrentAnimationName == null) throw new Exception("No animation playing");
        return _animations[CurrentAnimationName].GetCurrentSprite();
    }

    public void Update()
    {
        if (CurrentAnimationName == null) return;
        _animations[CurrentAnimationName].Update(Globals.Time.DeltaTime);
    }
}