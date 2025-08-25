using raylib_cs_playground.Core;
using raylib_cs_playground.Scripts;

namespace raylib_cs_playground;

public static class Globals
{
    public static Time Time { get; } = new();
}

internal static class Program
{
    public static void Main()
    {
        var game = new Game(
            "Playground",
            800,
            600,
            144,
            Globals.Time
        );

        game.Init();
        game._entityManager.AddEntity(new Square());
        game.GameLoop();
    }
}