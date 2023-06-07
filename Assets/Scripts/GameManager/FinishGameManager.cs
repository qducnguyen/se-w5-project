using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FinishGameManager : MonoBehaviour
{
    public static FinishGameManager Instance;
    [SerializeField] private GameObject gameOverPanel;
    private TextMeshProUGUI moneyText;
    private TextMeshProUGUI highScoreText;
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

        highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        bool isNewHighScore = ScoreManager.Instance.CheckNewHighScore();
        if (isNewHighScore)
        {
            highScoreText.text = "New highscore!";
        }


        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        int moneyMade = MoneyManager.Instance.GetMoneyAndSaveMoney();
        moneyText.text = "MONEY  " + moneyMade + "$";
    }

    public void RestartGame()
    {
        Time.timeScale = inItTimeScale;
        SceneManager.LoadScene(1);
    }
}
