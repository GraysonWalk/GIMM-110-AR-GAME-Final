using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Minigames")]
    [SerializeField] private GameObject[] gameIndex;

    [Header("UI / Interaction")]
    [SerializeField] private TimerBar timerBar;
    [SerializeField] private ConsoleLight consoleLight;

    private IState currentState;
    private int index = 0;

    [Header("Completion Flags")]
    public bool game1Complete;
    public bool game2Complete;
    public bool game3Complete;

    private void Start()
    {
        // Start first minigame state
        SwitchState(new IdleGameState(this, consoleLight, timerBar));
    }

    private void Update()
    {
        currentState?.Update();
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void SwitchState(IState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }

    public void ProgressGame()
    {
        if (consoleLight != null)
            consoleLight.SetStateIdle();

        index++;
        if (index >= gameIndex.Length)
            index = gameIndex.Length - 1;

        if (game1Complete && game2Complete && game3Complete)
        {
            if (timerBar != null)
                timerBar.active = false;

            SwitchState(new SuccessState(this));
            return;
        }

        SwitchState(new IdleGameState(this, consoleLight, timerBar));
    }

            // ---------------- Getters ----------------
    public GameObject[] GetGameIndex() => gameIndex;
    public int GetIndex() => index;
    public TimerBar GetTimerBar() => timerBar;
    public ConsoleLight GetConsoleLight() => consoleLight;
}