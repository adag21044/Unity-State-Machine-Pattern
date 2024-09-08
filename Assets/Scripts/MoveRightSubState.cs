using UnityEngine;

public class MoveRightSubState : BaseState
{
    private Transform cubeTransform;

    public MoveRightSubState(StateMachine stateMachine) : base(stateMachine)
    {
        this.cubeTransform = stateMachine.GetTransform();
    }

    public override void Enter() => Debug.Log("Entered Move Right Sub-State");

    public override void Execute() => cubeTransform.Translate(Vector3.right * Time.deltaTime);

    public override void Exit() => Debug.Log("Exited Move Right Sub-State");
}
