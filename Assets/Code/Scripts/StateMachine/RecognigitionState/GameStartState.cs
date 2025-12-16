using System.Collections.Generic;
using UnityEngine;

public class GameStartState : RecognitionBaseState
{
    [SerializeField] private GameObject _systems;
    private readonly List<ARTarget> _targets = new List<ARTarget>();
    private readonly List<ARTarget> _activatedTargets = new List<ARTarget>();
    
    public override void OnEnter()
    {
        var targets = _systems.GetComponentsInChildren<ARTarget>();
        _targets.AddRange(targets);
        foreach (var target in targets)
        {
            target.OnActivated += OnTargetActivated;
        }
    }

    private void OnTargetActivated(ARTarget target)
    {
        if
        _activatedTargets.Add(target);
    }
}
