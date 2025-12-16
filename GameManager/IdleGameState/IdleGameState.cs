using UnityEngine;
using System.Collections;

public class IdleGameState : IState
{
    private readonly GameManager manager;
    private readonly ConsoleLight consoleLight;
    private readonly TimerBar timerBar;

    private int completedGames = 0;
    private float minigameCooldown = 2f;
    private GameObject currentMinigame;

    public IdleGameState(GameManager manager, ConsoleLight consoleLight, TimerBar timerBar)
    {
        this.manager = manager;
        this.consoleLight = consoleLight;
        this.timerBar = timerBar;
    }

    public void OnEnter()
    {
        consoleLight.SetStateIdle();
        timerBar?.StartTimer();
        manager.StartCoroutine(MinigameLoop());
    }

    public void Update()
    {
        if (timerBar != null && timerBar.TimeLeft <= 0f)
        {
            if (manager != null)
                manager.SwitchState(new FailState(manager));
        }
    }

    public void FixedUpdate() { }

    public void OnExit()
    {
        manager.StopAllCoroutines();
        consoleLight.SetStateIdle();
        currentMinigame?.SetActive(false);
    }

    private IEnumerator MinigameLoop()
    {
        GameObject[] minigames = manager.GetGameIndex();
        while (completedGames < minigames.Length && timerBar.TimeLeft > 0f)
        {
            yield return new WaitForSeconds(minigameCooldown);

            bool clicked = false;
            consoleLight.SetStateReady(() => clicked = true);
            yield return new WaitUntil(() => clicked);

            currentMinigame = minigames[completedGames];
            currentMinigame.SetActive(true);

            clicked = false;
            consoleLight.SetStateReady(() => clicked = true);
            yield return new WaitUntil(() => clicked);

            currentMinigame.SetActive(false);
            bool greenDone = false;
            consoleLight.SetStateSuccess(() => greenDone = true);
            yield return new WaitUntil(() => greenDone);

            consoleLight.SetStateIdle();
            completedGames++;
        }

        if (completedGames >= minigames.Length)
            manager.SwitchState(new SuccessState(manager));
    }
}