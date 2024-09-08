using UnityEngine;

public class MoveDownSubState : BaseState
{
    public Transform cubeTransform;

    public MoveDownSubState(StateMachine stateMachine) : base(stateMachine)
    {
        this.cubeTransform = stateMachine.GetTransform();
    }

    public override void Enter() => Debug.Log("Entered Move Down Sub-State");

    public override void Execute() => cubeTransform.Translate(Vector3.back * Time.deltaTime);

    public override void Exit() => Debug.Log("Exited Move Down Sub-State");
}