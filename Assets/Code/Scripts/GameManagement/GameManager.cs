using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public List<ARTarget> startingTargets = new();
    public GameObject gridGamePrefab;
    public GameObject recognitionGamePrefab;
    private Animator _animator;
    
    public bool GridGameComplete { get; set; }
    private BaseState _endGameState;
    private BaseState _gridGameState;

    private BaseState _initializeState;
    private BaseState _recognitionGameState;

    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        StateMachine = new StateMachine();

        // // Create States
        _initializeState = new InitializeState(this, _animator);
        _gridGameState = new GridGameState(this, _animator);
        // _recognitionGameState = new RecognitionGameState(this, _animator);
        // _endGameState = new EndGameState(this, _animator);
        
        // // Add Transitions
        // StateMachine.AddTransition(_initializeState, _gridGameState, () => _initializeState.IsCompleted);
        // StateMachine.AddTransition(_gridGameState, _recognitionGameState, () => _gridGameState.IsCompleted);
        // StateMachine.AddTransition(_recognitionGameState, _endGameState, () => _recognitionGameState.IsCompleted);

        // Set Initial State
        StateMachine.SetState(_initializeState);
    }
}