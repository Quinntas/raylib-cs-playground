namespace raylib_cs_playground.Core;

public class TpsSystem
{
    private const double MaxFrameTime = 0.25;
    private double _accumulator;
    private double _lastSecond;
    private double _lastTickTime;
    private int _tickCount;

    private double _tickDeltaTimeReciprocal;

    public TpsSystem(double targetTps = 120.0)
    {
        SetTargetTps(targetTps);
        _lastTickTime = 0;
        _accumulator = 0;
        _tickCount = 0;
        _lastSecond = 0;
        ActualTps = 0;
    }

    public double TargetTps { get; private set; }
    public int ActualTps { get; private set; }
    public double TickDeltaTime { get; private set; }

    public void SetTargetTps(double tps)
    {
        if (tps <= 0)
            throw new ArgumentException("TPS must be greater than 0", nameof(tps));

        TargetTps = tps;
        TickDeltaTime = 1.0 / tps;
        _tickDeltaTimeReciprocal = tps;
    }

    public void Update(double currentTime, Action onTick)
    {
        if (_lastTickTime == 0)
        {
            _lastTickTime = currentTime;
            _lastSecond = currentTime;
            return;
        }

        var deltaTime = currentTime - _lastTickTime;
        _lastTickTime = currentTime;

        if (deltaTime > MaxFrameTime)
            deltaTime = MaxFrameTime;

        _accumulator += deltaTime;

        var ticksThisFrame = 0;
        const int maxTicksPerFrame = 10;

        while (_accumulator >= TickDeltaTime && ticksThisFrame < maxTicksPerFrame)
        {
            onTick?.Invoke();
            _accumulator -= TickDeltaTime;
            _tickCount++;
            ticksThisFrame++;
        }

        if (currentTime - _lastSecond >= 1.0)
        {
            ActualTps = _tickCount;
            _tickCount = 0;
            _lastSecond = currentTime;
        }
    }

    public void Reset()
    {
        _lastTickTime = 0;
        _accumulator = 0;
        _tickCount = 0;
        _lastSecond = 0;
        ActualTps = 0;
    }

    public double GetInterpolationAlpha()
    {
        return _accumulator * _tickDeltaTimeReciprocal;
    }
}