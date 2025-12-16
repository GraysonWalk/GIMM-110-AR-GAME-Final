using UnityEngine;
using UnityEngine.Video;

/// <summary>
///     Manages the recognition game logic, including loading configurations,
///     handling AR target recognition events, and determining when the puzzle is solved.
/// </summary>
public class RecognitionGameManager : MonoBehaviour
{
    [Header("Configs")]
    public GameConfiguration[] configurations; // assign in inspector or load via Resources

    [Tooltip("Optional VideoPlayer component used to play the current solution's video")]
    public VideoPlayer videoPlayer; // Optional VideoPlayer to play solution video in games that utilize them

    [SerializeField] private MatchChecker matchChecker;

    private GameConfiguration _activeConfig; // Currently active game configuration

    private void Start()
    {
        if (configurations == null || configurations.Length == 0)
            configurations =
                Resources.LoadAll<GameConfiguration>("GameConfigurations"); // Load from Resources if not assigned

        if (configurations == null || configurations.Length == 0)
        {
            Debug.LogError("No game configurations available.");
            return;
        }

        _activeConfig = configurations[Random.Range(0, configurations.Length)]; // Select a random game configuration
        Debug.Log($"Selected configuration: {_activeConfig.configurationName}");

        if (videoPlayer != null &&
            _activeConfig.videoClip != null) // TODO: Test this to make sure it plays hologram videos as expected
        {
            videoPlayer.clip = _activeConfig.videoClip;
            videoPlayer.Play();
        }

        if (matchChecker == null)
        {
            matchChecker = GetComponent<MatchChecker>();
            if (matchChecker == null) Debug.LogError("MatchChecker component is missing.");
        }
    }

    private void HandleTargetFound(ARTarget target)
    {
        var puzzleSolved = matchChecker.CheckMatches(_activeConfig, target);

        if (puzzleSolved) OnPuzzleSolved();
    }

    /// <summary>
    ///     Currently a placeholder for further actions to take when the puzzle is solved.
    /// </summary>
    private void OnPuzzleSolved()
    {
        Debug.Log("Puzzle solved!");
    }
}