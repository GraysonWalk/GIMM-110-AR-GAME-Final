using UnityEngine;

public class RecognitionGameState : BaseState
{
    private readonly GameManager _gameManager;
    private GameObject _newRecognitionGame;
    private RecognitionGameManager _recognitionGameManager;

    public RecognitionGameState(GameManager gameManager, Animator animator) : base(gameManager, animator)
    {
        _gameManager = gameManager;
        _newRecognitionGame = gameManager.recognitionGamePrefab;
    }

    public override void Update()
    {
        base.Update();
        // Check if the recognition game is complete
        if (_gameManager.RecognitionGameComplete)
        {
            // IsCompleted = true;
            // _gameManager.StateMachine.ChangeState(EndGameState);
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _newRecognitionGame = Object.Instantiate(_gameManager.recognitionGamePrefab);
        _recognitionGameManager.gameManager = _gameManager;
    }

    public override void OnExit()
    {
        base.OnExit();
        _newRecognitionGame.gameObject.SetActive(false);
    }
}