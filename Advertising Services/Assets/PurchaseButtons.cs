using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButtons : MonoBehaviour
{
    public PurchaseType purchasingType;
    public enum PurchaseType { removeAds, coins100, coins500};

    public void PurchasingButtonClick()
    {
        switch(purchasingType)
        {
            case PurchaseType.removeAds:
                IAP.instanceIAP.BuyRemoveAdverts();
                break;
            case PurchaseType.coins100:
                IAP.instanceIAP.Buy100Coins();
                break;
            case PurchaseType.coins500:
                IAP.instanceIAP.Buy500Coins();
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
