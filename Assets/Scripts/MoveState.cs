using UnityEngine;

public class MoveState : BaseState
{
    private IState currentSubState;
    public MoveState(StateMachine stateMachine) : base(stateMachine)
    {
        currentSubState = new IdleState(stateMachine);
    }

    public override void Enter()
    {
        Debug.Log("MoveState Enter");
        currentSubState.Enter();
    }

    public override void Execute()
    {
        Debug.Log("MoveState Execute");
        currentSubState.Execute();
        HandleInput(); // Delegate input handling to sub-state
    }

    public override void Exit()
    {
        Debug.Log("MoveState Exit");
        currentSubState.Exit();
    }

    public override void HandleInput()
    {
        // Delegate input to sub-state or change sub-state based on input
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            ChangeSubState(new MoveLeftSubState(stateMachine));
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            ChangeSubState(new MoveRightSubState(stateMachine));
    }

    private void ChangeSubState(IState newSubState)
    {
        currentSubState.Exit();
        currentSubState = newSubState;
        currentSubState.Enter();
    }

    public override IState GetSubState() => currentSubState;
}