using System;
using UnityEngine;

public class ARTarget : DefaultObserverEventHandler
{
    [SerializeField]
    private bool isActive;

    [SerializeField] private ARTargetList _arTargetList;

    [SerializeField] private int selectedTargetIndex = -1;

    public bool IsActive
    {
        get => isActive;
        set => SetActive(value);
    }

    public ARTarget SelectedFromList
    {
        get
        {
            if (_arTargetList == null || selectedTargetIndex < 0 || selectedTargetIndex >= _arTargetList.Targets.Count)
                return null;
            return _arTargetList.Targets[selectedTargetIndex];
        }
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