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
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
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
