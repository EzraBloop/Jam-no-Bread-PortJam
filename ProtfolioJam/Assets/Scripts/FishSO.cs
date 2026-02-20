using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "Fish/Fish Data")]
[Serializable]
public class FishSO : ScriptableObject
{
    public int fishID = 0;
    public enum InitialDirection
    {
        LEFT,
        RIGHT
    }
    public InitialDirection direction = InitialDirection.LEFT;

    [Header("Fish Stats")]
    [Space(10)]
    public string fishName = "feesh";
    public float fishValue = 0;

    [Space(10)]
    public float fishSpeed = 1;
    public float fishMaxVelocity = 1;

    public GameObject prefab;
    public int fishSpawnWeight;

    public float SellFish(int amountCaught)
    {
        float valueReturn = 0;
        valueReturn = fishValue * amountCaught;
        return valueReturn;
    }
}