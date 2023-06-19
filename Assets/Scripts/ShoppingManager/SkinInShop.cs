using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinInShop : MonoBehaviour
{
    [SerializeField] private SkinInfo skinInfo;
    public SkinInfo _skinInfo { get { return skinInfo; } }

    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image skinImage;

    [SerializeField] private bool isSkinUnlocked;
    [SerializeField] private bool isFreeSkin;

    private void Awake()
    {
        skinImage.sprite = skinInfo._skinSprite;

        if (isFreeSkin)
        {
            if (PlayerMoney.Instance.TryRemoveMoney(0))
            {
                PlayerPrefs.SetInt(skinInfo._skinID.ToString(), 1);
            }
        }

        // initial skin
        if (PlayerPrefs.GetInt(skinInfo._skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            buttonText.text = "Purchased";
        }

        IsSkinUnlocked();
    }

    private void IsSkinUnlocked()
    {
        if (PlayerPrefs.GetInt(skinInfo._skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            buttonText.text = "Purchased";
        }
        else
        {
            buttonText.text = "Buy: " + skinInfo._skinPrice;
        }
    }

    public void OnButtonPress()
    {
        if (isSkinUnlocked)
        {
            //purchased
            SkinManager.Instance.EquipSkin(this);
        }
        else
        {
            //buy
            if (PlayerMoney.Instance.TryRemoveMoney(skinInfo._skinPrice))
            {
                PlayerPrefs.SetInt(skinInfo._skinID.ToString(), 1);
                IsSkinUnlocked();
            }
        }
    }
}