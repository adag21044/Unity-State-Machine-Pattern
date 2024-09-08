using UnityEngine;

public class MoveUpSubState : BaseState
{
    private Transform cubeTransform;

    public MoveUpSubState(StateMachine stateMachine) : base(stateMachine)
    {
        this.cubeTransform = stateMachine.GetTransform(); // get the transform of the cube
    }

    public override void Enter() => Debug.Log("Entered Move Up Sub-State");

    public override void Execute() => cubeTransform.Translate(Vector3.forward * Time.deltaTime); // move the cube up

    public override void Exit() => Debug.Log("Exited Move Up Sub-State");
}