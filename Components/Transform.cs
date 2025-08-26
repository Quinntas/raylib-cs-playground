using System.Numerics;
using raylib_cs_playground.Core;

namespace raylib_cs_playground.Components;

public class Transform : Component
{
    public Vector3 Position { get; set; } = new(0, 0, 0);
    public Vector3 Rotation { get; set; } = new(0, 0, 0);
    public Vector3 Scale { get; set; } = new(1, 1, 1);
}