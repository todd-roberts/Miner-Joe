public class StateMachine<T>
{
    private readonly T _entity;
    private State<T> _currentState;

    public StateMachine(T entity)
    {
        _entity = entity;
    }

    public void SetState(State<T> newState)
    {
        _currentState?.Exit();

        _currentState = newState;
        _currentState.SetEntity(_entity); 

        _currentState.Enter();
    }

    public void SetNovelState(State<T> newState)
    {
        if (_currentState == null || !(_currentState is T))
        {
            SetState(newState);  
        }
    }

    public State<T> GetState() => _currentState;

    public void Update()
    {
        _currentState?.Update();
    }

    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }
}
