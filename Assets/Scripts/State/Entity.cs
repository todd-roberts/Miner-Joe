using UnityEngine;

public abstract class Entity<T> : MonoBehaviour where T : Entity<T>
{
    protected StateMachine<T> _stateMachine;

    protected virtual void Awake()
    {
        _stateMachine = new StateMachine<T>((T)this);
        _stateMachine.Initialize();
    }

    private void Update()
    {
        _stateMachine?.Update();
    }

    private void FixedUpdate() {
        _stateMachine?.FixedUpdate();
    }
    
    public void SetState(State<T> state)
    {
        _stateMachine.SetState(state);
    }

     public abstract State<T> GetInitialState();
}
