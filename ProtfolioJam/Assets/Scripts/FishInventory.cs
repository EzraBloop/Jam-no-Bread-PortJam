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
        IntializeDictonary();
    }
    private void IntializeDictonary()
    {
        fishList.Clear();
        foreach (var fish in fishScriptableObjects)
        {
            fishList.Add(fish, 0);
        }

    }
    public void EditFishCount(FishSO fish, int change)
    {
        if (!fishList.ContainsKey(fish))
        {
            Debug.LogError($"Fish List does not contain {fish}");
            return;
        }
        fishList[fish] += change;
        Debug.Log($"{fish.fishName} : {fishList[fish].ToString()}");
    }
    private float SellFish(FishSO fish)
    {
        var money = fish.fishValue * fishList[fish];

        fishList[fish] = 0;

        return money;

    }
    public List<FishSO> GetCaughtFish()
    {
        List<FishSO> list = new List<FishSO>();

        foreach (var fish in fishList)
        {
            if (fish.Value > 0)
            {
                list.Add(fish.Key);
            }
        }
        return list;
    }
    public Dictionary<FishSO, int> GetCaughtFishAndAmount()
    {
        Dictionary<FishSO, int> caughtList = new();
        foreach (var fish in fishList)
        {
            if (fish.Value > 0)
            {
                caughtList.Add(fish.Key,fish.Value);
            }
        }
        return caughtList;
    }
    /// <summary>
    /// Sells all fish in inventory and returns value based on the fish sold
    /// </summary>
    /// <returns></returns>
    public float SellAllFish()
    {
        var fishes = GetCaughtFishAndAmount();
        float amount = 0;
        foreach (var fish in fishes)
        {
            amount += SellFish(fish.Key);
        }
        return amount;
    }
}
