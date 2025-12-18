using System.Linq;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    // Returns true when every required objectState has at least one ARTarget with the same definition id and IsActive == true.
    public bool CheckMatches(GameConfiguration activeConfig, ARTarget scannedTarget, GameObject systemsHolder)
    {
        if (activeConfig == null) return false;

        ARTarget[] allTargets = null;

        if (systemsHolder != null)
            allTargets = systemsHolder.GetComponentsInChildren<ARTarget>(true);

        if (allTargets == null || allTargets.Length == 0)
            allTargets = FindObjectsOfType<ARTarget>(true);

        var requiredStates = activeConfig.objectStates
            .Where(s => s.targetDefinition != null && s.requireActive);

        foreach (var state in requiredStates)
        {
            var matched = allTargets.Any(t => t.Definition != null
                                              && t.Definition.Id == state.targetDefinition.Id
                                              && t.IsActive);
            if (!matched) return false;
        }

        return true;
    }
}