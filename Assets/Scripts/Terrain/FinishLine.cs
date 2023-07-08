using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public static FinishLine Instance;
    public GameObject detroyedObject;

    private void Awake() {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Terrain"))
        {
            switch (collision.GetComponent<TerrainType>().terrainType)
            {
                case TerrainType.TerrainTypes._base:
                    TerrainCountManager.Instance.countBase -= 1;
                    break;
                
                case TerrainType.TerrainTypes.money:
                    TerrainCountManager.Instance.countMoney -= 1;
                    break;

                case TerrainType.TerrainTypes.obstacle:
                    TerrainCountManager.Instance.countObstacle -= 1;
                    break;

                case TerrainType.TerrainTypes.monster:
                    TerrainCountManager.Instance.countMonster -= 1;
                    break;
            }
            
            Destroy(collision.gameObject);
        }     
    }
}
