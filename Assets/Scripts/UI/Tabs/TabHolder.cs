using System;
using System.Collections.Generic;
using UI.Screens.Menu;
using UnityEngine;

namespace UI.Tabs
{
    public abstract class TabHolder<T> : MonoBehaviour where T : Enum
    {
        private Dictionary<T, Tab<T>> _tabs = new();

        private Tab<T> _currentTab;
        
        private void Awake()
        {
            var tabs = GetComponentsInChildren<Tab<T>>();
            foreach (var tab in tabs)
            {
                _tabs.Add(tab.TabName, tab);
                tab.Hide();
                tab.OnShowed += TabOpened;
            }
        }

        public void ShowTab(T tabType)
        {
            if (!_tabs.ContainsKey(tabType)) return;
            
            if (_currentTab)
                _currentTab.Hide();

            var tab = _tabs[tabType];
            _currentTab = tab;
            tab.Show();
        }

        private void TabOpened(Tab<T> tab)
        {
            if(_currentTab == tab) return;
            
            if (_currentTab)
                _currentTab.Hide();

            _currentTab = tab;
        }
    }
}