using System;
using Inventory.UI;
using ItemsCore.Grab;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ItemsTool itemsTool;
        
        private SlotData _slotData;
        
        public void InitializeSlot(IInventoryItem item)
        {
            _slotData = new SlotData(item);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            var buttonType = eventData.button;
            
            switch(buttonType)
            {
                case PointerEventData.InputButton.Left:
                    break;
                case PointerEventData.InputButton.Right:
                    break;
                case PointerEventData.InputButton.Middle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
}