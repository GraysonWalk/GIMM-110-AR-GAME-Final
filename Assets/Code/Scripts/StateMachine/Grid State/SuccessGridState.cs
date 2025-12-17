using UnityEngine;

public class SuccessGridState : IState
{
    private readonly GridStateMachine machine;
    private readonly GameManager manager;
    private readonly GameObject GameInstance;

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
        Debug.Log(manager);
        manager.OnGridGameComplete();
        GameInstance.SetActive(false);
    }
}