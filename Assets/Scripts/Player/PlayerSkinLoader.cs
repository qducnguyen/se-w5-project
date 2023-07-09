using UnityEngine;

public class PlayerSkinLoader: MonoBehaviour
{   

    [SerializeField] private SpriteRenderer playerSR;

    private void Awake() {
        if (SkinManager.equippedSkin != null)
            playerSR.sprite = SkinManager.equippedSkin;
    }

    private void OnEnable() {
        
    }
}