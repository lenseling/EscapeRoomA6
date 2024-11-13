using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CTools.CTimer;

public class PuzzleManager : MonoBehaviour
{
    public int paintingCnt;        // total number of paintings in the scene
    private List<GameObject> paintings;
    private int paintingFinished;
    private Timer puzzleTimer;

    // Start is called before the first frame update
    void Start()
    {
        paintings = new List<GameObject>();
        paintingFinished = 0;

        // Create a timer with a duration of 10 seconds and subscribe to its events
        if (TimerManager.Instance != null)
        {
            puzzleTimer = TimerManager.Timer(10.0f); // Create a 10-second timer

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

    public bool containPainting(GameObject p)
    {
        return paintings.Contains(p);
    }

    public bool removePainting(GameObject p)
    {
        paintingFinished++;
        if (paintingFinished == paintingCnt)
            acquireKey();
        return paintings.Remove(p);
    }

    public bool addPainting(GameObject painting)
    {
        if (painting.tag != "painting" || paintings.Contains(painting)) {
            Debug.Log("Not a painting or the painting already exists.");
            return false;
        }

        paintings.Add(painting);
        return true;
    }

    public void acquireKey()
    {
        Debug.Log("You get the key!");
    }
}
