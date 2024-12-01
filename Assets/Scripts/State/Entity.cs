using UnityEngine;

public abstract class Entity<T> : MonoBehaviour
{
    protected StateMachine<T> _stateMachine; 

    private void Update()
    {
        OnUpdate();
        _stateMachine?.Update();
    }

    protected virtual void OnUpdate() { }

    private void FixedUpdate() {
        OnFixedUpdate();
        _stateMachine?.FixedUpdate();
    }

    protected virtual void OnFixedUpdate() { }
    
    public void SetState(State<T> state)
    {
        _stateMachine.SetState(state);
    }

     public abstract State<T> GetInitialState();
}
