
using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{

    [SerializeField] GameObject timer, gameManager;
    
    [SerializeField] float startTime = 60f, currentTime;
    [SerializeField] bool timerActive;

    [SerializeField] TMP_Text[] scoresText;
    void Start()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timerActive = false;
        currentTime = startTime;
        UpdateTimerDisplay();
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
                //gameManager.GetComponent<GameManager>().EndGame();
            }
        }
        for (int i = 0; i < scoresText.Length; i++)
        {
            //scoresText[i].text = $"{gameManager.GetComponent<GameManager>().Scores[i]}";
        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timer.GetComponent<TMP_Text>().text = $"{seconds}";
    }

    public void PauseTimer()
    {
        timerActive = false;
    }
    public void StartTimer()
    {
        timerActive = true;
    }
}
