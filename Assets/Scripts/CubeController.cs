using UnityEngine;

public class CubeController : MonoBehaviour
{
    private StateMachine stateMachine;

    public StateMachine StateMachine => stateMachine; // Public property for stateMachine

    private void Start()
    {
        stateMachine = new StateMachine(transform);
        stateMachine.SetState(new MoveState(stateMachine));
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }
}
