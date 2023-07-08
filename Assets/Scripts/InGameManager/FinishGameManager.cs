using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishGameManager : MonoBehaviour
{
    public static FinishGameManager Instance;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text highScoreText;
    private float inItTimeScale;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        inItTimeScale = Time.timeScale;
        }

    public void FinishGame()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);

        bool isNewHighScore = ScoreManager.Instance.CheckNewHighScore();
        if (isNewHighScore)
        {
            highScoreText.text = "New Highscore!";
        }
        else{
            highScoreText.text = null;
        }

        int moneyMade = MoneyManager.Instance.GetMoneyAndSaveMoney();
        moneyText.text = "Total Money: " + moneyMade;
    }

    public void RestartGameButton()
    {
        Time.timeScale = inItTimeScale;
        SceneManager.LoadScene("InGameScreen");
    }

    public void BacktoMainScreenButton()
    {
        Time.timeScale = inItTimeScale;
        SceneManager.LoadScene("StartScreen");
    }
}
