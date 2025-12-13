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

        // Movement Inputs //
        // Takes input and calls TryMove with directionality
        // Uses Grid Node Directional ENUM for readability

        // Up
        if (Input.GetKeyDown(KeyCode.W))
            TryMove(node.up);
        // Down
        if (Input.GetKeyDown(KeyCode.S))
            TryMove(node.down);
        // Left
        if (Input.GetKeyDown(KeyCode.A))
            TryMove(node.left);
        // Right
        if (Input.GetKeyDown(KeyCode.D))
            TryMove(node.right);
        // Interact
            // Checks node canInteract is active and E input
        if (Input.GetKeyDown(KeyCode.E) && node.canInteract)
        {
            machine.SwitchState(new InteractGridState(machine));
        }
    }

    // Movement logic for Nodes
    private void TryMove(GridNodeDirectional next)
    {
        // Checks existence of node
        if (next == null)
        {
            return;

            // Checks for active, stops movement to non-active nodes
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
