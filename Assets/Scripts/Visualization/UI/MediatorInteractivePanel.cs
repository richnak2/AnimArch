using UnityEngine;
using UnityEngine.UIElements;
using Visualization.Animation;
using Visualization.TODO;

namespace Visualization.UI
{
    public class MediatorInteractivePanel : Mediator
    {
        [SerializeField] private GameObject InteractivePanel;
        [SerializeField] private GameObject ButtonMethod1;
        [SerializeField] private GameObject ButtonMethod2;
        [SerializeField] private GameObject ButtonMethod3;
        [SerializeField] private GameObject ButtonMethod4;
        [SerializeField] private GameObject ButtonMethod5;

        public override void OnClicked(GameObject gameObject)
        {
            if (ReferenceEquals(gameObject, ButtonMethod1))
            {
                OnButtonMethod1Clicked();
            }
            else if (ReferenceEquals(gameObject, ButtonMethod2))
            {
                OnButtonMethod2Clicked();
            }
            else if (ReferenceEquals(gameObject, ButtonMethod3))
            {
                OnButtonMethod3Clicked();
            }
            else if (ReferenceEquals(gameObject, ButtonMethod4))
            {
                OnButtonMethod4Clicked();
            }
            else if (ReferenceEquals(gameObject, ButtonMethod5))
            {
                OnButtonMethod5Clicked();
            }
        }

        private void OnButtonMethod1Clicked()
        {
            MenuManager.Instance.SelectMethod(0);
        }
        private void OnButtonMethod2Clicked()
        {
            MenuManager.Instance.SelectMethod(1);
        }
        private void OnButtonMethod3Clicked()
        {
            MenuManager.Instance.SelectMethod(2);
        }
        private void OnButtonMethod4Clicked()
        {
            MenuManager.Instance.SelectMethod(3);
        }
        private void OnButtonMethod5Clicked()
        {
            MenuManager.Instance.SelectMethod(4);
        }
        
    }
}