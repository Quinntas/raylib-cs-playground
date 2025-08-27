using Raylib_cs;

namespace raylib_cs_playground.Core;

public class Game(string windowTitle, Time time)
{
    public readonly EntityManager EntityManager = new();
    public readonly TpsSystem TpsSystem = new();

    public void Init()
    {
        Raylib.SetTargetFPS(144);

        Raylib.InitWindow(800, 800, windowTitle);

        // Raylib.ToggleFullscreen();
    }

    private void OnDraw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);

        EntityManager.RunDraw();

        Raylib.EndDrawing();
    }

    private void OnTick()
    {
        time.TickDeltaTime = TpsSystem.TickDeltaTime;

        Globals.Debugger.UpsertDebugData("Target TPS", TpsSystem.TargetTps);
        Globals.Debugger.UpsertDebugData("Actual TPS", TpsSystem.ActualTps);

        EntityManager.RunFixedUpdate();
    }

    private void OnFrame()
    {
        time.DeltaTime = Raylib.GetFrameTime();

        Globals.Debugger.UpsertDebugData("FPS", Raylib.GetFPS());

        EntityManager.RunUpdate();
    }


    public void GameLoop()
    {
        EntityManager.RunStart();

        while (!Raylib.WindowShouldClose())
        {
            var startTime = Raylib.GetTime();

            Globals.Network.RunCallbacks();

            TpsSystem.Update(startTime, OnTick);

            OnFrame();

            OnDraw();

            var endTime = Raylib.GetTime();
            time.LastFrameTime = endTime - startTime;
        }

        Raylib.CloseWindow();
    }
}