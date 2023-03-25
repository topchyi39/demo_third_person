using UI.Transitions;
using UnityEngine;

namespace UI.Screens.HUDs.HUDWidgets
{
    public abstract class HUDWidget : MonoBehaviour
    {
        [SerializeField] protected Transition transition;
        
        
        public abstract void Show();
        public abstract void Hide();
        public abstract void ShowWithParams(WidgetParams widgetParams);
        public abstract void UpdateParams(WidgetParams widgetParams);
    }
}