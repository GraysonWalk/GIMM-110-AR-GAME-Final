using System.Linq;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    /// <summary>
    ///     Handles the event when an AR target is found. It marks the isActive flag on the ARTarget component as true,
    ///     then checks if the found target matches the expected state in the active game configuration. If all targets match
    ///     their expected states,
    ///     it triggers the puzzle solved event.
    /// </summary>
    /// <param name="activeConfig">Current game configuration</param>
    /// <param name="target">Image target that may or may not be required for the given game configuration</param>
    public bool CheckMatches(GameConfiguration activeConfig, ARTarget target)
    {
        if (activeConfig == null || target == null) return false;

        target.IsActive = true; // Mark target as active on ARTarget component

        // Match by the identifier stored in the ScriptableObject (targetId)
        var match = activeConfig.objectStates
            .FirstOrDefault(s => s.targetId == target.name);

        if (match == null)
        {
            Debug.Log($"No expected state for target: {target.name}");
            return false;
        }

        var ok = match.Matches(target);
        Debug.Log($"Target '{target.name}' match result: {ok}");

        if (ok)
        {
            var allMatched = activeConfig.objectStates.All(s =>
            {
                var found = GameObject.Find(s.targetId); // Find the target GameObject in the scene
                var targetComp = found != null ? found.GetComponent<ARTarget>() : null; // Get its ARTarget component
                return targetComp != null && s.Matches(targetComp); // Check if it matches the expected state
            });

            if (allMatched) return true;
        }

        Debug.LogError($"Target '{target.name}' does not match expected states!");
        return false;
    }
}