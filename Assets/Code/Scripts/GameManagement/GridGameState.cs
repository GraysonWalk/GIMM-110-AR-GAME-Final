using UnityEngine;

public class GridGameState : BaseState
{
    private readonly GameManager _gameManager;
    private GameObject _newGridGame;

    public GridGameState(GameManager gameManager, Animator animator) : base(gameManager, animator)
    {
        _gameManager = gameManager;
        _newGridGame = gameManager.gridGamePrefab;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _newGridGame = Object.Instantiate(_gameManager.gridGamePrefab);
        GridStateMachine gridStateMachine = _newGridGame.GetComponentInChildren<GridStateMachine>();
        gridStateMachine.manager = _gameManager;
    }
    
    public override void Update()
    {
        base.Update();
        // Check if the grid game is complete
        if (_gameManager.GridGameComplete)
        {
            IsCompleted = true;
            // _gameManager.StateMachine.ChangeState(RecognitionState);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        _newGridGame.gameObject.SetActive(false);
    }
}