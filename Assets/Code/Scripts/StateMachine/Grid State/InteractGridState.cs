using Unity.VisualScripting;
using UnityEngine;

public class InteractGridState : IState
{
    private readonly GridStateMachine machine;
    private ArrowPasswordInput passwordUI;
    private bool firstPass;
    [SerializeField] private GameObject node;


    public InteractGridState(GridStateMachine machine)
    {
        this.machine = machine;
    }


    public void OnEnter()
    {
        Debug.Log("Entered Interact State");
        firstPass = machine.GetPassBool();
        

            ArrowPassword pw = machine.GetNode().GetComponent<ArrowPassword>();

        SelectionBehavior sb = machine.GetNode().GetComponent<SelectionBehavior>();

        if (pw != null)
        {
            if (firstPass)
            {
                passwordUI = machine.GetUI();
                machine.firstPass = false;
            }
            else
            {
                passwordUI = machine.GetUI2();
            }

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
