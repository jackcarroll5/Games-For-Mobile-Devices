using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleAdmobMediator : MonoBehaviour
{
    public BannerView bannerView;
    // Start is called before the first frame update
   public void Start()
    {
        string appId = "ca-app-pub-3940256099942544~3347511713";

        MobileAds.Initialize(appId);

        RequestBanner();
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        ////// Called when an ad request has successfully loaded.
       this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        ////// Called when an ad request failed to load.
       this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        ////// Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        ////// Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        ////// Called when the ad click caused the user to leave the application.
       this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
