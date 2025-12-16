using UnityEngine;

public class BaseState : IState
{
    protected const float CrossFadeDuration = 0.1f;
    protected readonly Animator Animator;
    protected readonly GameManager GameManager;


    public BaseState(GameManager gameManager, Animator animator)
    {
        GameManager = gameManager;
        Animator = animator;
    }

    public bool IsCompleted { get; protected set; }

    public virtual void OnEnter()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void OnExit()
    {
    }
}