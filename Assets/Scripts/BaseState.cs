public abstract class BaseState : IState
{
    protected StateMachine stateMachine;
    protected const float moveSpeed = 5.0f; 

    protected BaseState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }

    public virtual IState GetSubState() => null;
}
