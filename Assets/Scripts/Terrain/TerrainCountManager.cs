using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCountManager : MonoBehaviour
{
    public static TerrainCountManager Instance;
    [SerializeField] public int countBase = 1;
    [SerializeField] public int countMoney = 1;
    [SerializeField] public int countObstacle = 1;
    [SerializeField] public int countMonster = 1;

    private void Awake() {
        Instance = this;
    }


}