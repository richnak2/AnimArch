using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Visualization.Animation;
using Visualization.ClassDiagram;


namespace Visualization.UI
{
    public class MediatorAnimationPlayOpt : Mediator
    {
        [SerializeField] private GameObject PlayBtn1;
        [SerializeField] private GameObject PlayBtn2;
        [SerializeField] private GameObject PlayBtn3;
        [SerializeField] private GameObject PlayBtn4;
        [SerializeField] private GameObject ButtonExit;
        public MediatorAnimationPlay MediatorAnimationPlay;
        
        public override void OnClicked(GameObject gameObject)
        {
            if (ReferenceEquals(gameObject, PlayBtn1))
            {
                OnPlayBtn1Clicked();
            }
            else if (ReferenceEquals(gameObject, PlayBtn2))
            {
                OnPlayBtn2Clicked();
            }
            else if (ReferenceEquals(gameObject, PlayBtn3))
            {
                OnPlayBtn3Clicked();
            }
            else if (ReferenceEquals(gameObject, PlayBtn4))
            {
                OnPlayBtn4Clicked();
            }
            else if (ReferenceEquals(gameObject, ButtonExit))
            {
                OnButtonExitClicked();
            }
        }

        private void OnPlayBtn1Clicked()
        {
            MenuManager.Instance.SelectPlayMethod(0);
        }
        private void OnPlayBtn2Clicked()
        {
            MenuManager.Instance.SelectPlayMethod(1);
        }
        private void OnPlayBtn3Clicked()
        {
            MenuManager.Instance.SelectPlayMethod(2);
        }
        private void OnPlayBtn4Clicked()
        {
            MenuManager.Instance.SelectPlayMethod(3);
        }
        private void OnButtonExitClicked()
        {
            MenuManager.Instance.UnshowAnimation();
            MenuManager.Instance.EndPlay();
            DiagramPool.Instance.ObjectDiagram.ResetDiagram();
            MediatorAnimationPlay.SetActiveAnimationPlay(false);
            MediatorAnimationPlay.SetActiveMainPanel(true);
        }

    }
}