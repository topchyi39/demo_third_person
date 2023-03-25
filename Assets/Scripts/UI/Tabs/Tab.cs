using System;
using UI.Transitions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Tabs
{
    public abstract class Tab<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] protected Transition transition;
        [SerializeField] protected Button headerButton;
        [Space]
        [SerializeField] protected UnityEvent<Tab<T>> onShowed;
        
        public abstract T TabName { get; }
        
        public event UnityAction<Tab<T>> OnShowed
        {
            add => onShowed.AddListener(value);
            remove => onShowed.RemoveListener(value);
        }

        private void Awake()
        {
            if(headerButton)
                headerButton.onClick.AddListener(ShowByButton);
        }

        public virtual void Show()
        {
            transition.Show();
        }
        
        public virtual void ShowByButton()
        {
            Show();
            onShowed?.Invoke(this);
        }

        public virtual void Hide()
        {
            transition.Hide();
        }
        
    }
}