using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAP : MonoBehaviour,IStoreListener
{
    private string coin100 = "coins_100";
    private string coin500 = "coins_500";
    private string removeAds = "remove_ads";

    public static IAP instanceIAP;

    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    // Start is called before the first frame update
    void Start()
    {
        if (storeController == null) { 
            InitPurchasing(); 
        }
    }

    private void Awake()
    {
        TestSingle();
    }

    private void TestSingle()
    {
        if (instanceIAP != null) { 
            Destroy(gameObject);
            return; 
        }

        instanceIAP = this;
        DontDestroyOnLoad(gameObject);
    }

    public void InitPurchasing()
    {
        if(isInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(removeAds, ProductType.NonConsumable);
        builder.AddProduct(coin100, ProductType.Consumable);
        builder.AddProduct(coin500, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private bool isInitialized()
    {
        return storeController != null && storeExtensionProvider != null;
    }


    public void BuyRemoveAdverts()
    {
        BuyProductID(removeAds);
    }

    public void Buy100Coins()
    {
        BuyProductID(coin100);
    }

    public void Buy500Coins()
    {
        BuyProductID(coin500);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product productIAPItem = storeController.products.WithID(productId);

            if (productIAPItem != null && productIAPItem.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing the product asychronously: '{0}'", productIAPItem.definition.id));
                storeController.InitiatePurchase(productIAPItem);
            }
            else
            {
                Debug.Log("BuyProductID: FAILED. No purchasing of product, either is not found or not available for purchasing that item");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAILED. Not initialized.");
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
        {
            Debug.Log("Removal of the ads successful");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, coin100, StringComparison.Ordinal))
        {
            Debug.Log("Give user 100 byte coins");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, coin500, StringComparison.Ordinal))
        {
            Debug.Log("Give user 500 byte coins");
        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }

    private bool IsInitialized()
    {
        return storeController != null && storeExtensionProvider != null;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed ReasonForInitializationFailure:" + error);
    }


    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.LogError(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", i.definition.storeSpecificId, p));
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInit: PASSED");
        storeController = controller;
        storeExtensionProvider = extensions;
    }
}
