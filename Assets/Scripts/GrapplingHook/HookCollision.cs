using UnityEngine;

public class HookCollision : MonoBehaviour
{
    public int moneyGet = 0;

    [SerializeField] private AudioSource collectSound;
    
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        switch (collision.GetComponent<TerrainType>().terrainType)
        {
            case TerrainType.TerrainTypes.obstacle:
                break;
            
            case TerrainType.TerrainTypes.monster:
                collectSound.Play();
                moneyGet += 3;
                TerrainCountManager.Instance.countMonster -= 1;
                MoneyManager.Instance.AddMoney(3);
                Destroy(collision.gameObject);
                GrapplingGun.Instance.isGrapplingMonster = true;
                break;

            case TerrainType.TerrainTypes.money:
                break;

            case TerrainType.TerrainTypes._base:
                break;
        }

    }
}
