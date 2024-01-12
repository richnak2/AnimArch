using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization.UI
{
    public class ScrollableMethodList : MonoBehaviour
    {
        public GameObject MethodPrefabButton;
        public Transform ButtonParent;
        private List<GameObject> Buttons = new List<GameObject>();
        private List<string> Items = new List<string>();
        
        private void Start()
        {   
            this.Items = new() { "Test1" };
            ConstructButtons();
        }

        public void LoadButtons(List<GameObject> buttons)
        {
            this.Buttons = buttons;
            Refresh();
        }

        public void FillItems(List<string> items)
        {
            this.Items = new List<string>(items);
            foreach (string item in this.Items)
            {
                Debug.Log("Item: " + item);
            }
            Refresh();
        }

        public void Refresh()
        {
            foreach (GameObject button in this.Buttons)
            {
                button.SetActive(false);
            }

            ConstructButtons();
            
            for (int i = 0; i < this.Items.Count; i++)
            {
                this.Buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = this.Items[i];
                this.Buttons[i].SetActive(true);
            }
            
        }

        public string GetSelectedItem(int btnIndex)
        {
            return Items[btnIndex];
        }

        private void ConstructButtons()
        {
            if (this.Items == null)
            {
                return;
            }

            foreach (string item in this.Items)
            {
                GameObject button = Instantiate(MethodPrefabButton, ButtonParent);
                button.GetComponentInChildren<TextMeshProUGUI>().text = item;
                button.GetComponent<Button>().onClick.AddListener(()=>Debug.Log("clicked>> "+ button.GetComponentInChildren<TextMeshProUGUI>().text));
                this.Buttons.Add(button);
                button.SetActive(true);
            }
            Debug.Log("pocet buttons>>> "+ this.Buttons.Count);
        }
    }
}