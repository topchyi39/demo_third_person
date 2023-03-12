using ItemsCore.Items;

namespace ItemsCore.Grab
{
    public interface IInventoryItem
    {
        public void Remove();
        public void Use();
        public void Drop();
        public BaseItemData GetInformation();
    }
}