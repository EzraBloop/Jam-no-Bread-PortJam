using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Shop : MonoBehaviour
{
    UIDocument ui;
    VisualElement ve;

    public GameManager instance;
    private Button turn, boost, catchNumber, force, backFish, dayLength;
    public int turnCost, boostCost, catchNumberCost, forceCost, dayCost;
    private Label shopText, balanceText;
    private int counter;
    public AudioSounds SFX;

    private void Awake()
    {
        instance = GameManager.Instance;

        turnCost = 10;
        boostCost = 50;
        catchNumberCost = 20;
        forceCost = 5;
        dayCost = 30;

        ui = GetComponent<UIDocument>();
        ve = ui.rootVisualElement as VisualElement;

        turn = ui.rootVisualElement.Q<Button>("TurnUpgrade");
        boost = ui.rootVisualElement.Q<Button>("BoostUpgrade");
        catchNumber = ui.rootVisualElement.Q<Button>("CatchUpgrade");
        force = ui.rootVisualElement.Q<Button>("ForceUpgrade");
        backFish = ui.rootVisualElement.Q<Button>("ReturnFish");
        dayLength = ui.rootVisualElement.Q<Button>("DayUpgrade");

        shopText = ui.rootVisualElement.Q<Label>("ShopKeepText");
        balanceText = ui.rootVisualElement.Q<Label>("BalanceText");

        shopText.text = "Welcome to Meowshu's shop! You want something? It's yours my friend for the right price";
    }

    private void Start()
    {
        BalanceUpdate();
        SFX.PlayAudioClip(Random.Range(0,3));
        if (instance.turnSpeed >= 133)
        {
            turn.SetEnabled(false);
        }

        if(instance.fallBoostAvailible == true)
        {
            boost.SetEnabled(false);
        }

        if (instance.fishCaptureable >= 5)
        {
            catchNumber.SetEnabled(false);
        }

        if (instance.forceMultiplier >= 16)
        {
            force.SetEnabled(false);
        }

        if (instance.dayTimer >= 180f)
        {
            dayLength.SetEnabled(false);
        }
    }
    private void OnEnable()
    {
        turn.RegisterCallback<ClickEvent>(onTurnUpgrade);
        turn.RegisterCallback<MouseEnterEvent>(onTurnHover);

        boost.RegisterCallback<ClickEvent>(onBoostUpgrade);
        boost.RegisterCallback<MouseEnterEvent>(onBoostHover);

        catchNumber.RegisterCallback<ClickEvent>(onCatchNumberUpgrade);
        catchNumber.RegisterCallback<MouseEnterEvent>(onCatchNumberHover);

        force.RegisterCallback<ClickEvent>(onForceUpgrade);
        force.RegisterCallback<MouseEnterEvent>(onForceHover);

        backFish.RegisterCallback<ClickEvent>(onReturnFishing);

        dayLength.RegisterCallback<ClickEvent>(onDayLengthUpgrade);
        dayLength.RegisterCallback<MouseEnterEvent>(onDayLengthHover);
    }

    private void OnDisable()
    {
        turn.UnregisterCallback<ClickEvent>(onTurnUpgrade);
        turn.UnregisterCallback<MouseEnterEvent>(onTurnHover);

        boost.UnregisterCallback<ClickEvent>(onBoostUpgrade);
        boost.UnregisterCallback<MouseEnterEvent>(onBoostHover);

        catchNumber.UnregisterCallback<ClickEvent>(onCatchNumberUpgrade);
        catchNumber.UnregisterCallback<MouseEnterEvent>(onCatchNumberHover);

        force.UnregisterCallback<ClickEvent>(onForceUpgrade);
        force.UnregisterCallback<MouseEnterEvent>(onForceHover);

        backFish.UnregisterCallback<ClickEvent>(onReturnFishing);

        dayLength.UnregisterCallback<ClickEvent>(onDayLengthUpgrade);
        dayLength.UnregisterCallback<MouseEnterEvent>(onDayLengthHover);
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
                Purchase();
                SFX.PlayAudioClip(5);
            }
        }
        else
        {
            NotEnough();
        }

        if (instance.turnSpeed == 133)
        {
            turn.SetEnabled(false);
        }
        BalanceUpdate();
    }
    public void onTurnHover(MouseEnterEvent evt)
    {
        shopText.text = $"Increase Your Horizantal Movement. Current Speed: {instance.turnSpeed}. Price: ${turnCost}";
    }

    public void onBoostUpgrade(ClickEvent evt)
    {
        if (instance.currentBalance > boostCost)
        {
            instance.currentBalance -= boostCost;
            instance.fallBoostAvailible = true;
            boost.SetEnabled(false);
            Purchase();
        }
        else
        {
            NotEnough();
        }
        BalanceUpdate();
    }
    public void onBoostHover(MouseEnterEvent evt)
    {
        shopText.text = $"Gain a Small Boost Downwards Every 5 Seconds. Press F to activate. Price: ${boostCost}";
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
                Purchase();
            }
        }
        else
        {
            NotEnough();
        }

        if (instance.fishCaptureable == 5)
        {
            catchNumber.SetEnabled(false);
        }
        BalanceUpdate();
    }
    public void onCatchNumberHover(MouseEnterEvent evt)
    {
        shopText.text = $"Increase the Number of Fish you Can Catch in One Cast. Current Amount: {instance.fishCaptureable}. Price: ${catchNumberCost}";
    }

    public void onForceUpgrade(ClickEvent evt)
    {
        if (instance.currentBalance > forceCost)
        {
            if (instance.forceMultiplier <= 16)
            {
                instance.currentBalance -= forceCost;
                instance.forceMultiplier *= 2;
                forceCost *= 2;
                counter ++;
                Purchase();
            }
        }
        else
        {
            NotEnough();
        }

        if (instance.forceMultiplier >= 16)
        {
            force.SetEnabled(false);
        }
        BalanceUpdate();
    }
    public void onForceHover(MouseEnterEvent evt)
    {
        shopText.text = $"Increase the Amount of Force You Shoot Out the Plunger With. Current Force Multiplier: {instance.forceMultiplier}. Price: ${forceCost}";
    }

    public void onDayLengthUpgrade(ClickEvent evt)
    {
        if(instance.currentBalance > dayCost)
        {
            if (instance.dayTimer < 180f)
            {
                instance.currentBalance -= dayCost;
                instance.dayTimer += 30f;
                dayCost *= 2;
                Purchase();
            }
        }

        if (instance.dayTimer == 180f)
        {
            dayLength.SetEnabled(false);
        }
        BalanceUpdate();
    }
    public void onDayLengthHover(MouseEnterEvent evt)
    {
        shopText.text = $"Increase the Amount of Time You Have to Catch Fish. Current Time: {instance.dayTimer}. Price: ${dayCost}";
    }

    public void onReturnFishing(ClickEvent evt)
    {
        instance.dailyEarnings = 0;
        SceneManager.LoadScene("MainFishing");
    }

    public void NotEnough()
    {
        shopText.text = "You don't have the money for that, come back when you're a little, mroew, richer!";
        SFX.PlayAudioClip(4);
    }

    public void Purchase()
    {
        shopText.text = "Thank you for shopping at Moewshu's shop!";
        SFX.PlayAudioClip(5);
    }
    public void BalanceUpdate()
    {
        balanceText.text = $"Current Blanace: ${instance.currentBalance}";
    }
}
