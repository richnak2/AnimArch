using UnityEngine;
using UnityEngine.UIElements;
using Visualization.Animation;
using Visualization.TODO;

namespace Visualization.UI
{
    public class MediatorCodePanel : Mediator
    {
        [SerializeField] private GameObject CodePanel;
        [SerializeField] private GameObject ButtonFull;
        [SerializeField] private GameObject ButtonSep;
        [SerializeField] private GameObject ButtonExit;
        [SerializeField] private GameObject FullInput;
        [SerializeField] private GameObject SepInput;
        [SerializeField] private GameObject ButtonCheck;
        [SerializeField] private UnityEngine.UI.Button ButtonSave;

        public override void OnClicked(GameObject gameObject)
        {
            if (ReferenceEquals(gameObject, ButtonFull))
            {
                OnButtonFullClicked();
            }
            else if (ReferenceEquals(gameObject, ButtonSep))
            {
                OnButtonSepClicked();
            }
            else if (ReferenceEquals(gameObject, ButtonExit))
            {
                OnButtonExitClicked();
            }
            else if (ReferenceEquals(gameObject, FullInput))
            {
                OnFullInputSelected();
            }
            else if (ReferenceEquals(gameObject, SepInput))
            {
                OnSepInputSelected();
            }
            else if (ReferenceEquals(gameObject, ButtonCheck))
            {
                OnButtonCheckClicked();
            }
            else if (ReferenceEquals(gameObject, ButtonSave))
            {
                OnButtonSaveClicked();
            }
        }

        private void OnButtonFullClicked()
        {
            FullInput.SetActive(true);
            SepInput.SetActive(false);
        }
        private void OnButtonSepClicked()
        {
            FullInput.SetActive(false);
            SepInput.SetActive(true);
        }
        private void OnButtonExitClicked()
        {
            MenuManager.Instance.EndAnimate();
            Animation.Animation.Instance.UnhighlightAll();
        }
        private void OnFullInputSelected()
        {
            ButtonSave.gameObject.SetActive(false);
            FullInput.GetComponent<CodeHighlighter>().RemoveColors();
        }
        private void OnSepInputSelected()
        {
            ButtonSave.gameObject.SetActive(false);
            SepInput.GetComponent<CodeHighlighter>().RemoveColors();
        }
        private void OnButtonCheckClicked()
        {
            CodePanel.GetComponent<CheckScript>().CheckScriptCode();
            FullInput.GetComponent<CodeHighlighter>().UpdateColors();
        }
        private void OnButtonSaveClicked()
        {
            MenuManager.Instance.SaveAnimation();
        }

        public void UpdateColors()
        {
            FullInput.GetComponent<CodeHighlighter>().UpdateColors();
        }
        public void SetInteractableButtonSave(bool active)
        {
            ButtonSave.interactable = active;
        }
        
    }
}