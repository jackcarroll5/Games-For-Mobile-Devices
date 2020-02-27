using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdReward : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "3483542";
    bool test = true;
    public string myPlacementID = "rewardedVideo";

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
           
        }
        else if (showResult == ShowResult.Skipped)
        {
            
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
        if(placementId == myPlacementID)
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
