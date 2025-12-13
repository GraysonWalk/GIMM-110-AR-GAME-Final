using UnityEngine;
using Vuforia;

/// <summary>
///     This class can handle status change events for Vuforia targets when attached to an object that also has an
///     Observer Event Handler script attached to it (Vuforia Target Objects).
///     It is currently set up to log status changes to the console.
/// </summary>
public class StatusEventHandler : MonoBehaviour
{
    private ObserverBehaviour _observerBehaviour;

    private void Awake()
    {
        _observerBehaviour = GetComponent<ObserverBehaviour>();

        if (_observerBehaviour != null)
            _observerBehaviour.OnTargetStatusChanged += OnStatusChanged;
    }

    private void OnDestroy()
    {
        if (_observerBehaviour != null)
            _observerBehaviour.OnTargetStatusChanged -= OnStatusChanged;
    }

    /// <summary>
    ///     Callback called when the status of the target changes. Currently logs the target name, status, and status info to
    ///     the console.
    /// </summary>
    /// <param name="behaviour"></param>
    /// <param name="status"></param>
    private void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        Debug.LogFormat("TargetName: {0}, Status is: {1}, StatusInfo is: {2}", behaviour.TargetName, status.Status,
            status.StatusInfo);
    }
}

/*
 *  Code pulled from https://developer.vuforia.com/library/vuforia-engine/getting-started/vuforia-engine-api/observer-and-observations/pose-status-and-status-info-unity/
 */