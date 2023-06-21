namespace Blobsio.Core;

public abstract class Component
{
    public Entity entity { get; private set; }

    internal void OnInstantiate(Entity entity)
    {
        this.entity = entity;
    }
    
    public virtual void Start() { }

    public virtual void Update() { }

    public virtual void LateUpdate() { }

    public virtual void OnDestroy() { }

    public virtual void OnCollision(Entity collision) { }


    #region WorldMethods
    public void Destroy(Entity entity)
    {
        this.entity.Destroy(entity);
    }

    public void Destroy(Component component)
    {
        entity.Destroy(component);
    }

    protected T GetComponent<T>() where T : Component
        => entity.GetComponent<T>();

    protected List<T> GetComponents<T>() where T : Component
        => entity.GetComponents<T>();

    protected T AddComponent<T>() where T : Component
        => entity.AddComponent<T>();

    public Entity Instantiate(Entity entity)
        => this.entity.world.Instantiate(entity);

    public Entity FindEntityByTag(string tag)
    {
        return entity.world.FindEntitiyByTag(tag);
    }

    public Entity[] FindEntitiesByTag(string tag)
    {
        return entity.world.FindEntitiesByTag(tag);
    }

    #endregion
}
