using UnityEngine;

public class SuccessState : IState
{
    private readonly GameManager manager;

    public SuccessState(GameManager manager)
    {
        this.manager = manager;
    }

    public void OnEnter()
    {
        Debug.Log("SUCCESS STATE! All minigames complete!");
        manager.GetTimerBar().active = false;
        // TODO: Show UI, stop gameplay
    }

    public void Update() { }
    public void FixedUpdate() { }
    public void OnExit() { }
}
