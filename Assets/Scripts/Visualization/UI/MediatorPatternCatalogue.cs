using UnityEngine;
using UnityEngine.UIElements;
using Visualisation.Animation;
using Visualization.Animation;
using Visualization.TODO;
using TMPro;
using System.Collections.Generic;

namespace Visualization.UI
{
    public class MediatorPatternCatalogue : Mediator 
    {
        [SerializeField] private GameObject PatternCataloguePanel;
        [SerializeField] private GameObject ButtonExit;
        [SerializeField] private GameObject Separator;
        [SerializeField] private GameObject UpperSeparator;
        [SerializeField] private GameObject PatternCatalogueLabel;
        [SerializeField] private GameObject PrefabCanvas;
        [SerializeField] private GameObject PrefabPattern;
        [SerializeField] private GameObject PrefabLeaf;
        private PatternCatalogueCompositeLoader patternCatalogueLoader = new PatternCatalogueCompositeLoader();
        public MediatorMainPanel MediatorMainPanel;
        Component patternCatalogueComponent = new Composite("PatternCatalogue");
        public override void OnClicked(GameObject ButtonExit)
        {
            if (ReferenceEquals(ButtonExit, ButtonExit))
            {
                OnButtonExitClicked();
            }
        }
        private void OnButtonExitClicked()
        {
            MediatorMainPanel.UnshowPatternCatalogue();
        }

        public void SetActivePatternCataloguePanel(bool active)
        {
            PatternCataloguePanel.SetActive(active);
            Separator.SetActive(active);
            UpperSeparator.SetActive(active);
            PatternCatalogueLabel.SetActive(active);
            ButtonExit.SetActive(active);
            patternCatalogueLoader.Browse(patternCatalogueComponent);
            RecursiveCreatePatternPrefabs(PrefabCanvas,patternCatalogueComponent);
            //CreatePatternPrefabsIteratively(PrefabCanvas, patternCatalogueComponent);
        }

        //TODO Not assigning the children in the scene correctly
        // ukladať referencie detí, a setactive podla kliku na parenta
        // dvakrat načítanie katalogu - naloaduje ich moc moc
        // premenovať composite, component a leaf - na nejaké krajšie nech je jasné
        // kde-čo-ako sa nachadzajú
        // method pagination = inšpirácia na priradovanie parenta
        // priradit parent do atribútu
        private void RecursiveCreatePatternPrefabs(GameObject parent, Component patternCatalogueComponent)
        {
            foreach(Component child in patternCatalogueComponent.GetComposite().GetChildren())
            {
                if(child is Composite)
                {
                    GameObject newParent = Instantiate(PrefabPattern);
                    newParent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = child.GetName();
                    newParent.transform.SetParent(parent.transform, false);
                    RecursiveCreatePatternPrefabs(newParent, child.GetComposite());
                }
                else if(child is Leaf)
                {
                    GameObject newPattern = Instantiate(PrefabLeaf);
                    newPattern.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = child.GetName();
                    newPattern.transform.SetParent(parent.transform, false);
                }
            }
        }

        private void CreatePatternPrefabsIteratively(GameObject parent, Component patternCatalogueComponent)
        {
            Stack<(GameObject, Component)> stack = new Stack<(GameObject, Component)>();
            stack.Push((parent, patternCatalogueComponent));

            while (stack.Count > 0)
            {
                (GameObject currentParent, Component currentComponent) = stack.Pop();

                foreach (Component child in currentComponent.GetComposite().GetChildren())
                {
                    if (child is Composite)
                    {
                        GameObject newParent = new GameObject();
                        newParent.transform.SetParent(currentParent.transform);
                        newParent = Instantiate(PrefabPattern, newParent.transform);
                        newParent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = child.GetName();
                        stack.Push((newParent, child.GetComposite()));
                    }
                    else if (child is Leaf)
                    {
                        GameObject newPattern = new GameObject();
                        newPattern.transform.SetParent(currentParent.transform);
                        newPattern = Instantiate(PrefabLeaf, newPattern.transform);
                        newPattern.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = child.GetName();
                    }
                }
            }
        }

    }
}