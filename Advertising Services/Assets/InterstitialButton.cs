using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InterstitialButton : MonoBehaviour, IUnityAdsListener
{
    string gameId = "3483542";
    bool testMode = true;
    Button button;
    public string interstitialID = "interstitial";

    public void OnUnityAdsDidError(string message)
    {
   
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log("Interstitial Ad closed");
        if (showResult == ShowResult.Finished)
        {
            if(placementId == interstitialID)
            {
            // Reward the user for watching the ad to completion.
            Debug.Log("Ad has been finished! Reward Received");
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
            Debug.Log("Ad has been skipped! No reward");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
       
    }

    public void OnUnityAdsReady(string placementId)
    {
    
    }

    public void ShowInterstitial()
    {
        Advertisement.Show();
    }

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        button.interactable = Advertisement.IsReady(interstitialID);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (button)
            button.onClick.AddListener(ShowInterstitial);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
