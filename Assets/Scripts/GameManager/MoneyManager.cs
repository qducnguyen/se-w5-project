using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    [SerializeField] private int currentMoney;
    public const string prefMoney = "prefMoney";

    private void Awake() 
    {
        Instance = this;
        currentMoney = PlayerPrefs.GetInt("money");
    }

    public void AddMoney(int moneyToAdd) 
    {
        currentMoney += moneyToAdd;
    }

    public int GetMoneyAndSaveMoney() 
    {
        int moneyMade = currentMoney - PlayerPrefs.GetInt(prefMoney);
        PlayerPrefs.SetInt("money", currentMoney);

        return moneyMade;
    }
}
