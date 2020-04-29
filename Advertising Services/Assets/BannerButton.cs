using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class BannerButton : MonoBehaviour, IUnityAdsListener
{
    public string gameId = "3483542";
    public string placementId = "banner";
    public bool testMode = true;
    Button button;
    public Text banner;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Unity Ads initialized: " + Advertisement.isInitialized);
        Debug.Log("Unity Ads is supported: " + Advertisement.isSupported);
        Debug.Log("Unity Ads test mode enabled: " + testMode);

        button = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        button.interactable = Advertisement.IsReady();

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (button)
            button.onClick.AddListener(ShowBanner);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowBanner()
    {
        StartCoroutine(ShowBannerWhenReady());
    }

    public void BannerAdChoice()
    {

    }

    public IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(placementId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        banner.text = "Banner Unity Ad is ready!";
    }

    public void OnUnityAdsDidError(string message)
    {
        banner.text = "Banner Unity Ad has suffered from an error!";
    }

    public void OnUnityAdsDidStart(string placementId)
    {
       
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
           
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
           
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
}
