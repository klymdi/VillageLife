using TMPro;
using UnityEngine;

public class InteractionPanelUI : MonoBehaviour
{
    public static InteractionPanelUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI textComponent;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        HidePanel();
    }

    public void ShowPanel(string text)
    {
        this.gameObject.SetActive(true);
        textComponent.text = text;
    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
}