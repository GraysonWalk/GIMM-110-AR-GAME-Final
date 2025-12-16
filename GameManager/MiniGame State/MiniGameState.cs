using UnityEngine;

public class MiniGameState : IState
{
    private readonly GameManager manager;
    private readonly GameObject gameRoot;
    private readonly System.Action onComplete;

    public MiniGameState(GameManager manager, GameObject gameRoot, System.Action onComplete)
    {
        this.manager = manager;
        this.gameRoot = gameRoot;
        this.onComplete = onComplete;
    }

    public void OnEnter()
    {
        gameRoot.SetActive(true);
        Debug.Log($"Started {gameRoot.name}");
    }

    public void Update() { }
    public void FixedUpdate() { }

    public void OnExit()
    {
        gameRoot.SetActive(false);
        onComplete?.Invoke();
        Debug.Log($"Completed {gameRoot.name}");
    }
}