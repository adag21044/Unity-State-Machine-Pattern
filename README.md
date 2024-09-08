# Unity State Machine Pattern

This Unity project demonstrates the implementation of the State Machine pattern and adheres to SOLID principles. The primary focus is on managing the state and behavior of a cube object in the scene.

## Overview

The project consists of a state machine managing the movement of a cube object. The cube can move left, right, up, and down, and transitions between these states are handled through a state machine pattern.

## Key Components

### 1. **BaseState (Abstract Class)**

Defines the base functionality for all states, including methods for entering, executing, and exiting states, as well as handling input.

```csharp
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
```

### 2. **CubeController (MonoBehaviour)**

Manages the cube's state machine, initializes it, and updates it based on user input.

```csharp
public class CubeController : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = new StateMachine(transform);
    }

    private void Start()
    {
        if (StateMachine != null)
        {
            StateMachine.SetState(new MoveState(StateMachine));
        }
        else
        {
            Debug.LogError("StateMachine is still not initialized.");
        }
    }

    private void Update()
    {
        if (StateMachine != null)
        {
            StateMachine.HandleInput();
            StateMachine.Update();
        }
    }
}
```

### 3. **IState (Interface)**

Defines the contract for all state classes, including methods for managing state transitions and handling input.

```csharp
public interface IState 
{
    void Enter();
    void Execute();
    void Exit();
    void HandleInput();
    IState GetSubState(); // For hierarchical states
}
```

### 4. Sub-States (MoveLeftSubState, MoveRightSubState, MoveUpSubState, MoveDownSubState)

Each sub-state defines specific behavior for moving the cube in a particular direction.
+ MoveLeftSubState
+ MoveRightSubState
+ MoveUpSubState
+ MoveDownSubState

Each of these classes extends `BaseState` and overrides the `Enter`, `Execute`, and `Exit` methods to handle movement in a specific direction.

### 5. MoveState (Composite State)

Manages sub-states (e.g., moving left, right, up, or down) based on user input.

```csharp
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
        HandleInput();
    }

    public override void Exit()
    {
        Debug.Log("MoveState Exit");
        currentSubState.Exit();
    }

    public override void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            ChangeSubState(new MoveLeftSubState(stateMachine));
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            ChangeSubState(new MoveRightSubState(stateMachine));
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            ChangeSubState(new MoveUpSubState(stateMachine));
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            ChangeSubState(new MoveDownSubState(stateMachine));
        else
            ChangeSubState(new IdleState(stateMachine));
    }

    private void ChangeSubState(IState newSubState)
    {
        currentSubState.Exit();
        currentSubState = newSubState;
        currentSubState.Enter();
    }

    public override IState GetSubState() => currentSubState;
}
```

### 6. StateFactory

Factory class to create state instances based on type.

```csharp
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
```

### 7. StateMachine (State Machine Manager)

Manages the current state and transitions between states. It also handles input and updates the current state.

```csharp
public class StateMachine
{
    private IState currentState;
    private Transform cubeTransform;

    public event System.Action<IState> OnStateChanged;

    public StateMachine(Transform transform)
    {
        this.cubeTransform = transform;
    }

    public void SetState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        OnStateChanged?.Invoke(currentState);
    }

    public void Update()
    {
        currentState?.Execute();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public Transform GetTransform()
    {
        return cubeTransform;
    }
}
```

### 8. StateObserver (Observer Pattern)
Observes state changes and logs them.

```csharp
public class StateObserver : MonoBehaviour
{
    [SerializeField] private CubeController cubeController;

    private void OnEnable()
    {
        if (cubeController == null)
        {
            Debug.LogError("CubeController reference is not assigned in the inspector.");
            return;
        }

        if (cubeController.StateMachine == null)
        {
            Debug.LogError("StateMachine in CubeController is not initialized.");
            return;
        }

        cubeController.StateMachine.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        if (cubeController != null && cubeController.StateMachine != null)
        {
            cubeController.StateMachine.OnStateChanged -= HandleStateChanged;
        }
    }

    private void HandleStateChanged(IState newState)
    {
        Debug.Log($"State changed to {newState.GetType().Name}");
    }
}
```



## SOLID Principles Used

### Single Responsibility Principle (SRP)

Each class in this project has a single responsibility:

- **StateMachine:** Handles state transitions and manages the current state.
- **MoveState, MoveLeftSubState, MoveRightSubState, MoveUpSubState, MoveDownSubState:** Manage specific behaviors related to movement in different directions.
- **StateObserver:** Observes and logs state changes.

### Open/Closed Principle (OCP)

The system is designed to be open for extension but closed for modification. You can add new states by:

- **Extending** the `BaseState` class.
- **Implementing** the `IState` interface.

This design allows for easy addition of new states without modifying existing code.

### Liskov Substitution Principle (LSP)

Sub-classes like `MoveLeftSubState` and `MoveRightSubState` inherit from `BaseState` and can be used interchangeably with other states. This ensures that derived classes can substitute their base classes without affecting the correctness of the program.

### Interface Segregation Principle (ISP)

The `IState` interface is designed to be simple and focused on state behavior. It ensures that:

- **Implementing classes** only need to provide methods relevant to state behavior (`Enter`, `Execute`, `Exit`, `HandleInput`, `GetSubState`).

### Dependency Inversion Principle (DIP)

The `StateMachine` class depends on the `IState` interface rather than on concrete implementations. This design allows for:

- **Greater flexibility** in changing or adding new state implementations.
- **Easier testing** since `StateMachine` interacts with abstractions rather than concrete classes.

