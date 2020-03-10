using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdmobMediator : MonoBehaviour
{
    private BannerView banner;

    [SerializeField]
    private string appId = "ca-app-pub-9359698886030361~1643908833";

    [SerializeField]
    private string bannerId = "ca-app-pub-9359698886030361/7521538589";

    [SerializeField]
    private string intersititalId= "ca-app-pub-9359698886030361/5746805378";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickShowBannerAd()
    {
        this.BannerRequest();
    }

    public void OnClickShowInterstitial()
    {
        this.InterstitialRequest();
    }


    private void BannerRequest()
    {
        banner = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);

        AdRequest adRequest = new AdRequest.Builder().Build();

        banner.LoadAd(adRequest);
        
    }

    private void InterstitialRequest()
    {
        InterstitialAd ad = new InterstitialAd(intersititalId);

        AdRequest adRequest = new AdRequest.Builder().Build();

        ad.LoadAd(adRequest);
    }

    private void Awake()
    {
        MobileAds.Initialize(appId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
