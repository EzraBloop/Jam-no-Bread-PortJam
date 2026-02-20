using UnityEngine;
using UnityEngine.UIElements;

public class Earning : MonoBehaviour
{
    UIDocument uiDocument;
    public int numberOfItems = 100;

    FishInventory inventory;

    void OnEnable()
    {
        //inventory = FishInventory;

        // Get the root visual element
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // Create a ScrollView (or query it from UXML)
        var scrollView = root.Q<ScrollView>("FishList");
        

        // Add a title
        scrollView.Add(new Label("Fish Caught This Day"));

        // Loop to create and add items dynamically
        for (int i = 0; i < inventory.fishList.Count; ++i)
        {
            var newItem = new Label
            {
                //text = inventory.fishList. + i
            }
            // Add the new item directly to the ScrollView
            scrollView.Add(newItem);
        }
    }
}
