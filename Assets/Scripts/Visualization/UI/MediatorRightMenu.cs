using UnityEngine;
using UnityEngine.UIElements;
using Visualization.Animation;

namespace Visualization.UI
{
    public class MediatorRightMenu : Mediator
    {
        [SerializeField] private GameObject RightMenu;

        public override void OnClicked(GameObject gameObject)
        {
            throw new System.NotImplementedException();
        }

        public void SetActiveRightMenu(bool active)
        {
            RightMenu.SetActive(active);
        }
    
    }
}