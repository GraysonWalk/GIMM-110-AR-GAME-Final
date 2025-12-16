using System.Collections.Generic;
using UnityEngine;

public class InitializeState : BaseState
{
    private readonly List<ARTarget> _activatedTargets = new();
    private readonly GameObject _systems;
    private readonly List<ARTarget> _targets = new();

    private GameManager _gameManager;

    public InitializeState(GameManager gameManager, Animator animator) : base(gameManager, animator)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        var targets = _systems.GetComponentsInChildren<ARTarget>();
        _targets.AddRange(targets);
        foreach (var target in targets) target.OnActivated += OnTargetActivated;
    }

    private void OnTargetActivated(ARTarget target)
    {
        if (_activatedTargets.Count >= 3)
        {
            _gameManager.startingTargets = _activatedTargets;
            IsCompleted = true; // Set to true when targets are set
        }
        else
        {
            _activatedTargets.Add(target);
        }
    }
}