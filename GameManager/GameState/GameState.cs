using System.Collections;
using UnityEngine;

public class GameState : IState
{
    private GameManager machine;

    // References from the GameManager
    private int index;
    private GameObject[] games;
    private TimerBar timerBar;
    private ConsoleLight consoleLight;

    private GameObject currentMiniGame;

    public GameState(GameManager machine)
    {
        this.machine = machine;

        // Grab references from the GameManager
        this.index = machine.GetIndex();
        this.games = machine.GetGameIndex();
        this.timerBar = machine.GetTimerBar();
        this.consoleLight = machine.GetConsoleLight();
    }

    public void OnEnter()
    {
        // Activate the current minigame
        if (games != null && index < games.Length)
        {
            currentMiniGame = games[index];
            currentMiniGame.SetActive(true);
        }

        // Timer should already be running, leave as is
        // ConsoleLight remains idle until OnExit()
    }

    public void Update()
    {
        // Check for timeout
        if (timerBar != null && timerBar.TimeLeft <= 0f)
        {
            machine.SwitchState(new FailState(machine));
        }
    }

    public void FixedUpdate()
    {
        // Not needed for this GameState
    }

    public void OnExit()
    {
        // Deactivate the current minigame
        if (currentMiniGame != null)
        {
            currentMiniGame.SetActive(false);
        }

        // Mark completion flag for this minigame
        switch (index)
        {
            case 0:
                machine.game1Complete = true;
                break;
            case 1:
                machine.game2Complete = true;
                break;
            case 2:
                machine.game3Complete = true;
                break;
        }

        // Flash ConsoleLight green, then enable interaction for progression
        if (consoleLight != null)
        {
            consoleLight.SetStateSuccess(() =>
            {
                consoleLight.SetStateReady(() =>
                {
                    machine.ProgressGame();
                });
            });
        }
    }
}