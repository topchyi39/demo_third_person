using System;
using System.Collections.Generic;
using UI.Screens.HUDs.HUDWidgets;
using UI.Screens.HUDs.HUDWidgets.NotifyWidget;
using UI.Screens.HUDs.HUDWidgets.NotifyWidget.ItemNotifier;
using UnityEngine;

namespace UI.Screens.HUDs
{
    
    public class HUD : Screen
    {
        private Dictionary<Type, HUDWidget> _widgets = new();

        private void Awake()
        {
            InitializeWidget();
        }

        private void InitializeWidget()
        {
            var widgetArray = GetComponentsInChildren<HUDWidget>();
            foreach (var widget in widgetArray)
            {
                _widgets.Add(widget.GetType(), widget);
            }
        }

        public void NotifyWidget<T>(WidgetParams widgetParams) where T : NotifyHUDWidget
        {
            var widget = GetWidget<T>() as NotifyHUDWidget;
            
            if (!widget) return;
            
            widget.Notify(widgetParams);
            SetLastSiblingIndex(widget.transform);
            widget.Show();}

        public void ShowWidget<T>() where T : HUDWidget
        {
            var widget = GetWidget<T>();
            if (!widget) return;

            SetLastSiblingIndex(widget.transform);
            widget.Show();
        }

        public void HideWidget<T>() where T : HUDWidget
        {
            var widget = GetWidget<T>();
            if (!widget) return;
            
            SetFirstSiblingIndex(widget.transform);
            widget.Hide();
        }
        
        public void ShowWidgetWithParams<T>(WidgetParams widgetParams) where T : HUDWidget
        {
            var widget = GetWidget<T>();

            if (!widget) return;
            
            SetLastSiblingIndex(widget.transform);
            widget.ShowWithParams(widgetParams);
        }

        public void UpdateWidgetParams<T>(WidgetParams widgetParams) where T : HUDWidget
        {
            var widget = GetWidget<T>();
            
            if(widget)
                widget.UpdateParams(widgetParams);
        }
        
        private T GetWidget<T>() where T : HUDWidget
        {
            if (!_widgets.TryGetValue(typeof(T), out var widget)) return null;

            return widget as T;
        }

        private void SetLastSiblingIndex(Transform childTransform)
        {
            childTransform.SetAsLastSibling();
        }

        private void SetFirstSiblingIndex(Transform childTransform)
        {
            childTransform.SetAsFirstSibling();
        }
    }
}