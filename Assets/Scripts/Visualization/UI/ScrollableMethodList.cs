using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization.UI
{
    public class ScrollableMethodList : MonoBehaviour
    {
        public GameObject MethodPrefab;
        public Transform ButtonParent;
        private List<GameObject> Buttons;

        private void Start()
        {   
            ConstructButtons();
        }

        public ScrollableMethodList(List<GameObject> buttons)
        {
                this.Buttons = buttons;
        }

        private void ConstructButtons()
        {
                foreach(GameObject metbutton in this.Buttons)
                {
                        
                        GameObject button = Instantiate(MethodPrefab, ButtonParent);
                        button.GetComponentInChildren<TextMeshProUGUI>().text = "ananas";
                        button.GetComponent<Button>().onClick.AddListener(() => Debug.Log("Click"));
                        Buttons.Add(button);
                        button.SetActive(true);
                }
        }
    }
}