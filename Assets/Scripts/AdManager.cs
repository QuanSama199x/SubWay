using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private static AdManager instance;
    public static AdManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<AdManager>();
            return instance;
        }
    }
    private BannerView bannerAd;
    // Start is called before the first frame update
    void Start()
    {
        EventScript.Instance.GameOver.AddListener(GameOver);

        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        RequestInterstitial();


    }
    public void RequestBanner()
    {
        
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        this.bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        this.bannerAd.OnAdLoaded += BannerAd_OnAdLoaded;
        this.bannerAd.OnAdFailedToLoad += BannerAd_OnAdFailedToLoad;
        this.bannerAd.OnAdClosed += BannerAd_OnAdClosed;
     
        this.bannerAd.LoadAd(this.CreateAdRequest());
    

        
    }
    private AdRequest CreateAdRequest()
    {
        return new  AdRequest.Builder().Build();
    }
    private void BannerAd_OnAdClosed(object sender, EventArgs e)
    {
        Debug.LogError("ad closed");
    }

    private void BannerAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.LogError("ad failed to load ");
    }

    private void BannerAd_OnAdLoaded(object sender, EventArgs e)
    {
        this.bannerAd.Show();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private InterstitialAd interstitial;

    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
     // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    private void GameOver()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

}


