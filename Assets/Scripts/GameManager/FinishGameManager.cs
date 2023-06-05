using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishGameManager : MonoBehaviour
{
    public static FinishGameManager Instance;
    [SerializeField] private GameObject gameOverPanel;
    private Text moneyText;
    private Text highScoreText;
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

        highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        bool isNewHighScore = ScoreManager.Instance.CheckNewHighScore();
        if (isNewHighScore)
        {
            highScoreText.text = "New highscore!";
        }


        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        int moneyMade = MoneyManager.Instance.GetMoneyAndSaveMoney();
        moneyText.text = "Money: " + moneyMade;
    }

    public void RestartGame()
    {
        Time.timeScale = inItTimeScale;
        SceneManager.LoadScene(1);
    }
}
