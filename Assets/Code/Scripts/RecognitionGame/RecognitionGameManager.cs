using System.Linq;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Manages the recognition game logic, including loading configurations,
/// handling AR target recognition events, and determining when the puzzle is solved.
/// </summary>
public class RecognitionGameManager : MonoBehaviour
{
    [Header("Configs")]
    public GameConfiguration[] configurations; // assign in inspector or load via Resources

    [Tooltip("Optional VideoPlayer component used to play the current solution's video")]
    public VideoPlayer videoPlayer; // Optional VideoPlayer to play solution video in games that utilize them

    private GameConfiguration _activeConfig; // Currently active game configuration

    private void Start()
    {
        if (configurations == null || configurations.Length == 0)
            configurations = Resources.LoadAll<GameConfiguration>("GameConfigurations"); // Load from Resources if not assigned

        if (configurations == null || configurations.Length == 0)
        {
            Debug.LogError("No game configurations available.");
            return;
        }

        _activeConfig = configurations[Random.Range(0, configurations.Length)]; // Select a random game configuration
        Debug.Log($"Selected configuration: {_activeConfig.configurationName}");

        if (videoPlayer != null && _activeConfig.videoClip != null) // TODO: Test this to make sure it plays hologram videos as expected
        {
            videoPlayer.clip = _activeConfig.videoClip;
            videoPlayer.Play();
        }
    }

    /// <summary>
    /// Handles the event when an AR target is found. It marks the isActive flag on the ARTarget component as true,
    /// then checks if the found target matches the expected state in the active game configuration. If all targets match their expected states,
    /// it triggers the puzzle solved event.
    /// </summary>
    /// <param name="target">Image target that may or may not be required for the given game configuration</param>
    public void HandleTargetFound(ARTarget target)
    {
        if (_activeConfig == null || target == null) return;
        
        target.isActive = true; // Mark target as active on ARTarget component

        // Match by the identifier stored in the ScriptableObject (targetId)
        var match = _activeConfig.objectStates
            .FirstOrDefault(s => s.targetId == target.name);

        if (match == null)
        {
            Debug.Log($"No expected state for target: {target.name}");
            return;
        }

        var ok = match.Matches(target);
        Debug.Log($"Target '{target.name}' match result: {ok}");

        if (ok)
        {
            var allMatched = _activeConfig.objectStates.All(s =>
            {
                var found = GameObject.Find(s.targetId); // Find the target GameObject in the scene
                var targetComp = found != null ? found.GetComponent<ARTarget>() : null; // Get its ARTarget component
                return targetComp != null && s.Matches(targetComp); // Check if it matches the expected state
            });

            if (allMatched) OnPuzzleSolved();
        }
    }
    /// <summary>
    /// Currently a placeholder for further actions to take when the puzzle is solved.
    /// </summary>
    private void OnPuzzleSolved()
    {
        Debug.Log("Puzzle solved!");
    }
}