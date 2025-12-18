using UnityEngine;

public class SuccessGridState : IState
{
    private readonly GameObject GameInstance;
    private readonly GridStateMachine machine;
    private readonly GameManager manager;

    public SuccessGridState(GridStateMachine machine)
    {
        this.machine = machine;
        GameInstance = machine.GetGame();
        manager = machine.GetManager();
    }

    public void OnEnter()
    {
        Debug.Log("Entered Success State");
    }

    public void Update()
    {
        // Quits game instance on any input
        if (Input.anyKeyDown)
        {
            Debug.Log("Success confirmed");
            OnExit();
        }
    }

    public void FixedUpdate()
    {
    }

    public void OnExit()
    {
        Debug.Log("Exited Success State");
        if (manager != null)
            manager.OnGridGameComplete();
        else
            Debug.LogWarning(
                "SuccessGridState: manager is null — cannot call OnGridGameComplete(). Ensure GridStateMachine.manager is assigned.");

        if (GameInstance != null)
            GameInstance.SetActive(false);
    }
}