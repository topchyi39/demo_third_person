using TMPro;
using UnityEngine;

namespace UI.Screens.HUDs.HUDWidgets.NotifyWidget.ItemNotifier
{
    public class ItemNotifyObject : NotifyObject
    {
        [SerializeField] private TMP_Text itemName;
        
        
        public override void SetParams(WidgetParams widgetParams)
        {
            if(widgetParams is not ItemNotifyParams itemNotifyParams) return;

            itemName.text = itemNotifyParams.itemName;
        }
    }
}