using UnityEngine;

public class MoveLeftSubState : BaseState
{
    private Transform cubeTransform;
    

    public MoveLeftSubState(StateMachine stateMachine) : base(stateMachine)
    {
        this.cubeTransform = stateMachine.GetTransform();
    }

    public override void Enter() => Debug.Log("Entered Move Left Sub-State");

    public override void Execute() => cubeTransform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

    public override void Exit() => Debug.Log("Exited Move Left Sub-State");
}
