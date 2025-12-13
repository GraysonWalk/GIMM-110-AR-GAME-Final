using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    // Event triggered when the timer finishes
    public delegate void TimerFinishedHandler();

    public float TotalTime = 10f; // Total time for the countdown in seconds
    private bool _isRunning;
    private float _remainingTime;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (_isRunning)
        {
            _remainingTime -= Time.deltaTime;
            if (_remainingTime <= 0f)
            {
                _remainingTime = 0f;
                _isRunning = false;
                OnTimerFinished?.Invoke();
            }
        }
    }

    public event TimerFinishedHandler OnTimerFinished;

    // Starts the countdown timer
    public void StartTimer()
    {
        _isRunning = true;
    }

    // Stops the countdown timer
    public void StopTimer()
    {
        _isRunning = false;
    }

    // Resets the timer to the total time
    public void ResetTimer()
    {
        _remainingTime = TotalTime;
        _isRunning = false;
    }

    // Gets the remaining time
    public float GetRemainingTime()
    {
        return _remainingTime;
    }

    // Checks if the timer is running
    public bool IsRunning()
    {
        return _isRunning;
    }
}