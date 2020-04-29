using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdReward : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "3483542";
    bool test = true;
    public string myPlacementID = "rewardedVideo";
    public Text status;

    public void OnUnityAdsDidError(string message)
    {
        status.text = "Unity Ad failed to load";
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            status.text = "Rewarded Unity Ad is finished";
        }
        else if (showResult == ShowResult.Skipped)
        {
            
        }
        else if (showResult == ShowResult.Failed)
        {
            status.text = "Rewarded Unity Ad failed to show when ad finished";
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        status.text = "Rewarded Unity Ad has started";
    }

    public void OnUnityAdsReady(string placementId)
    {
        status.text = "Rewarded Unity Ad has started";

        if (placementId == myPlacementID)
        {
            Advertisement.Show(myPlacementID);
        }
    }

    // Start is called before the first frame update
   public void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
