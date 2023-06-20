using UnityEngine;
using TMPro;

public class StartScreenMoneyManager : MonoBehaviour
{
    public static StartScreenMoneyManager Instance;

    [SerializeField] private TMP_Text moneyText; 

    private int playerMoney;

    public const string preftotalMoney = "prefTotalMoney";

    private void Awake()
    {
        Instance = this;
        playerMoney = PlayerPrefs.GetInt(preftotalMoney);
    }

    private void Start() 
    {
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

        // TODO
        PlayerPrefs.SetInt(preftotalMoney, playerMoney + 1);
        playerMoney = PlayerPrefs.GetInt(preftotalMoney);
        moneyText.text = "Money: " + playerMoney;
    }
}