using UnityEngine;
using UnityEngine.SceneManagement;
using CTools.CTimer;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Vector3 playerPosition;
    private int paintingsFixed = 0;
    public GameObject lock1;
    public bool puzzle1complete = false;
    public GameObject lock2;
    public bool puzzle2complete = false;

    public GameObject lock3;
    public bool puzzle3complete = false;

    public AudioClip unlockSound; // The sound clip to play
    private AudioSource audioSource;
    public AudioClip musicBoxAudio;
    private AudioSource musicSource;
    public bool musicBoxCompleted = false;
    public GameObject key;
    public GameObject path;
    
    public GameObject musicBox;

    public GameTimer gameTimer;
    private bool gameEnded = false;
    public float timeRemaining = 600f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        musicSource = GetComponent<AudioSource>();
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //// Re-assign references after loading a new scene
        //if (scene.name == "EscapeRoom")
        //{
        //    // Find the locks and music box in the scene
        //    lock1 = GameObject.Find("Lock1");
        //    lock2 = GameObject.Find("Lock2");
        //    lock3 = GameObject.Find("Lock3");
        //    musicBox = GameObject.Find("MusicBox");

        //    // Make sure they are not null
        //    if (lock1 == null || lock2 == null || lock3 == null || musicBox == null)
        //    {
        //        Debug.LogWarning("One or more objects (locks or music box) are missing in the MainScene.");
        //    }
        //}
    }

    public void CompletePuzzle()
    {
        // Get the original scene name and load it
        string previousScene = PlayerPrefs.GetString("PreviousScene", "MainScene");
        SceneManager.LoadScene(previousScene);
    }

    public void fixPainting()
    {
        paintingsFixed++;
        Debug.Log("Paintings fixed: " + paintingsFixed);
        if (paintingsFixed >= 3) {
            puzzle1complete = true;
            Debug.Log("Paintings Complete! Unlock");
            lock1.SetActive(false);
            // Play the unlock sound
            if (unlockSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(unlockSound);
            }
            else
            {
                Debug.LogWarning("Unlock sound or AudioSource is missing!");
            }
        }
        
    }

    public void musicBoxComplete()
    {
        puzzle2complete = true;
        AssignObjects();
        if (unlockSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(unlockSound);
        }
        else
        {
            Debug.LogWarning("Unlock sound or AudioSource is missing!");
        }

        if (lock2 != null)
        {
            lock2.SetActive(false);
        }
        if (musicBox != null) { musicBox.SetActive(false); }

        if (musicSource != null)
        {
            musicSource.volume = 0.5f;
            musicSource.PlayOneShot(musicBoxAudio);

        }
        else
        {
            Debug.LogWarning("Music box audio is missing!");
        }
    }

    public void KeyCollected()
    {
        puzzle3complete = true;
        if (unlockSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(unlockSound);
        }
        else
        {
            Debug.LogWarning("Unlock sound or AudioSource is missing!");
        }

        if (lock3 != null)
        {
            lock3.SetActive(false);
        }
        if (key != null)
        {
            key.SetActive(false);
        }
        if (path != null) {
            path.SetActive(false);

        }

    }

    private void Update()
    {
        
        if (!gameEnded)
        {
            if (musicBoxCompleted)
            {
                musicBoxCompleted = false;
                musicBoxComplete();
            }
            if (lock1 != null && lock2 != null && lock3 != null && !lock1.activeSelf && !lock2.activeSelf && !lock3.activeSelf)
            {
                WinGame();
            }
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Countdown by time
                gameTimer.DisplayTime(timeRemaining);
            }
            else
            {
                gameEnded = true;
                EndGame(); // Time's up!
            }
        }

        

    }

    private void AssignObjects()
    {
        // Re-assign references after loading a new scene
        //if (scene.name == "EscapeRoom")
        //{
            // Find the locks and music box in the scene
        lock1 = GameObject.Find("Lock1");
        lock2 = GameObject.Find("Lock2");
        lock3 = GameObject.Find("Lock3");
        

        musicBox = GameObject.Find("MusicBox");
        key = GameObject.Find("Key_Golden");
        path = GameObject.Find("PathToTheKey");

        if (puzzle1complete)
        {
            lock1.SetActive(false);
        }
        if (puzzle2complete)
        {
            lock2.SetActive(false);
            musicBox.SetActive(false);
        }
        if (puzzle3complete)
        {
            lock3.SetActive(false);
            key.SetActive(false);
            if (path != null)
            {
                path.SetActive(false);

            }
        }

        // Make sure they are not null
        if (lock1 == null || lock2 == null || lock3 == null || musicBox == null)
            {
                Debug.LogWarning("One or more objects (locks or music box) are missing in the MainScene.");
            }
        //}
    }

    private void EndGame()
    {

        Debug.Log("Game over");
        SceneManager.LoadScene("FailState");
        
        
    }
    private void WinGame()
    {
        SceneManager.LoadScene("Success");
    }
}
