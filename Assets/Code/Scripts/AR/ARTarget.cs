using System;
using UnityEngine;

public class ARTarget : DefaultObserverEventHandler
{
    [SerializeField]
    private bool isActive;

    public ARTargetList target;

    public bool IsActive
    {
        get => isActive;
        set => SetActive(value);
    }

    public void Awake()
    {
        if (target == ARTargetList.None) Debug.LogWarning($"ARTarget on {gameObject.name} has no target assigned.");
    }

    public event Action<ARTarget> OnActivated;

    // Use this to change state. If notify is false, the event won't fire.
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