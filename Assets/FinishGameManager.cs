using UnityEngine;

public class FinishGameManager : MonoBehaviour
{
    private void Awake() {
        Instance = this;
    }
    public static FinishGameManager Instance;

    public void FinishGame()
    {
        Time.timeScale = 0;
    }

}
