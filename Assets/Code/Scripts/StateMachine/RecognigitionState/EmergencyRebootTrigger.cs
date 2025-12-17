using UnityEngine;
using Vuforia;

public class EmergencyRebootTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerBar timerBar;

    [Header("Settings")]
    [SerializeField] private bool singleUse = true;

    private ObserverBehaviour observer;
    private bool used;

    private void Awake()
    {
        observer = GetComponent<ObserverBehaviour>();
    }

    private void OnEnable()
    {
        observer.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    private void OnDisable()
    {
        observer.OnTargetStatusChanged -= OnTargetStatusChanged;
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (used && singleUse) return;

        if (status.Status == Status.TRACKED ||
            status.Status == Status.EXTENDED_TRACKED)
            // timerBar.RestoreToFull();
            used = true;
    }
}