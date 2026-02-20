using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Earning : MonoBehaviour
{
    UIDocument uiDocument;
    ScrollView scrollView;

    FishInventory inventory;
    public Dictionary<FishSO, int> fishes;

    void OnEnable()
    {
  
    }

    public void DisplayFish()
    {
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
    }
}
