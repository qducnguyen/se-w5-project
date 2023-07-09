using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCountManager : MonoBehaviour
{
    public static TerrainCountManager Instance;
    [SerializeField] public int countBase = 0;
    [SerializeField] public int countMoney = 0;
    [SerializeField] public int countObstacle = 0;
    [SerializeField] public int countMonster = 0;

    private void Awake() {
        Instance = this;
    }


}