using System.Numerics;
using raylib_cs_playground.Core;
using Raylib_cs;
using Transform = raylib_cs_playground.Components.Transform;

namespace raylib_cs_playground.Scripts;

public class Knight : IEntity
{
    private readonly AnimationController _animationController = new();
    private readonly SpriteSheet _idleSpriteSheet = new("knight_idle.png", 42, 42);
    private readonly Transform _transform = new();

    public void Start()
    {
        _animationController.AddAnimation("idle", _idleSpriteSheet);
        _animationController.Play("idle");
        _transform.Position = new Vector3(100, 100, 0);
        _transform.Scale = new Vector3(4, 4, 1);
    }

    public void Update()
    {
        _transform.Position = _transform.Position with { X = _transform.Position.X + 5 * Globals.Time.DeltaTime };
        // _transform.Rotation = _transform.Rotation with { Z = _transform.Rotation.Z + 100 * Globals.Time.DeltaTime };
        _animationController.Update();
    }

    public void Draw()
    {
        var sprite = _animationController.GetSprite();

        Raylib.DrawTexturePro(
            sprite.Texture,
            sprite.SourceRect,
            new Rectangle(
                _transform.Position.X, _transform.Position.Y,
                sprite.Texture.Width * _transform.Scale.X,
                sprite.Texture.Height * _transform.Scale.Y
            ),
            sprite.Origin,
            _transform.Rotation.Z,
            Color.White
        );
    }
}