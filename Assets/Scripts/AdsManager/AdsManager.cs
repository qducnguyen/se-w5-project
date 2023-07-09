using System;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager Instance;

    private const string androidGameID = "5327089";
    private const string iosGameID = "5327088";
    public string GAME_ID; 

    private const string REWARDED_VIDEO_PLACEMENT = "rewardedVideo";

    private bool testMode = true;

    public delegate void DebugEvent(string msg);
    public static event DebugEvent OnDebugLog;

   private void Awake()
    {
        Instance = this;

        if (Advertisement.isSupported)
        {
            if (Application.platform == RuntimePlatform.Android)
                Debug.Log("Have ads for android on device");
                GAME_ID = androidGameID;

            if (Application.platform == RuntimePlatform.IPhonePlayer)
                Debug.Log("Have ads for ios");
                GAME_ID = iosGameID;

            if (Application.isEditor)
                Debug.Log("Have ads for android on editor");
                GAME_ID = androidGameID;
        }
    }

    public void Initialize()
    {
        if (Advertisement.isSupported)
        {
            DebugLog(Application.platform + " supported by Advertisement");
        }
        Advertisement.Initialize(GAME_ID, testMode, this);
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        DebugLog("Init Success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        DebugLog($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        DebugLog($"Load Success: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        DebugLog($"Load Failed: [{error}:{placementId}] {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        DebugLog($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        DebugLog($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
        StartScreenMoneyManager.Instance.UpdateFromWatchAds();
    }
    #endregion

    public void OnGameIDFieldChanged(string newInput)
    {
        GAME_ID = newInput;
    }

    public void ToggleTestMode(bool isOn)
    {
        testMode = isOn;
    }

    void DebugLog(string msg)
    {
        OnDebugLog?.Invoke(msg);
        Debug.Log(msg);
    }
}
