using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InteractGridState : IState
{
    private readonly GridStateMachine machine;
    private ArrowPasswordInput passwordUI;
    [SerializeField] private GameObject node;


    public InteractGridState(GridStateMachine machine)
    {
        this.machine = machine;
    }


    public void OnEnter()
    {
        Debug.Log("Entered Interact State");

        passwordUI = machine.GetUI();

        ArrowPassword pw = machine.GetNode().GetComponent<ArrowPassword>();

        SelectionBehavior sb = machine.GetNode().GetComponent<SelectionBehavior>();

        if (pw != null)
        {
            Debug.Log("password started");
            passwordUI.StartSequence(pw, machine.GetNode());

        } else if (sb != null) {
  
                sb.Selected();
        }
        else
        {
            machine.GetNode().onActivate.Invoke();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ArrowPassword pw = machine.GetNode().GetComponent<ArrowPassword>();

            SelectionBehavior sb = machine.GetNode().GetComponent<SelectionBehavior>();
            if (sb == null && pw == null)
            {
                machine.GetNode().onDeactivate.Invoke();
            }
            machine.SwitchState(new IdleGridState(machine));
        }
    }

    public void FixedUpdate()
    {
         //
    }

    public void OnExit()
    {
        Debug.Log("Exited Interact State");
    }
}
