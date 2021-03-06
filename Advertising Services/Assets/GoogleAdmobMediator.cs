﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class GoogleAdmobMediator : MonoBehaviour
{
    private InterstitialAd interstitial;
    public BannerView bannerView;
    private RewardedAd rewardedAd;
    public Text adStatusText;
    private int amountGame = 0;
    private int amountReward = 0;

    // Start is called before the first frame update
    public void Start()
    {
        string appId = "ca-app-pub-3940256099942544~3347511713";

        MobileAds.Initialize(appId);

        RequestBanner();
        RequestInterstitial();
        RequestLoadRewardedAd();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        ShowBanner();
        adStatusText.text = "Banner Ad has loaded";
    }

    public void HandleOnAdFailedToLoad(object sender, EventArgs args)
    {
        adStatusText.text = "Banner Ad failed to load";
        RequestBanner();
    }


    public void HandleOnAdClosed(object sender, EventArgs args)
    { 
        RequestInterstitial();
        adStatusText.text = "Interstitial Ad is closed";
    }

    private void RequestLoadRewardedAd()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
       // this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnUserEarnedReward += HandleOnAdRewarded;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    void OnDestroy()
    {
        rewardedAd.OnAdLoaded -= this.HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward -= this.HandleOnAdRewarded;
        rewardedAd.OnAdClosed -= this.HandleRewardedAdClosed;
    }

    public void HandleOnAdRewarded(object sender, EventArgs args)
    {//user finished watching ad
       /* int points = int.Parse(adStatusText.text);
        points += 50; //add 50 points
        adStatusText.text = points.ToString();*/
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
 
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        RequestLoadRewardedAd();
        adStatusText.text = "Rewarded Ad is closed";
    }

    private void HandleUserEarnedReward(object sender, Reward args)
    {
        double amount = args.Amount;
        amountReward = (int)amount;
    }

    public void UserChoseToWatchAd()
    {
        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
        else
            print("Reward based video ad has not been loaded yet");
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

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        adStatusText.text = "Rewarded Ad failed to load";
        RequestLoadRewardedAd();
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
       
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void ShowBanner()
    {
        bannerView.Show();
    }

    public void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        ////// Called when an ad request failed to load.
       this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        ////// Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        ////// Called when the ad click caused the user to leave the application.
       this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";

        if (this.interstitial != null)
            this.interstitial.Destroy();

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();

        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (interstitial.IsLoaded())
            interstitial.Show();
        else
          print("Interstitial Ad has not been loaded yet");
    }

    // Update is called once per frame
    void Update()
    {
     if(amountReward > 0)
        {
            amountGame += amountReward;
            adStatusText.text = amountGame.ToString();
            amountReward = 0;
        }
    }
}
