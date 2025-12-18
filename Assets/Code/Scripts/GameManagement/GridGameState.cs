using UnityEngine;

public class GridGameState : BaseState
{
    private readonly GameManager _gameManager;
    private readonly GameObject _gridGame;
    private readonly GridStateMachine _gridStateMachine;

    public GridGameState(GameManager gameManager, Animator animator, GameObject gridGame,
        GridStateMachine gridStateMachine) : base(gameManager, animator)
    {
        _gameManager = gameManager;
        _gridGame = gridGame;
        _gridStateMachine = gridStateMachine;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _gridGame.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}