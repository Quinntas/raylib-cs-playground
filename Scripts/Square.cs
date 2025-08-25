using System.Numerics;
using raylib_cs_playground.Core;
using Raylib_cs;
using Transform = raylib_cs_playground.Components.Transform;

namespace raylib_cs_playground.Scripts;

public class Square : IEntity
{
    private readonly Transform _transform = new();

    public void Start()
    {
        Console.WriteLine("Start");
        _transform.Position = new Vector3(100, 100, 0);
        _transform.Rotation = new Vector3(50, 50, 50);
    }

    public void Update()
    {
        Console.WriteLine(Globals.Time.DeltaTime);

        _transform.Position = _transform.Position with { X = _transform.Position.X + 100 * Globals.Time.DeltaTime };
        _transform.Rotation = _transform.Rotation with { Z = _transform.Rotation.Z + 100 * Globals.Time.DeltaTime };
    }

    public void Draw()
    {
        Raylib.DrawRectanglePro(
            new Rectangle(
                _transform.Position.X,
                _transform.Position.Y,
                50,
                50
            ),
            new Vector2(25, 25),
            _transform.Rotation.Z,
            Color.Black
        );
    }
}