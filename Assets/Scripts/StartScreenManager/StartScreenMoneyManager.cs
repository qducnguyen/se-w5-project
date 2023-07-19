using UnityEngine;
using TMPro;

public class StartScreenMoneyManager : MonoBehaviour
{
    public static StartScreenMoneyManager Instance;

    [SerializeField] private TMP_Text moneyText; 

    private int playerMoney;

    public const string preftotalMoney = "prefTotalMoney";

    private const string skinPref = "skinPref_";

    public string[] SkinIDs = {"level1", "level2", "level3", "level4"};

    private void Awake()
    {
        Instance = this;
        playerMoney = PlayerPrefs.GetInt(preftotalMoney, 0);
        
    }

    
    public void GetMoneyfromPrefs(){
        playerMoney = PlayerPrefs.GetInt(preftotalMoney, 0);
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
        playerMoney = PlayerPrefs.GetInt(preftotalMoney, 0);
        moneyText.text = "Money: " + playerMoney;
    }

    public void UpdateMoney(){
        playerMoney = PlayerPrefs.GetInt(preftotalMoney, 0);
        moneyText.text = "Money: " + playerMoney;
    }
}