using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using TMPro;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    private Button button;
    private LocalizeStringEvent eventHandler;
    private TextMeshProUGUI label;

    public void Awake()
    {
        button = this.gameObject.GetComponent<Button>();
        eventHandler = this.gameObject.GetComponent<LocalizeStringEvent>();
        label = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Invoke(nameof(ShowTooltip), 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke(nameof(ShowTooltip));
        TooltipManager.Instance.HideTooltip();
    }

    private void ShowTooltip()
    {
        if (button != null && button.IsActive())
        {
            TooltipManager.Instance.ShowTooltip(eventHandler.StringReference.GetLocalizedString());
        } else if (label != null) {
            TooltipManager.Instance.ShowTooltip(label.text);
        }
    }
}
