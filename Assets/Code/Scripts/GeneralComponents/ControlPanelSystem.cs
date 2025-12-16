using UnityEngine;

public class ControlPanelSystem : MonoBehaviour
{
    private ARTarget[] _systemStates;

    private void Awake()
    {
        _systemStates = GetComponentsInChildren<ARTarget>();
        if (_systemStates == null) return;
        foreach (var s in _systemStates) s.OnActivated += OnStateActivated;
    }

    private void OnDestroy()
    {
        if (_systemStates == null) return;
        foreach (var s in _systemStates) s.OnActivated -= OnStateActivated;
    }

    private void OnStateActivated(ARTarget activated)
    {
        foreach (var s in _systemStates)
        {
            if (s == activated) continue;
            // Turn others off without triggering their activation event
            s.SetActive(false, false);
        }
    }
}