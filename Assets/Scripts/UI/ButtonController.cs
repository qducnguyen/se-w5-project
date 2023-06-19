using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void OnPressButton(string screenName) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(screenName);
    }
}
