using Raylib_cs;

namespace raylib_cs_playground.Core;

public class Game(string windowTitle, Time time)
{
    public readonly EntityManager EntityManager = new();

    public void Init()
    {
        Raylib.SetTargetFPS(144);

        Raylib.InitWindow(800, 800, windowTitle);

        // Raylib.ToggleFullscreen();
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);

        EntityManager.RunDraw();

        Raylib.EndDrawing();
    }

    public void GameLoop()
    {
        EntityManager.RunStart();

        while (!Raylib.WindowShouldClose())
        {
            var startTime = Raylib.GetTime();

            time.DeltaTime = Raylib.GetFrameTime();

            Globals.Network.RunCallbacks();

            EntityManager.RunUpdate();

            Draw();

            var endTime = Raylib.GetTime();
            time.LastFrameTime = endTime - startTime;
        }

        Raylib.CloseWindow();
    }
}