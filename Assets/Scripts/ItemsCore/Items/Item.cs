using ItemsCore.Grab;
using UnityEngine;

namespace ItemsCore.Items
{
    public abstract class Item : MonoBehaviour
    {
        private IPickable _pickable;

        private void Start()
        {
            _pickable = GetComponent<IPickable>();
        }
    }
}
