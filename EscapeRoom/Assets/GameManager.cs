using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Vector3 playerPosition;

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
    }

    public void CompletePuzzle()
    {
        // Get the original scene name and load it
        string previousScene = PlayerPrefs.GetString("PreviousScene", "MainScene");
        SceneManager.LoadScene(previousScene);
    }
}
