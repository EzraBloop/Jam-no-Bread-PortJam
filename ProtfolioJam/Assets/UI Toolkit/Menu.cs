using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    UIDocument ui;
    VisualElement ve;

    Button play, quit;

    private void Awake()
    {
        ui = GetComponent<UIDocument>();
        ve = ui.rootVisualElement as VisualElement;

        play = ui.rootVisualElement.Q<Button>("Play");
        quit = ui.rootVisualElement.Q<Button>("Quit");
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
        SceneManager.UnloadSceneAsync("Menu");
        SceneManager.LoadScene("MainFishing", LoadSceneMode.Single);
    }

    public void onQuit(ClickEvent click)
    {
        Application.Quit();
    }
}
