using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Earning : MonoBehaviour
{
    UIDocument uiDocument;
    ScrollView scrollView;
    Label total, daily;

    GameManager gameManager;

    FishInventory inventory;
    public Dictionary<FishSO, int> fishes;

    void OnEnable()
    {
        
    }

    public void DisplayFish()
    {
        gameManager = GameManager.Instance;
        inventory = FishInventory.Instance;
        fishes = inventory.GetCaughtFishAndAmount();

        // Get the root visual element
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // Create a ScrollView (or query it from UXML)
        scrollView = root.Q<ScrollView>("FishList");


        // Add a title
        scrollView.Add(new Label("Fish Caught This Day"));

        // Loop to create and add items dynamically
        foreach (var fish in fishes)
        {
            var newItem = new Label
            {
                text = fish.Key.fishName + " " + fish.Value
            };

            // Add the new item directly to the ScrollView
            scrollView.Add(newItem);
        }
        //Selling Fish
        daily = root.Q<Label>("Daily");
        total = root.Q<Label>("Total");
        float amount = inventory.SellAllFish();
        gameManager.dailyEarnings = (int)amount;
        gameManager.currentBalance += (int)amount;
        daily.text = $"Daily Earnings: {gameManager.dailyEarnings}";
        total.text = $"Current Balance: {gameManager.currentBalance}";
    }
}
