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
        
        private bool EditMode = true;
        private void Start()
        {   
            this.Items = new();
        }

        public void FillItems(List<string> items, bool editMode = true)
        {
            this.EditMode = editMode;
            this.Items = new List<string>(items);
            Refresh();
        }

        public void Refresh()
        {
            foreach (GameObject button in this.Buttons)
            {
                Destroy(button); 
            }
            this.Buttons.Clear();

            ConstructButtons();
        }

        public void ClearItems()
        {
            foreach (GameObject button in this.Buttons)
            {
                Destroy(button); 
            }
            this.Buttons.Clear();
            this.Items.Clear();
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
                
                if(this.EditMode){
                    button.GetComponent<Button>().onClick.AddListener(() => MenuManager.Instance.SelectMethod(item));
                }else{
                    button.GetComponent<Button>().onClick.AddListener(() => MenuManager.Instance.SelectPlayMethod(item));
                }
                button.SetActive(true);
                this.Buttons.Add(button);
            }
        }
    }
}