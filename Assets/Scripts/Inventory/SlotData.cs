using System;
using ItemsCore.Grab;

namespace Inventory
{
    [Serializable]
    public class SlotData : IEquatable<SlotData>
    {
        public string id { get; }
        public string name { get; }
        public int count { get; private set; }

        private IInventoryItem _inventoryItem;
        
        public SlotData(IInventoryItem inventoryItem)
        {
            _inventoryItem = inventoryItem;
            var information = _inventoryItem.GetInformation();

            id = information.ID;
            name = information.Name;
            count = 1;
        }

        public void IncreaseCount()
        {
            count++;
        }

        public void DecreaseCount()
        {
            count--;
        }

        public bool Equals(SlotData other)
        {
            return id == other.id && name == other.name;
        }

        public override bool Equals(object obj)
        {
            return obj is SlotData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, name);
        }
    }
}