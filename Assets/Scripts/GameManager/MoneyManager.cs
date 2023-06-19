using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    [SerializeField] public int currentMoney;
    public const string prefMoney = "prefMoney";
    public const string preftotalMoney = "prefTotalMoney";

    private void Awake() 
    {
        Instance = this;
        currentMoney = PlayerPrefs.GetInt(preftotalMoney);
    }

    public void AddMoney(int moneyToAdd) 
    {
        currentMoney += moneyToAdd;
    }

    public int GetMoneyAndSaveMoney() 
    {
        int moneyMade = currentMoney - PlayerPrefs.GetInt(prefMoney);
        PlayerPrefs.SetInt(preftotalMoney, currentMoney);

        return moneyMade;
    }
}
