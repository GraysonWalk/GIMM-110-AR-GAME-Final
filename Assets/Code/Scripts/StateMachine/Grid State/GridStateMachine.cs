using UnityEngine;

public class GridStateMachine : MonoBehaviour
{
    [Header("Node Reference")]
    [SerializeField] private GridNodeDirectional startNode;

    [SerializeField] private GridNodeDirectional checkpointNode;

    [SerializeField] private ArrowPasswordInput PasswordUI;

    [SerializeField] private ArrowPasswordInput PasswordUI2;


    [SerializeField] private GameObject GameInstance;

    [SerializeField] public GameManager manager;

    [SerializeField] private Animator anim;

    public bool firstPass = true;



    private IState currentState;

    private GridNodeDirectional currentNode;

    // Sets current node and player position to the start node
    // Activates idle grid state
    void Start()
    {
        currentNode = startNode;
        transform.position = startNode.transform.position;

        SwitchState(new IdleGridState(this));
    }

    void Update()
    {
        currentState?.Update();
        if (currentNode == null)
        {
            SetNode(checkpointNode);
        }
    }

    public void SwitchState(IState newState)
    {
        if (currentState != null)
            currentState.OnExit();

        currentState = newState;
        currentState.OnEnter();
    }

    public Transform GetTransform() => transform;
    public GridNodeDirectional GetNode() => currentNode;
    public GridNodeDirectional GetStartNode() => startNode;
    public ArrowPasswordInput GetUI() => PasswordUI;
    public ArrowPasswordInput GetUI2() => PasswordUI2;
    public GameObject GetGame() => GameInstance;
    public GameManager GetManager() => manager;
    public Animator GetAnim() => anim;
    public bool GetPassBool() => firstPass;


    public void SetNode(GridNodeDirectional node)
    {
        currentNode = node;
        transform.position = node.transform.position;
    }

    public void IdleSwap()
    {
        SwitchState(new IdleGridState(this));
    }

    public void SuccessSwap()
    {
        SwitchState(new SuccessGridState(this));
    }
}

