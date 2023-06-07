using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelController : MonoBehaviour
{
    public void PressPlayAgainButton() {
        SceneManager.LoadScene("InGameScreen");
        Time.timeScale = 1f;
    }

    public void PressGoShoppingButton() {
        SceneManager.LoadScene("ShoppingSystemScreen");
    }

    public void PressExitButton() {
        SceneManager.LoadScene("ExitScreen");
    }
}
