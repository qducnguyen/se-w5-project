using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject pauseButton;

    public void resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void restart()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("MainScene");
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void back()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("StartScreen");
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }
}
