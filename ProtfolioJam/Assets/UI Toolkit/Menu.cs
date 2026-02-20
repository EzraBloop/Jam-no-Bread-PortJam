using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    UIDocument ui;
    VisualElement ve;

    Button play, quit;
    public AudioSounds SFX;

    private List<Button> menuButtons = new List<Button>();

    private void Awake()
    {
        ui = GetComponent<UIDocument>();
        ve = ui.rootVisualElement as VisualElement;

        play = ui.rootVisualElement.Q<Button>("Play");
        quit = ui.rootVisualElement.Q<Button>("Quit");

        menuButtons = ui.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
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
        //SFX.PlayAudioClip(0);
        StartCoroutine(Delay());
    }

    public void onQuit(ClickEvent click)
    {
        //SFX.PlayAudioClip(0);
        Application.Quit();
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        SFX.PlayAudioClip(0);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("MainFishing", LoadSceneMode.Single);
    }
}
