using UnityEngine;

namespace UI.UIControls
{
    public abstract class UIControl : MonoBehaviour
    {
        public abstract void Enable();
        public abstract void Disable();
    }
}