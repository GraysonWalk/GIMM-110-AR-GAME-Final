using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IdleGridState : IState
{
    private readonly GridStateMachine machine;

    public IdleGridState(GridStateMachine machine)
    {
        this.machine = machine;
    }

    public void OnEnter()
    {
        Debug.Log("Entered Idle State");
    }

    public void Update()
    {
        GridNodeDirectional node = machine.GetNode();

        if (Input.GetKeyDown(KeyCode.W))
            TryMove(node.up);
        if (Input.GetKeyDown(KeyCode.S))
            TryMove(node.down);
        if (Input.GetKeyDown(KeyCode.A))
            TryMove(node.left);
        if (Input.GetKeyDown(KeyCode.D))
            TryMove(node.right);
        if (Input.GetKeyDown(KeyCode.E) && node.canInteract)
        {
            machine.SwitchState(new InteractGridState(machine));
        }
    }

    private void TryMove(GridNodeDirectional next)
    {
        if (next == null)
        {
            return;
        } else if (next.isActiveAndEnabled)
        {
            machine.SetNode(next);
        }
    }

    public void FixedUpdate()
    {
        //
    }

    public void OnExit()
    {
        Debug.Log("Exited Idle State");
        //
    }
}
