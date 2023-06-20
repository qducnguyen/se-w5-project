using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsUI : MonoBehaviour
{

    public AdsManager unityAdsManager;
    public Button showRewardedBtn;

    // public Text debugLogText;

    private string textLog = "DEBUG LOG: \n";

    private void Awake()
    {
        if (unityAdsManager == null)
        {
            unityAdsManager = FindObjectOfType<AdsManager>();
        }
        unityAdsManager.Initialize();
    }

    private void Start()
    {
        // showRewardedBtn.onClick.AddListener(unityAdsManager.Initialize);
        showRewardedBtn.onClick.AddListener(unityAdsManager.LoadRewardedAd);
        showRewardedBtn.onClick.AddListener(unityAdsManager.ShowRewardedAd);
    }

    private void OnEnable()
    {
        AdsManager.OnDebugLog += HandleDebugLog;
    }

    private void OnDisable()
    {
        AdsManager.OnDebugLog -= HandleDebugLog;
    }


    void HandleDebugLog(string msg)
    {
        textLog += "\n" + msg + "\n";
    }
}
