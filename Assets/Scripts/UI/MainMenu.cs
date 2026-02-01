using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource menuAudioSource;

    public AudioClip hoverSound;
    public AudioClip clickSound;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float hoverVolume = 0.5f;
    [Range(0f, 1f)] public float clickVolume = 0.8f;
    public void PlayGame(string sceneName)
    {
        StartCoroutine(LoadSceneWithSound(sceneName));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayHoverSound()
    {
        menuAudioSource.PlayOneShot(hoverSound, hoverVolume);
    }

    public void PlayClickSound()
    {
        menuAudioSource.PlayOneShot(clickSound, clickVolume);
    }

    private IEnumerator LoadSceneWithSound(string sceneName)
    {
        menuAudioSource.PlayOneShot(clickSound, clickVolume);
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(sceneName);
    }
}
