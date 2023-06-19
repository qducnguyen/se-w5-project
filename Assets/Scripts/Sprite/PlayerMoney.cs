using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;

    [SerializeField] private int playerMoney;
    TextMeshProUGUI moneyText;

    public const string preftotalMoney = "prefTotalMoney";

    private void Awake()
    {
        Instance = this;
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
    }

    private void Start() 
    {
        playerMoney = PlayerPrefs.GetInt(preftotalMoney);
        moneyText.text = "Money: " + playerMoney;
    }

    public bool TryRemoveMoney(int moneyToRemove)
    {
        if (playerMoney >= moneyToRemove)
        {
            playerMoney -= moneyToRemove;
            PlayerPrefs.SetInt(preftotalMoney, playerMoney);
            moneyText.text = "Money: " + playerMoney;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateFromWatchAds()
    {
        PlayerPrefs.SetInt(preftotalMoney, playerMoney + 1);
        playerMoney = PlayerPrefs.GetInt(preftotalMoney);
        moneyText.text = "Money: " + playerMoney;
    }
}