public interface IState 
{
    void Enter();
    void Execute();
    void Exit();
    void HandleInput();
    IState GetSubState(); //for hierarchical states
}
