using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() => Debug.Log("Entered Idle State");

    public override void Execute() => Debug.Log("Idle State Executing");

    public override void Exit() => Debug.Log("Exited Idle State");
}