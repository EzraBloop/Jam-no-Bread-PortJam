using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int currentBalance, dailyEarnings;
    public List<FishSO> fishData;

    public float turnSpeed;
    public bool fallBoostAvailible;
    public int fishCaptureable;
    public int initialLaunchForce;

    private void Awake()
    {
        Instance = this;
        turnSpeed = 100;
        fallBoostAvailible = false;
        fishCaptureable = 1;
        initialLaunchForce = 800;

    } 
}
