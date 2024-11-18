using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit(); // Closes the game. Only works in a built application.
    }

    public void ResetGame()
    {
        Debug.Log("Resetting game...");
        SceneManager.LoadScene("EscapeRoom");
        GameManager.Instance = null;// Replace "MainScene" with your starting scene's name.
    }
}
