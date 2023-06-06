using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManager : MonoBehaviour 
{
    private static InterstitialAd _interstitialAd;
    private BannerView bannerView;
    private static RewardedAd _rewardedAd;
    private void Start()
    {
        MobileAds.Initialize(_ => { });
        CreateBannerView();
        LoadInterstitialAd();
        LoadRewardedAd();
    }
    // These ad units are configured to always serve test ads.
#if PLATFORM_ANDROID
    private string _adsBannerId = "ca-app-pub-3940256099942544/6300978111";
    private string _adsinterstitialId = "ca-app-pub-3940256099942544/1033173712";
    private string _adsRewardedId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
  private string _adsBannerId = "ca-app-pub-3940256099942544/2934735716";
    private string _adsinterstitialId = "ca-app-pub-3940256099942544/4411468910";
     private string _adsRewardedId = "ca-app-pub-3940256099942544/1712485313";
#else
  private string _adsBannerId = "unused";
     private string _adsinterstitialId = "unused";
     private string _adsRewardedId = "unused";
#endif
//------------------------------------------------------------------------------------
//Banner Ads
    private void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        if (bannerView != null)
        {
            Debug.Log("DESTROY AD");
        }

        bannerView = new BannerView(_adsBannerId, AdSize.IABBanner, AdPosition.Bottom);
        if (bannerView == null)
        {
            CreateBannerView();
        }

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");
        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);
    }

//------------------------------------------------------------------------------------
//Interstitial Ads
    private void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");
        InterstitialAd.Load(_adsinterstitialId, adRequest,
            (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
            });
    }

    public static void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    //------------------------------------------------------------------------------------
//Rewarded Ads
    private void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(_adsRewardedId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }

    public static void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }
}