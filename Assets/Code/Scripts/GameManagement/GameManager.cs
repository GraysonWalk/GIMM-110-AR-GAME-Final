using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<ARTarget> startingTargets = new();
    public GameObject gridGamePrefab;
    public GameObject recognitionGamePrefab;
    [SerializeField] private Camera arCamera;
    public bool gameLost;
    private Animator _animator;
    private BaseState _endGameState;
    private BaseState _gridGameState;

    private BaseState _recognitionGameState;

    public bool GridGameComplete { get; set; }
    public bool RecognitionGameComplete { get; set; }

    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        StateMachine = new StateMachine();

        // // Create States
        _gridGameState = new GridGameState(this, _animator);
        _recognitionGameState = new RecognitionGameState(this, _animator);
        _endGameState = new EndGameState(this, _animator);

        // // Add Transitions
        // StateMachine.AddTransition(_initializeState, _gridGameState, () => _initializeState.IsCompleted);
        StateMachine.AddTransition(_gridGameState, _recognitionGameState,
            new FuncPredicate(() => _gridGameState.IsCompleted));
        StateMachine.AddTransition(_endGameState, _recognitionGameState,
            new FuncPredicate(() => _endGameState.IsCompleted));

        // Set Initial State
        StateMachine.SetState(_gridGameState);
    }

    public void OnGridGameComplete()
    {
        arCamera.rect = new Rect(.19f, .01f, .34f, .61f);
        StateMachine.ChangeState(_recognitionGameState);
    }

    public void OnTimeout()
    {
        gameLost = true;
        StateMachine.ChangeState(_endGameState);
    }
}