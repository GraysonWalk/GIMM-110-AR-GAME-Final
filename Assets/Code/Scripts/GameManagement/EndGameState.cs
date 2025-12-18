using UnityEngine;

public class EndGameState : BaseState
{
    private readonly GameObject _failureMessage;
    private readonly GameManager _gameManager;
    private readonly GameObject _successMessage;

    public EndGameState(GameManager gameManager, Animator animator, GameObject failureMessage,
        GameObject successMessage) : base(gameManager, animator)
    {
        _gameManager = gameManager;
        _failureMessage = failureMessage;
        _successMessage = successMessage;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_gameManager.gameLost)
            _failureMessage.SetActive(true);
        else
            _successMessage.SetActive(true);
    }
}