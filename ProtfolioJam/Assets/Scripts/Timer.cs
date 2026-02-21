
using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject timer;
    
    [SerializeField] float startTime, currentTime;
    [SerializeField] bool timerActive;

    public Camera cam;
    public Earning ear;

    void Start()
    {
        gameManager = GameManager.Instance;
        startTime = gameManager.dayTimer;
        currentTime = startTime;
        UpdateTimerDisplay();
        timerActive = false;
    }

    void Update()
    {
        if (timerActive)
        {
            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                currentTime = 0;
                timerActive = false;
                Debug.Log("Timer Over");
                EndDay();
            }
        }
        
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(currentTime);
        timer.GetComponent<TMP_Text>().text = $"Day Ends In {seconds}";
    }

    public void PauseTimer()
    {
        timerActive = false;
    }
    public void StartTimer()
    {
        timerActive = true;
    }

    public void EndDay()
    {
        if(cam.depth == 0)
        {
            cam.depth = -2;
            ear.DisplayFish();
        }
        else
        {
            cam.depth = 0;
        }
    }
}
