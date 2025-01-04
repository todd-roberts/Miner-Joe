public abstract class State<T> where T : Entity<T>
{
    protected T _entity;

    public void SetEntity(T entity)
    {
        _entity = entity;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }
}
