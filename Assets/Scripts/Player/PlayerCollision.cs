using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int moneyGet = 0;

    [SerializeField] private AudioSource collectSound;
    [SerializeField] private AudioSource dieSound;
    
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        switch (collision.GetComponent<TerrainType>().terrainType)
        {
            case TerrainType.TerrainTypes.obstacle:
                dieSound.Play();
                FinishGameManager.Instance.FinishGame();
                break;
            
            case TerrainType.TerrainTypes.monster:
                dieSound.Play();
                FinishGameManager.Instance.FinishGame();
                break;

            case TerrainType.TerrainTypes.money:
                collectSound.Play();
                moneyGet += 1;
                TerrainCountManager.Instance.countMoney -= 1;
                MoneyManager.Instance.AddMoney(1);
                Destroy(collision.gameObject);
                break;

            case TerrainType.TerrainTypes._base:
                break;
        }
    }
}
