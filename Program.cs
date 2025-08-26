using raylib_cs_playground.Core;
using raylib_cs_playground.Scripts;

namespace raylib_cs_playground;

public static class Globals
{
    public static Time Time { get; } = new();
    public static Network Network { get; } = new();
}

internal static class Program
{
    public static void Main()
    {
        Globals.Network.Init();

        var game = new Game(
            "Playground",
            Globals.Time
        );

        game.Init();
        game.EntityManager.AddEntity(new Knight());
        game.GameLoop();

        Globals.Network.Close();
    }
}