using System.Collections.Generic;
using UnityEngine;

public class FishInventory : MonoBehaviour
{
    public static FishInventory Instance;

    public List<FishSO> fishScriptableObjects = new List<FishSO>();
    public Dictionary<FishSO, int> fishList = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }
    private void IntializeDictonary() 
    {
        fishList.Clear();
        foreach (var  fish in fishScriptableObjects)
        {
            fishList.Add(fish, 0);
        }

    }
    public void EditFishCount(FishSO fish, int change)
    {
        var feesh = fishList[fish] += change;
    }
    public float SellFish(FishSO fish)
    {
        var money = fish.fishValue * fishList[fish];

        fishList[fish] = 0;

        return money;
        
    }
}
