using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayScreenController : MonoBehaviour
{
    public void PressBackButton() {
        SceneManager.LoadScene("StartScreen");
    }

    public void PressStartButton() {
        SceneManager.LoadScene("InGameScreen");
    }
}
