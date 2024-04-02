using UnityEngine;
using TMPro;

public class NotSelectedToAnimatePanel : MonoBehaviour
{
    [SerializeField] private GameObject NotSelectedPanelText;
    private TMP_Text NotSelectedPanelTextComponent;

    void Awake()
    {
        NotSelectedPanelTextComponent = NotSelectedPanelText.GetComponent<TMP_Text>();
    }

    public void SetNotSelectedPanelText(string text)
    {
        NotSelectedPanelTextComponent.text = text;
    }
}