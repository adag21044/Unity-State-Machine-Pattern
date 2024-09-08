

public class StateFactory 
{
    public static IState CreateState(StateType stateType, StateMachine stateMachine)
    {
        switch (stateType)
        {
            case StateType.MoveLeft:
                return new MoveLeftSubState(stateMachine);
            case StateType.MoveRight:
                return new MoveRightSubState(stateMachine);
            default:
                return new IdleState(stateMachine);
        }
    }
}

public enum StateType
{

    MoveLeft,
    MoveRight
}