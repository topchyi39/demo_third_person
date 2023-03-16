using System;
using ItemsCore.Items.Data.OtherData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ItemsCore.Items.Data.Base
{
    [Serializable]
    [InlineEditor]
    public abstract class BaseItemData : ScriptableObject
    {
        public Func<string, int, bool> onNameValidation;
        public event Action<BaseItemData, string> onNameChanged; 

        [HorizontalGroup("Base", Width = 150)]
        
        [VerticalGroup("Base/Left")]
        [LabelWidth(90)]
        [HideLabel, Title("Name", Bold = true, HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
        [OnValueChanged("OnNameChanged")]
        [ValidateInput("ValidateName", MessageType = InfoMessageType.Error, DefaultMessage = "Data with same `itemName` is already exists!")]
        [ShowInInspector]
        public string itemName { get; private set; }
        
        [VerticalGroup("Base/Left")]
        [PreviewField(145)]
        [HideLabel]
        [ShowInInspector]
        public Sprite icon { get; private set; }
        
        [VerticalGroup("Base/Right")]
        [MultiLineProperty(10)]
        [HideLabel, Title("Description", Bold = true, HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
        [ShowInInspector]
        public string description { get; private set; }
        
        [HorizontalGroup("Base/Right/Lower")]
        
        [VerticalGroup("Base/Right/Lower/Middle")]
        [LabelWidth(50)] 
        [ReadOnly]
        [ShowInInspector]
        public int id { get; private set; }
        
        [VerticalGroup("Base/Right/Lower/Middle")]
        [LabelWidth(50)] 
        [PropertyRange(0, 30)]
        [ShowInInspector]
        public float weight { get; private set; }
        
        [VerticalGroup("Base/Right/Lower/Middle")]
        [LabelWidth(50)] 
        [PropertyRange(0, 10000)]
        [ShowInInspector]
        public float cost { get; private set; }
        
        [VerticalGroup("Base/Right/Lower/Middle")]
        [LabelWidth(50)]
        [ShowInInspector]
        public RarityType rarity { get; private set; }

        public void SetNewID(int newId)
        {
            id = newId;
        }

        public void SetName(string newItemName)
        {
            itemName = newItemName;
        }
        
        private void OnNameChanged()
        {
            onNameChanged?.Invoke(this, itemName);
        }

        private bool ValidateName()
        {
            var result = onNameValidation(itemName, id);
            return result;
        }
    }
}