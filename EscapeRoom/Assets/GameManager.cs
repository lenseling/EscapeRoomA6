using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Vector3 playerPosition;
    private int paintingsFixed = 0;
    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public AudioClip unlockSound; // The sound clip to play
    private AudioSource audioSource;

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
}
