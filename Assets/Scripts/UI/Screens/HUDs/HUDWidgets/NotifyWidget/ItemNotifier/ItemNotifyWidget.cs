using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Screens.HUDs.HUDWidgets.NotifyWidget.ItemNotifier
{
    public class ItemNotifyParams : WidgetParams
    {
        public string itemName;
    }
    
    public class ItemNotifyWidget : NotifyHUDWidget
    {
        [SerializeField] private Transform containerTransform;
        [SerializeField] private int maxNotifyObjects;
        
        private readonly List<NotifyObject> _currentObjects = new();

        public override void Notify(WidgetParams widgetParams)
        {
            var objectInstance = Instantiate(notifyObjectPrefab, containerTransform);
            objectInstance.SetParams(widgetParams);
            ShowObject(objectInstance);
        }
        
        private async void ShowObject(NotifyObject notifyObject)
        {
            if (_currentObjects.Count >= maxNotifyObjects)
                await UniTask.WaitUntil(()=> _currentObjects.Count < maxNotifyObjects);
            
            notifyObject.Show();
            _currentObjects.Add(notifyObject);
            
            await WaitForSeconds(2f);
            
            await notifyObject.HideAsync();
            
            _currentObjects.Remove(notifyObject);
            Destroy(notifyObject.gameObject);
        }

        private IEnumerator WaitForSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }
    }
}