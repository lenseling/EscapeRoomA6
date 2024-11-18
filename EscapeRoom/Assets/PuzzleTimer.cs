using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CTools.CTimer;
using Oculus.Interaction;

public class PuzzleTimer : MonoBehaviour
{
    private Timer puzzleTimer;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        // Create a timer with a duration of 10 seconds and subscribe to its events
        if (TimerManager.Instance != null)
        {
            puzzleTimer = TimerManager.Timer(time); // Create a timer

            // Subscribe to OnStart and OnComplete events using the OnStart and OnComplete methods
            puzzleTimer
                .OnStart(() => Debug.Log("Timer started!"))
                .OnComplete(() => Debug.Log("Timer finished!"));

            // Start the timer
            puzzleTimer.Continue();
        }
        else
        {
            Debug.LogWarning("TimerManager instance is not initialized.");
        }
    }

    public void pause()
    {
        puzzleTimer.Pause();
    }

    public void resume()
    {
        puzzleTimer.Continue();
    }

    private void Update()
    {
        if (puzzleTimer != null && puzzleTimer.IsRunning)
        {
            float remainingTime = puzzleTimer.GetRemainingTime();
            Debug.Log($"Remaining Time: {remainingTime} seconds");
        }
    }
}
