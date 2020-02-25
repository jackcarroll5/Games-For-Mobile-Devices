using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdInterstitial : MonoBehaviour
{
    string gameId = "3483542";
    bool testMode = true;

   public void Start()
    {
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
        // Show an ad:
        Advertisement.Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
