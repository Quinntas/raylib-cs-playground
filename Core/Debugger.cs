using Raylib_cs;

namespace raylib_cs_playground.Core;

public class Debugger : IEntity
{
    private readonly Dictionary<string, double> _debugData = new();

    public void Draw()
    {
        var yPosition = 10;
        foreach (var (name, value) in _debugData)
        {
            Raylib.DrawText($"{name}: {value:F2}", 10, yPosition, 17, Color.Black);
            yPosition += 25;
        }
    }

    public void UpsertDebugData(string name, double value)
    {
        _debugData[name] = value;
    }
}