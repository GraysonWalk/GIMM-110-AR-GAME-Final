using UnityEngine;

public class RecognitionGameState : BaseState
{
    private readonly GameManager _gameManager;
    private readonly GameObject _recognitionGame;
    private readonly RecognitionGameManager _recognitionGameManager;

    public RecognitionGameState(GameManager gameManager, Animator animator, GameObject recognitionGame) : base(
        gameManager, animator)
    {
        _gameManager = gameManager;
        _recognitionGame = recognitionGame;
        _recognitionGameManager = _recognitionGame.GetComponent<RecognitionGameManager>();
    }

    public override void Update()
    {
        base.Update();
        // Check if the recognition game is complete
        // if (_gameManager.RecognitionGameComplete) IsCompleted = true;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _recognitionGame.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        if (!_gameManager.gridGameFirst) _recognitionGame.SetActive(false);
    }
}