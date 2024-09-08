using UnityEngine;

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
