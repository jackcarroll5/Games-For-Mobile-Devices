using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class AdRewardButton : MonoBehaviour,IUnityAdsListener
{
    private string gameID = "3483542";
    Button button;
    public string myPlacementID = "rewardedVideo";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Unity Ads initialized: " + Advertisement.isInitialized);
        Debug.Log("Unity Ads is supported: " + Advertisement.isSupported);

        button = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        button.interactable = Advertisement.IsReady(myPlacementID);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (button) 
            button.onClick.AddListener(ShowRewardVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, true);
    }

    public void ShowRewardVideo()
    {
        Advertisement.Show(myPlacementID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
   
    }

    public void OnUnityAdsDidError(string message)
    {
       
    }

    public void OnUnityAdsDidStart(string placementId)
    {
       
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log("Rewarded Ad closed");
        if (showResult == ShowResult.Finished)
        {
            if(placementId == myPlacementID)
            { 
            // Reward the user for watching the ad to completion.
            Debug.Log("Rewarded Ad has been finished! Reward Received");
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
            Debug.Log("Rewarded Ad has been skipped! No reward");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
}
