namespace raylib_cs_playground.Core;

public class EntityManager
{
    private List<IEntity> Entities { get; } = new();

    public void AddEntity(IEntity entity)
    {
        Entities.Add(entity);
    }

    public void RunStart()
    {
        foreach (var entity in Entities)
            entity.Start();
    }

    public void RunUpdate()
    {
        foreach (var entity in Entities)
            entity.Update();
    }

    public void RunFixedUpdate()
    {
        foreach (var entity in Entities)
            entity.FixedUpdate();
    }

    public void RunDraw()
    {
        foreach (var entity in Entities)
            entity.Draw();
    }
}