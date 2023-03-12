using System.Collections.Generic;
using ItemsCore.Grab;
using UnityEngine;

namespace Inventory
{
    public abstract class ItemsHolder : MonoBehaviour
    {
        [SerializeField] private protected Slot slotPrefab;
        
        protected readonly HashSet<Slot> slots = new HashSet<Slot>();

        public virtual void AddSlot(IInventoryItem inventoryItem)
        {
            var slot = Instantiate(slotPrefab);
            slot.InitializeSlot(inventoryItem);
            slots.Add(slot);
        }

        public virtual void RemoveSlot()
        {
            
        }
        
        public virtual void Show(){}
        public virtual void Hide(){}
    }
}