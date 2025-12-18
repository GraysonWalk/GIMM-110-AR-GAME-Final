using System;
using UnityEngine;

public class ARTarget : DefaultObserverEventHandler
{
    [SerializeField]
    private bool isActive;

    public ARTargetList target;

    [SerializeField] private ARTargetDefinition definition;
    public ARTargetDefinition Definition => definition;
    public string DefinitionId => definition?.Id;

    public bool IsActive
    {
        get => isActive;
        set => SetActive(value);
    }

    public void Awake()
    {
        if (definition == null && target == ARTargetList.None)
            Debug.LogWarning($"ARTarget on {gameObject.name} has no definition or enum target assigned.");
    }

    public event Action<ARTarget> OnActivated;

    public void SetActive(bool value, bool notify = true)
    {
        if (isActive == value) return;
        isActive = value;
        if (value && notify) OnActivated?.Invoke(this);
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        SetActive(true);
    }
}