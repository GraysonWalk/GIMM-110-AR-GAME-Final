using UnityEngine;

public class EndGameState : BaseState
{
    private readonly GameManager _gameManager;
    private GameObject _failureMessage;
    private GameObject _successMessage;

    public EndGameState(GameManager gameManager, Animator animator) : base(gameManager, animator)
    {
        _gameManager = gameManager;
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