using UnityEngine;

public class FailState : IState
{
    private readonly GameManager manager;

    public FailState(GameManager manager)
    {
        this.manager = manager;
    }

    public void OnEnter()
    {
        Debug.Log("FAIL STATE! Game Over!");
        // TODO: Show UI, stop gameplay
    }

    public void Update() { }
    public void FixedUpdate() { }
    public void OnExit() { }
}