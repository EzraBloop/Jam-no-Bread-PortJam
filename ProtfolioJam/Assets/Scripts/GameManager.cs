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
    public int forceMultiplier;
    public float dayTimer;
    public int turnCost, boostCost, catchNumberCost, forceCost, dayCost;

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
        turnSpeed = 100;
        fallBoostAvailible = false;
        fishCaptureable = 1;
        initialLaunchForce = 100;
        forceMultiplier = 1;
        dayTimer = 60f;

        turnCost = 10;
        boostCost = 50;
        catchNumberCost = 20;
        forceCost = 5;
        dayCost = 30;
    } 

    
}
