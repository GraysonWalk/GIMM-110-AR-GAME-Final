using UnityEngine;

public class SuccessGridState : IState
{
    private readonly GridStateMachine machine;
    GameObject GameInstance;
    GameObject manager;
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
        //manager.game1Complete = false;
        GameInstance.SetActive(false);
    }
}
