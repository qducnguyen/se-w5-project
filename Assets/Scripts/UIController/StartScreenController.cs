using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController: MonoBehaviour
{
    public void PressPlayButton() {
        SceneManager.LoadScene("InGameScreen");
    }

    public void PressHowToPlayButton() {
        SceneManager.LoadScene("HowToPlayScreen");
    }

    public void PressSettingsButton() {
        SceneManager.LoadScene("SettingsScreen");
    }

    public void PressExitButton() {
        SceneManager.LoadScene("ExitScreen");
    }
}
