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

    private const string skinPref = "skinPref_";
    private void Awake()
    {
        skinImage.sprite = skinInfo._skinSprite;

        
        if (isFreeSkin)
        {
            if (StartScreenMoneyManager.Instance.TryRemoveMoney(0))
            {
                PlayerPrefs.SetInt(skinPref + skinInfo._skinID.ToString(), 1);
            }
        }

        // // initial skin
        // if (PlayerPrefs.GetInt(skinPref + skinInfo._skinID.ToString()) == 1)
        // {
        //     isSkinUnlocked = true;
        //     buttonText.text = "Purchased";
        // }

    }

    private void OnEnable() {
             IsSkinUnlockedandEquipped();
   
    }

    public void IsSkinUnlockedandEquipped()
    {
        if (PlayerPrefs.GetInt(skinPref + skinInfo._skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            
            Debug.Log(PlayerPrefs.GetString(skinPref, SkinInfo.SkinIDs.level1.ToString()));

            if (PlayerPrefs.GetString(skinPref, SkinInfo.SkinIDs.level1.ToString()) == skinInfo._skinID.ToString()){
                SkinManager.Instance.EquipSkin(this);
            }   
            else{
                buttonText.text = "Purchased";
            }
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
            if (StartScreenMoneyManager.Instance.TryRemoveMoney(skinInfo._skinPrice))
            {
                PlayerPrefs.SetInt(skinPref + skinInfo._skinID.ToString(), 1);
                IsSkinUnlockedandEquipped();
            }
        }
    }
}