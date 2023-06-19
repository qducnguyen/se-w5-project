using UnityEngine;

public class SkinLoader: MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSR;

    private void Awake() {
        if (SkinManager.equippedSkin != null)
            playerSR.sprite = SkinManager.equippedSkin;
    }

}