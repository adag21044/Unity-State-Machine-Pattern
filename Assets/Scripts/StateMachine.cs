using UnityEngine;

public class StateMachine
{
    private IState currentState;
    private Transform cubeTransform;

    public event System.Action<IState> OnStateChanged;

    public StateMachine(Transform transform)
    {
        this.cubeTransform = transform;
    }

    public void SetState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        OnStateChanged?.Invoke(currentState); // Observer pattern tetikleyici
    }

    public void Update()
    {
        currentState?.Execute();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public Transform GetTransform()
    {
        return cubeTransform;
    }
}
