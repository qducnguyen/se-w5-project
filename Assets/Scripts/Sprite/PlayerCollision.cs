using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int moneyGet = 0;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        switch (collision.GetComponent<TerrainType>().terrainType)
        {
            case TerrainType.TerrainTypes.obstacle:
                FinishGameManager.Instance.FinishGame();
                break;

            case TerrainType.TerrainTypes.money:
                moneyGet += 1;
                MoneyManager.Instance.AddMoney(1);
                Destroy(collision.gameObject);
                break;

            case TerrainType.TerrainTypes._base:
                break;
        }
    }
}
