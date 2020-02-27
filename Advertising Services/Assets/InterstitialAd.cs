using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour
{
    string gameID = "3483542";
    bool test = true;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameID, test);
        Advertisement.Show();
    }
}
