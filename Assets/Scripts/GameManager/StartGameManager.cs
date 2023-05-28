using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    private void Awake() {
        Instance = this;
    }
    public static StartGameManager Instance;

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

}
