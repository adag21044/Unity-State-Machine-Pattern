using UnityEngine;

public class CubeController : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        // Initialize StateMachine in Awake to ensure it's ready before any other component starts
        StateMachine = new StateMachine(transform);
    }

    private void Start()
    {
        if (StateMachine != null)
        {
            StateMachine.SetState(new MoveState(StateMachine)); // Set initial state
        }
        else
        {
            Debug.LogError("StateMachine is still not initialized.");
        }
    }

    private void Update()
    {
        if (StateMachine != null)
        {
            StateMachine.HandleInput(); // Handle input for state changes
            StateMachine.Update(); // Update current state and sub-state
        }
    }
}
