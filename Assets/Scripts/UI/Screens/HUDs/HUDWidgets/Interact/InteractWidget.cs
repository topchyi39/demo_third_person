using TMPro;
using UnityEngine;

namespace UI.Screens.HUDs.HUDWidgets.Interact
{
    public class InteractParams : WidgetParams
    {
        public string interactText;
    }
    
    public class InteractWidget : HUDWidget
    {
        [SerializeField] private TMP_Text interactText;

        public Transform Transform => transform;


        public override void Show()
        {
            transition.Show();
        }

        public override void Hide()
        {
            transition.Hide();
        }

        public override void ShowWithParams(WidgetParams widgetParams)
        {
            if(widgetParams is not InteractParams interactParams) return;
            
            UpdateParams(interactParams);
            Show();
        }

        public override void UpdateParams(WidgetParams widgetParams)
        {
            if(widgetParams is not InteractParams interactParams) return;
            
            interactText.text = interactParams.interactText;
        }
    }
}