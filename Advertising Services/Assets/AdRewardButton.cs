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
    public Text status;

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
        status.text = "Rewarded Unity Ad is ready!";
    }

    public void OnUnityAdsDidError(string message)
    {
        status.text = "Rewarded Unity Ad has suffered from an error!";
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        status.text = "Rewarded Unity Ad has started!";
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        status.text = "Rewarded Unity Ad is finished!";
        Debug.Log("Rewarded Ad closed");
        if (showResult == ShowResult.Finished)
        {
            if(placementId == myPlacementID)
            {
                // Reward the user for watching the ad to completion.
                Debug.Log("Rewarded Ad has been finished! Reward Received!");
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            status.text = "Rewarded Unity Ad has been skipped! No reward!";
            // Do not reward the user for skipping the ad.
            Debug.Log("Rewarded Ad has been skipped! No reward!");
        }
        else if (showResult == ShowResult.Failed)
        {
            status.text = "Rewarded Unity Ad failed to finish! No reward!";
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
}
