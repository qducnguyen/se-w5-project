using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance;

    private const string skinPref = "skinPref_";

    public static Sprite equippedSkin { get; private set; }

    [SerializeField] private SkinInfo[] allSkins;

    [SerializeField] private Transform skinsInShopPanelsParent;
    [SerializeField] private List<SkinInShop> skinsInShopPanels = new List<SkinInShop>();//SFD

    private Button currentlyEquippedSkinButton;

    private TMP_Text currentlyEquippedSkinText;

    private void Awake()
    {
        Instance = this;

        foreach (Transform s in skinsInShopPanelsParent)
        {
            if (s.TryGetComponent(out SkinInShop skinInShop))
                skinsInShopPanels.Add(skinInShop);
        }


    }

    public void LoadPreviousSkin(){

        foreach (SkinInShop skinPanel in skinsInShopPanels.ToArray()){
            if (skinPanel._skinInfo._skinID.ToString() == PlayerPrefs.GetString(skinPref, SkinInfo.SkinIDs.level1.ToString())){
                equippedSkin = skinPanel._skinInfo._skinSprite;
            }
        }

    }

    public void EquipSkin(SkinInShop skinInfoInShop)
    {
        equippedSkin = skinInfoInShop._skinInfo._skinSprite;
       
        PlayerPrefs.SetString(skinPref, skinInfoInShop._skinInfo._skinID.ToString());

        skinInfoInShop.GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        skinInfoInShop.GetComponentInChildren<Button>().interactable = false;
        
        foreach (SkinInShop skinPanel in skinsInShopPanels.ToArray()){
            if (skinPanel._skinInfo._skinID.ToString() != skinInfoInShop._skinInfo._skinID.ToString()){
                    // skinPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Purchased";
                    skinPanel.GetComponentInChildren<Button>().interactable = true;
                    skinPanel.IsSkinUnlockedandEquipped();
            }
        }
   
    }

}