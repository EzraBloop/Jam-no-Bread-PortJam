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
    private Label shopText;
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

        shopText.text = "Welcome to Meowshu's shop! You want something? It's yours my friend for the right price";
    }

    private void Start()
    {
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
        boost.RegisterCallback<ClickEvent>(onBoostUpgrade);
        catchNumber.RegisterCallback<ClickEvent>(onCatchNumberUpgrade);
        force.RegisterCallback<ClickEvent>(onForceUpgrade);
        backFish.RegisterCallback<ClickEvent>(onReturnFishing);
        dayLength.RegisterCallback<ClickEvent>(onDayLengthUpgrade);
    }

    private void OnDisable()
    {
        turn.UnregisterCallback<ClickEvent>(onTurnUpgrade);
        boost.UnregisterCallback<ClickEvent>(onBoostUpgrade);
        catchNumber.UnregisterCallback<ClickEvent>(onCatchNumberUpgrade);
        force.UnregisterCallback<ClickEvent>(onForceUpgrade);
        backFish.UnregisterCallback<ClickEvent>(onReturnFishing);
        dayLength.UnregisterCallback<ClickEvent>(onDayLengthUpgrade);
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
    }

    public void onBoostUpgrade(ClickEvent evt)
    {
        if (instance.currentBalance > boostCost)
        {
            instance.fallBoostAvailible = true;
            boost.SetEnabled(false);
            Purchase();
        }
        else
        {
            NotEnough();
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
    }

    public void onDayLengthUpgrade(ClickEvent evt)
    {
        if(instance.currentBalance > dayCost)
        {
            if (instance.dayTimer < 180f)
            {
                instance.dayTimer += 30f;
                dayCost *= 2;
                Purchase();
            }
        }

        if (instance.dayTimer == 180f)
        {
            dayLength.SetEnabled(false);
        }
    }

    public void onReturnFishing(ClickEvent evt)
    {
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
}
