using UnityEngine;

public class SuccessGridState : IState
{
    private readonly GridStateMachine machine;
    GameObject GameInstance;
    public SuccessGridState(GridStateMachine machine)
    {
        this.machine = machine;
        GameInstance = machine.GetGame();
    }

    public void OnEnter()
    {
        Debug.Log("Entered Success State");
    }

    public void Update()
    {
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
        GameInstance.SetActive(false);
    }
}
