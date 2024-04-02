using UnityEngine;
using UnityEngine.UIElements;
using Visualisation.Animation;
using Visualization.Animation;
using Visualization.TODO;

namespace Visualization.UI
{
    public class MediatorPatternCatalogue : Mediator 
    {
        [SerializeField] private GameObject PatternCataloguePanel;
        [SerializeField] private GameObject ButtonExit;
        [SerializeField] private GameObject Separator;
        [SerializeField] private GameObject UpperSeparator;
        [SerializeField] private GameObject PatternCatalogueLabel;
        public PatternCatalogueFileBrowser patternCatalogueFileBrowser = new PatternCatalogueFileBrowser();
        public MediatorMainPanel MediatorMainPanel;
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
            patternCatalogueFileBrowser.Browse();
            Debug.Log("Pattern Catalogue Panel Active");
        }
    }
}