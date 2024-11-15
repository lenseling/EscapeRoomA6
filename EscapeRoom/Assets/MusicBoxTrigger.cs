using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBoxTrigger : MonoBehaviour
{
    public string musicBoxSceneName = "MusicBoxPuzzle";

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered music box trigger area"); // Debug message
            // Save the current scene name
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);

            // Load the music box puzzle scene
            SceneManager.LoadScene(musicBoxSceneName);
        }
    }
}
