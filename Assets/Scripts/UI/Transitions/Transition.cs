using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Transitions
{
    public abstract class Transition : MonoBehaviour
    {
        public abstract void Show();
        public abstract void Hide();

        public virtual void ShowImmediately() {}
        public virtual void HideImmediately() {}
        public virtual async UniTask ShowAsync() {}
        public virtual async UniTask HideAsync() {}
    }
}