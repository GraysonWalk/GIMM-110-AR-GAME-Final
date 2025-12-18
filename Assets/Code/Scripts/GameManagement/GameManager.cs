using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public List<ARTarget> startingTargets = new();
    public GameObject gridGame;

    [FormerlySerializedAs("recognitionGamePrefab")]
    public GameObject recognitionGame;

    public bool gameLost;

    [SerializeField] private Camera arCamera;
    public bool gridGameFirst = true;

    [SerializeField] private GameObject failureMessage;
    [SerializeField] private GameObject successMessage;

    private Animator _animator;
    private BaseState _endGameState;
    private BaseState _gridGameState;

    private GridStateMachine _gridStateMachine;
    private BaseState _recognitionGameState;

    public bool GridGameComplete { get; set; }
    public bool RecognitionGameComplete { get; set; }

    private StateMachine StateMachine { get; set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        StateMachine = new StateMachine();
        _gridStateMachine = gridGame.GetComponent<GridStateMachine>();

        if (_gridStateMachine == null)
            Debug.LogError("GridStateMachine component missing on gridGame GameObject.");
        else
            // Ensure the GridStateMachine has a runtime reference to this GameManager
            _gridStateMachine.manager = this;

        // // Create States
        _gridGameState = new GridGameState(this, _animator, gridGame, _gridStateMachine);
        _recognitionGameState = new RecognitionGameState(this, _animator, recognitionGame);
        _endGameState = new EndGameState(this, _animator, failureMessage, successMessage);

        // Add Transitions
        if (gridGameFirst)
        {
            StateMachine.AddTransition(_gridGameState, _recognitionGameState,
                new FuncPredicate(() => _gridGameState.IsCompleted));
            StateMachine.AddTransition(_recognitionGameState, _endGameState,
                new FuncPredicate(() => _recognitionGameState.IsCompleted));
            StateMachine.SetState(_gridGameState);
        }
        else
        {
            StateMachine.AddTransition(_recognitionGameState, _gridGameState,
                new FuncPredicate(() => _recognitionGameState.IsCompleted));
            StateMachine.AddTransition(_gridGameState, _endGameState,
                new FuncPredicate(() => _gridGameState.IsCompleted));
            StateMachine.SetState(_recognitionGameState);
        }
    }

    private void Update()
    {
        StateMachine?.Update();
    }

    private void FixedUpdate()
    {
        StateMachine?.FixedUpdate();
    }

    public void OnGridGameComplete()
    {
        Debug.Log("GameManager: Grid game complete");
        _gridGameState.IsCompleted = true;
    }

    public void OnRecognitionGameComplete()
    {
        Debug.Log("GameManager: Recognition game complete");
        _recognitionGameState.IsCompleted = true;
    }

    public void OnTimeout()
    {
        gameLost = true;
        StateMachine.ChangeState(_endGameState);
    }
}