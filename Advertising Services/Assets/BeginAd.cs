using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BeginAd : MonoBehaviour
{
    string gameID = "3483542";
    bool test = true;
    string gameId = null;

    // Start is called before the first frame update
    void Start()
    {  
        gameId = gameID;
        Advertisement.Initialize(gameId, test);
        Debug.Log("Unity Ads initialized: " + Advertisement.isInitialized);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
