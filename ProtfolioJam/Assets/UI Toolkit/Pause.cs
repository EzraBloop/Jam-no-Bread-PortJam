using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    UIDocument ui;
    VisualElement ve;

    Button play, quit;

    bool paused = false;

    private void Awake()
    {
        ui = GetComponent<UIDocument>();
        ve = ui.rootVisualElement as VisualElement;

        play = ui.rootVisualElement.Q<Button>("Play");
        quit = ui.rootVisualElement.Q<Button>("Quit");

        ve.SetEnabled(false);
        ve.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(!paused)
            {
                ve.SetEnabled(true);
                ve.style.display = DisplayStyle.Flex;
                Time.timeScale = 0;
                paused = true;

            }
            else
            {
                ve.SetEnabled(false);
                ve.style.display = DisplayStyle.None;
                Time.timeScale = 1.0f;
                paused = false;
            }
        }
    }

    private void OnEnable()
    {
        play.RegisterCallback<ClickEvent>(onPlay);
        quit.RegisterCallback<ClickEvent>(onQuit);
    }

    private void OnDisable()
    {
        play.UnregisterCallback<ClickEvent>(onPlay);
        quit.UnregisterCallback<ClickEvent>(onQuit);
    }


    public void onPlay(ClickEvent click)
    {
        paused = false;
        Time.timeScale = 1.0f;
        ve.SetEnabled(false);
        ve.style.display = DisplayStyle.None;
    }

    public void onQuit(ClickEvent click)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
}
