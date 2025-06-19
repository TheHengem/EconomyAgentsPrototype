using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenActions : MonoBehaviour
{
    // Call this on a button to return to the main scene
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Call this on a button to quit the game
    public void QuitGame()
    {
        // Works in standalone build, not in editor
        Application.Quit();
        Debug.Log("QuitGame() called - will only quit in a built application.");
    }
}
