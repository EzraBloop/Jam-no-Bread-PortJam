using UnityEngine;
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


}
