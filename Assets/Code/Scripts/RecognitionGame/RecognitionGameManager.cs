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
    [SerializeField] private GameObject systemsHolder;
    [SerializeField] public GameManager gameManager;

    [SerializeField]
    private PasswordInputBase passwordInput; // Optional password input component for password-based games

    public ConsoleLight consoleLight;


    private GameConfiguration _activeConfig; // Currently active game configuration
    private bool _correctPassword;
    private bool _puzzleSolved;

    private void Awake()
    {
        var targets = systemsHolder != null
            ? systemsHolder.GetComponentsInChildren<ARTarget>(true)
            : FindObjectsOfType<ARTarget>(true);

        foreach (var target in targets)
            target.OnActivated += HandleTargetFound;
    }

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

        if (passwordInput != null && !string.IsNullOrEmpty(_activeConfig.password))
        {
            passwordInput.SetPassword(_activeConfig.password);
            passwordInput.OnCorrect += OnCorrectPassword;
        }
    }

    private void HandleTargetFound(ARTarget target)
    {
        if (_puzzleSolved) return;

        var solved = matchChecker.CheckMatches(_activeConfig, target, systemsHolder);
        if (!solved) return;

        _puzzleSolved = true;

        // Unsubscribe to prevent further state changes from affecting the solved state
        var targets = systemsHolder != null
            ? systemsHolder.GetComponentsInChildren<ARTarget>(true)
            : FindObjectsOfType<ARTarget>(true);

        foreach (var t in targets)
            t.OnActivated -= HandleTargetFound;

        OnAllTargetsFound();
    }

    private void OnAllTargetsFound()
    {
        _puzzleSolved = true;
        OnPuzzleSolved();
    }

    private void OnCorrectPassword()
    {
        _correctPassword = true;
        OnPuzzleSolved();
    }

    private void OnPuzzleSolved()
    {
        if (!_puzzleSolved ||
            (_activeConfig.password != null && _activeConfig.password != "" && !_correctPassword)) return;
        Debug.Log("RecognitionGameManager: Puzzle solved!");
        if (gameManager != null) gameManager.OnRecognitionGameComplete();
    }
}