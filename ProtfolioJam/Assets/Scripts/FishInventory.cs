using System.Collections.Generic;
using UnityEditor.Rendering;
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
        foreach (var  fish in fishScriptableObjects)
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
    public float SellFish(FishSO fish)
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
}
