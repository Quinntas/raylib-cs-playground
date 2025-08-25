using Raylib_cs;

namespace raylib_cs_playground.Core;

public class Game(string windowTitle, int screenWidth, int screenHeight, int targetFps, Time time)
{
    public readonly EntityManager _entityManager = new();
    public string WindowTitle { get; } = windowTitle;
    public int ScreenWidth { get; } = screenWidth;
    public int ScreenHeight { get; } = screenHeight;

    public void Init()
    {
        Raylib.SetTargetFPS(targetFps);
        Raylib.InitWindow(ScreenWidth, ScreenHeight, WindowTitle);
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);

        _entityManager.RunDraw();

        Raylib.EndDrawing();
    }

    public void GameLoop()
    {
        _entityManager.RunStart();

        while (!Raylib.WindowShouldClose())
        {
            var startTime = Raylib.GetTime();

            time.DeltaTime = Raylib.GetFrameTime();

            _entityManager.RunUpdate();

            Draw();

            var endTime = Raylib.GetTime();
            time.LastFrameTime = endTime - startTime;
        }

        Raylib.CloseWindow();
    }
}