using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingUIOnEnable : MonoBehaviour
{
    private void OnEnable() {
        StartScreenMoneyManager.Instance.UpdateMoney();    
    }
}
