using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Animator _animator;
    private IState _endGameState;
    private IState _gridGameState;

    private IState _initializeState;
    private IState _recognitionGameState;

    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        StateMachine = new StateMachine();

        // // Create States
        // _initializeState = new InitializeState(this, _animator);
        // _gridGameState = new GridGameState(this, _animator);
        // _recognitionGameState = new RecognitionGameState(this, _animator);
        // _endGameState = new EndGameState(this, _animator);
        //
        // // Add Transitions
        // StateMachine.AddTransition(_initializeState, _gridGameState, () => _initializeState.IsCompleted);
        // StateMachine.AddTransition(_gridGameState, _recognitionGameState, () => _gridGameState.IsCompleted);
        // StateMachine.AddTransition(_recognitionGameState, _endGameState, () => _recognitionGameState.IsCompleted);

        // Set Initial State
        StateMachine.SetState(_initializeState);
    }
}