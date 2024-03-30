using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using Visualization.Animation;

namespace Visualization.UI
{
    public class MediatorMainPanel : Mediator
    {
        [SerializeField] private GameObject MainPanel;
        [SerializeField] private GameObject Toggle;
        public MediatorDiagram MediatorDiagram;
        public MediatorMasking MediatorMasking;
        public MediatorAnimation MediatorAnimation;
        public MediatorCreationPanel MediatorCreationPanel;
        public MediatorAnimationPlay MediatorAnimationPlay;

        public override void OnClicked(GameObject gameObject)
        {
            if (ReferenceEquals(gameObject, Toggle))
            {
                OnToggleValueChanged();
            }
        }

        private void OnToggleValueChanged()
        {
            MenuManager.Instance.HideGraphRelations();
        }

        public void SetActiveMainPanel(bool active)
        {
            MainPanel.SetActive(active);
        }
        public void SetActiveCreationPanel(bool active)
        {
            MediatorCreationPanel.SetActiveCreationPanel(active);
        }  
    
    }
}