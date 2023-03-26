using System.Collections;
using Cysharp.Threading.Tasks;
using UI.Transitions;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Screens.HUDs.HUDWidgets.NotifyWidget
{
    public abstract class NotifyObject : MonoBehaviour
    {
        [SerializeField] protected Transition transition;
        
        public abstract void SetParams(WidgetParams widgetParams);

        public void Show()
        {
            transition.Show();
        }

        public void Hide()
        {
            transition.Hide();
        }
        public async UniTask ShowAsync()
        {
            await transition.ShowAsync();
        }
        
        public async UniTask HideAsync()
        {
            await transition.HideAsync();
        }
    }
    
    public abstract class NotifyHUDWidget : HUDWidget
    {
        [SerializeField] protected NotifyObject notifyObjectPrefab;

        public override void Show() { }

        public override void Hide() { }

        public override void ShowWithParams(WidgetParams widgetParams) { }

        public override void UpdateParams(WidgetParams widgetParams) { }
        
        public abstract void Notify(WidgetParams widgetParams);
    }
}