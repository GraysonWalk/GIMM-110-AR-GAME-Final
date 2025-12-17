using UnityEngine;

public class GridGameState : BaseState
{
    private readonly GameManager _gameManager;
    private GameObject _newGridGame;
    private GridStateMachine gridStateMachine;

    public GridGameState(GameManager gameManager, Animator animator) : base(gameManager, animator)
    {
        _gameManager = gameManager;
        _newGridGame = gameManager.gridGamePrefab;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _newGridGame = Object.Instantiate(_gameManager.gridGamePrefab);
        gridStateMachine = _newGridGame.GetComponentInChildren<GridStateMachine>();
        gridStateMachine.manager = _gameManager;
    }

    public override void OnExit()
    {
        gridStateMachine.GetGame().SetActive(false);
    }
}