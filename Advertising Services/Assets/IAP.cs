using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAP : MonoBehaviour
{
    private string coin100 = "coin_100";
    private string coin500 = "coin_500";
    private string removeAds = "remove_ads";

    public static IAP instanceIAP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPurchasing()
    {
        if(isInitialized())
        {
            return;
        }



    }

    private bool isInitialized()
    {
        return true;
    }


    public void BuyRemoveAdverts()
    {

    }

    public void Buy100Coins()
    {

    }

    public void Buy500Coins()
    {

    }

}
