using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shop : MonoBehaviour
{
    UIDocument ui;
    VisualElement ve;

    public GameManager instance;
    private Button turn, boost, catchNumber, force;
    public int turnCost, boostCost, catchNumberCost, forceCost;
    private Label shopText;
    private int counter;

    private void Awake()
    {
        instance = GameManager.Instance;

        turnCost = 10;
        boostCost = 50;
        catchNumberCost = 20;
        forceCost = 5;

        ui = GetComponent<UIDocument>();
        ve = ui.rootVisualElement as VisualElement;

        turn = ui.rootVisualElement.Q<Button>("TurnUpgrade");
        boost = ui.rootVisualElement.Q<Button>("BoostUpgrade");
        catchNumber = ui.rootVisualElement.Q<Button>("CatchUpgrade");
        force = ui.rootVisualElement.Q<Button>("ForceUpgrade");
        shopText = ui.rootVisualElement.Q<Label>("ShopKeepText");
    }

    private void Start()
    {

        if (instance.turnSpeed == 133)
        {
            turn.SetEnabled(false);
        }

        if(instance.fallBoostAvailible == true)
        {
            boost.SetEnabled(false);
        }

        if (instance.fishCaptureable == 5)
        {
            catchNumber.SetEnabled(false);
        }

        if (instance.forceMultiplier == 10)
        {
            force.SetEnabled(false);
        }
    }
    private void OnEnable()
    {
        turn.RegisterCallback<ClickEvent>(onTurnUpgrade);
        boost.RegisterCallback<ClickEvent>(onBoostUpgrade);
        catchNumber.RegisterCallback<ClickEvent>(onCatchNumberUpgrade);
        force.RegisterCallback<ClickEvent>(onForceUpgrade);
    }

    private void OnDisable()
    {
        turn.UnregisterCallback<ClickEvent>(onTurnUpgrade);
        boost.UnregisterCallback<ClickEvent>(onBoostUpgrade);
        catchNumber.UnregisterCallback<ClickEvent>(onCatchNumberUpgrade);
        force.UnregisterCallback<ClickEvent>(onForceUpgrade);
    }

    public void onTurnUpgrade(ClickEvent evt)
    {
        if (instance.currentBalance > turnCost)
        {
            if (instance.turnSpeed < 133)
            {
                instance.currentBalance -= turnCost;
                instance.turnSpeed += 11;
                turnCost *= 2;
            }
        }
        else
        {

        }

        if (instance.turnSpeed == 133)
        {
            turn.SetEnabled(false);
        }
    }

    public void onBoostUpgrade(ClickEvent evt)
    {
        if (instance.currentBalance > boostCost)
        {
            instance.fallBoostAvailible = true;
            boost.SetEnabled(false);
        }
        else
        {

        }
    }

    public void onCatchNumberUpgrade(ClickEvent evt)
    {
        if (instance.currentBalance > catchNumberCost)
        {
            if (instance.fishCaptureable < 5)
            {
                instance.currentBalance -= catchNumberCost;
                instance.fishCaptureable += 1;
                catchNumberCost *= 2;
            }
        }
        else
        {

        }

        if (instance.fishCaptureable == 5)
        {
            catchNumber.SetEnabled(false);
        }
    }

    public void onForceUpgrade(ClickEvent evt)
    {
        if (instance.currentBalance > forceCost)
        {
            if (counter < 5)
            {
                instance.currentBalance -= forceCost;
                instance.forceMultiplier *= 2;
                forceCost *= 2;
                counter ++;
            }
        }
        else
        {

        }

        if (instance.forceMultiplier == 10)
        {
            force.SetEnabled(false);
        }
    }
}
