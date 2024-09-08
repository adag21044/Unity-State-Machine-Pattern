using UnityEngine;

public class StateObserver : MonoBehaviour
{
    [SerializeField] private CubeController cubeController;

    private void OnEnable()
    {
        cubeController.StateMachine.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        cubeController.StateMachine.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(IState newState)
    {
        Debug.Log($"State changed to {newState.GetType().Name}");
    }
}
