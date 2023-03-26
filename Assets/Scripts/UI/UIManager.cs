using System;
using System.Collections.Generic;
using Inputs;
using UI.Screens;
using UnityEngine;
using Screen = UI.Screens.Screen;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private string defaultTypeName;

        private IInGameInput _inGameInput;
        
        private Screen[] screens;
        private Screen _currentScreen;
        
        private readonly Stack<Screen> _screenHistory = new();
        private readonly Dictionary<Type, Screen> _screensDictionary = new();
        
        private void Awake()
        {
            screens = GetComponentsInChildren<Screen>();
            
            foreach (var screen in screens)
            {
                _screensDictionary.Add(screen.GetType(), screen);
                screen.Hide();
            }

            if(!string.IsNullOrEmpty(defaultTypeName))
            {
                var defaultType = Type.GetType(defaultTypeName);
                OpenScreen(defaultType);
            }
        }

        public T GetScreen<T>() where T : Screen
        {
            if (_screensDictionary.TryGetValue(typeof(T), out var screen)) return screen as T;

            return null;
        }
        
        public void OpenScreen<T>() where T : Screen { OpenScreen(typeof(T)); }

        public void OpenScreenWithParams<T>(ScreenParams screenParams) where T : Screen
        {
            OpenScreen(typeof(T), screenParams);
        }

        public void OpenPrevious()
        {
            if (_currentScreen)
            {
                _currentScreen.Hide();
                _currentScreen.transform.SetAsFirstSibling();
            }

            if(_screenHistory.Count < 1) return;
            
            _currentScreen = _screenHistory.Pop();
            
            _currentScreen.Show();
            _currentScreen.transform.SetAsLastSibling();
        }

        public string GetDefaultScreenTypeName()
        {
            return defaultTypeName;
        }
        
        public void SetDefaultScreen(string typeName)
        {
            defaultTypeName = typeName;
        }

        private void OpenScreen(Type screenType, ScreenParams screenParams = null)
        {
            if (!_screensDictionary.TryGetValue(screenType, out var screen)) return;

            if (_currentScreen)
            {
                _screenHistory.Push(_currentScreen);
                _currentScreen.Hide();
                _currentScreen.transform.SetAsFirstSibling();
            }

            _currentScreen = screen;

            if (screenParams != null)
                _currentScreen.SetParams(screenParams);
            
            _currentScreen.Show();
            _currentScreen.transform.SetAsLastSibling();
        }
    }
}