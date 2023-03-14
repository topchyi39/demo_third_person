using System;
using Sirenix.OdinInspector;

namespace Editor.ButtonGroups
{
    [Serializable, HideLabel]
    public struct ItemTools
    {
        // [Searchable] 
        // public List<SlotData> slotsData;
        //
        // public ItemTools(List<SlotData> slotsData)
        // {
        //     this.slotsData = slotsData;
        // }
        
        [ButtonGroup(ButtonHeight = 25), Button(SdfIconType.Plus, "")]
        void Add() { }
        
    }
}