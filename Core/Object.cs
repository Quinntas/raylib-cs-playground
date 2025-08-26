namespace raylib_cs_playground.Core;

public class Object
{
    private readonly Guid _instanceId = Guid.NewGuid();

    public string? Name { get; set; }

    public Guid GetInstanceId()
    {
        return _instanceId;
    }

    public Component Clone()
    {
        var clone = new Component
        {
            Name = Name
        };
        return clone;
    }
}