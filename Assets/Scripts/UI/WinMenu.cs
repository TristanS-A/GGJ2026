using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    // Play the game again.
    public void PlayAgain(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Quit the game.
    public void QuitGame()
    {
        Application.Quit();
    }
}
