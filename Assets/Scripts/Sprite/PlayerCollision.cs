using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.GetComponent<TerrainType>().terrainType)
        {
            case TerrainType.TerrainTypes.obstacle:
                FinishGameManager.Instance.FinishGame();
                break;
        }
    }
}
