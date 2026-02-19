using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ToShop : MonoBehaviour
{
    UIDocument ui;
    VisualElement ve;

    Button shop;
    private void Awake()
    {
        ui = GetComponent<UIDocument>();
        ve = ui.rootVisualElement as VisualElement;

        shop = ui.rootVisualElement.Q<Button>("ToShop");
        
    }

    private void OnEnable()
    {
        shop.RegisterCallback<ClickEvent>(onShop);
    }

    private void OnDisable()
    {
        shop.UnregisterCallback<ClickEvent>(onShop);
    }
    public void onShop(ClickEvent evt)
    {
        SceneManager.LoadScene("ShopScene");
    }
}
