using UI.Transitions;
using UI.UIControls;
using UnityEngine;

namespace UI.Screens
{
    public abstract class Screen : MonoBehaviour
    {
        [SerializeField] protected Transition transition;
        [SerializeField] private UIControl control;
        
        public virtual void Show()
        {
            transition.Show();
            control.Enable();
        }

        public virtual void Hide()
        {
            transition.Hide();
            control.Disable();
        }

        public virtual void SetParams(ScreenParams screenParams)
        {
        }
    }
}